

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
    public class PositionDataService
    {
        public bool IsRunning = false;

        CancellationTokenSource disposalTokenSource;

        ClientWebSocket webSocket;

        public Task WebSocketLoop { get; set; }

        public ILogger Logger { get; set; }

        public System.Action<PositionDataMessage> OnDataReceived { get; set; } = p => { };

        public async Task ConnectToServer()
        {
            if (webSocket != null) return;
            webSocket = new ClientWebSocket();
            disposalTokenSource =  new CancellationTokenSource();
            await webSocket.ConnectAsync(new Uri("ws://localhost:5295/position-list"), disposalTokenSource.Token);
            Logger.LogInformation("connected to server!");

        }

        public void StartLoop()
        { 
            if (this.WebSocketLoop != null) return;
            this.WebSocketLoop = ListenToWebsocket();
            Task.Run(()=>this.WebSocketLoop);
            IsRunning = true;
        }

        public async Task ListenToWebsocket()
        {
            Logger.LogInformation("start listening...");
            var buffer = new ArraySegment<byte>(new byte[1024]);
            //while (!disposalTokenSource.IsCancellationRequested)
            while (true)
            {
                Logger.LogInformation("waiting for new message...");
                var received = await webSocket.ReceiveAsync(buffer, disposalTokenSource.Token);
                var receivedAsText = Encoding.UTF8.GetString(buffer.Array, 0, received.Count);
                Logger.LogInformation("new message!:"+receivedAsText);
                var parsed = JsonSerializer.Deserialize<PositionDataMessage>(receivedAsText);
                OnDataReceived(parsed);
                Logger.LogInformation("loop to next...");
                
                
            }
        }
    }
}
