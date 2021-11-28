

using System;
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
    public abstract class WebsocketListenService<TMessage>
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
        private int port = 5295;

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
            Task.Run(() => this.ListenToWebsocket());
        }

        /// <summary>
        /// Listening to server messages through websockets
        /// </summary>

        public async Task ListenToWebsocket()
        {
            var buffer = GetBuffer();
            //while (!disposalTokenSource.IsCancellationRequested)
            while (IsRunning)
            {
                Logger.LogInformation("waiting for new message...");

                WebSocketReceiveResult received = await webSocket.ReceiveAsync(buffer, token.Token);

                string message = GetWebsocketMessage(buffer, received);

                Logger.LogInformation("new message!:" + message);

                var parsed = JsonSerializer.Deserialize<TMessage>(message);

                OnDataReceived(parsed);

            }
        }

        private static ArraySegment<byte> GetBuffer()
        {
            return new ArraySegment<byte>(new byte[1024]);
        }

        private static string GetWebsocketMessage(ArraySegment<byte> buffer, WebSocketReceiveResult received)
        {
            return Encoding.UTF8.GetString(buffer.Array, 0, received.Count);
        }

        public void Stop()
        {
            this.IsRunning = false;
        }
    }
}
