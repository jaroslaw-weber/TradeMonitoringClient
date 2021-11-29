

using System;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TradeMonitoringClient.Data
{

    /// <summary>
    /// Connecting to server through websockets, listening to server and receiving data.
    /// Each API should inherit this class
    /// </summary>
    public abstract class WebsocketListenService<TMessage> where TMessage: class
    {
        /// <summary>
        /// Is connected to websocket and waiting for the messages?
        /// </summary>
        public bool IsRunning { get; private set; } = false;

        /// <summary>
        /// Cancellation token
        /// </summary>
        private CancellationTokenSource token;

        private ClientWebSocket webSocket;

        public ILogger Logger { get; set; }

        /// <summary>
        /// Endpoint for this api
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// What to do after receiving data (ex. update view)
        /// </summary>
        public System.Action<TMessage> OnDataReceived { get; set; } = p => { };

        /// <summary>
        /// Server port
        /// </summary>
        private int port = 6000;

        public async Task ConnectToServer()
        {
            webSocket = new ClientWebSocket();
            token =  new CancellationTokenSource();
            await webSocket.ConnectAsync(new Uri($"ws://localhost:{port}/{Endpoint}"), token.Token);

            Logger.LogInformation("connected to server!");

        }

        /// <summary>
        /// Run loop listening to server messages through websockets
        /// </summary>
        public void StartListeningToWebsocket()
        {
            Logger.LogInformation("start listening...");
            IsRunning = true;
            //Run loop without blocking main thread.
            Task.Run(() => this.ListenToWebsocket());
        }

        /// <summary>
        /// Listening to server messages through websockets
        /// </summary>

        private async Task ListenToWebsocket()
        {
            //while (!disposalTokenSource.IsCancellationRequested)
            while (IsRunning)
            {
                Logger.LogInformation("waiting for new message...");

                var buffer = new ArraySegment<byte>(new Byte[8192]);

                WebSocketReceiveResult result = null;
                string message = null;
                try
                {
                    //message may be split in chunks
                    //need to wait for the end of message

                    using (var memory = new MemoryStream())
                    {

                        while (true)
                        {
                            result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                            memory.Write(buffer.Array, buffer.Offset, result.Count);
                            if (result.EndOfMessage) break;
                        }

                        memory.Seek(0, SeekOrigin.Begin);

                        using (var reader = new StreamReader(memory, Encoding.UTF8))
                        {
                            message = reader.ReadToEnd();
                        }

                    }
                }
                catch(Exception e)
                {
                    Logger.LogCritical("failed getting message: " + e);
                }

                Logger.LogInformation("new message!:" + message);

                TMessage parsed = null;
                try
                {
                    parsed = JsonSerializer.Deserialize<TMessage>(message);
                }
                catch (Exception e)
                {
                    Logger.LogCritical("deserialization failed: " + e.ToString());
                }

                Logger.LogInformation("deserialized");

                OnDataReceived(parsed);
               

            }
        }

        private static string GetWebsocketMessage(ArraySegment<byte> buffer)
        {
            return Encoding.UTF8.GetString(buffer.Array, 0, buffer.Offset);
        }

        public void Stop()
        {
            this.IsRunning = false;
        }
    }
}
