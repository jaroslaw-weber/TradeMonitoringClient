

using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace stock_price_app_client.Data
{
    public class PositionDataService
    {
        CancellationTokenSource disposalTokenSource;

        ClientWebSocket webSocket;

        public Task WebSocketLoop { get; set; }

        public ILogger Logger { get; set; }

        public System.Action<PositionData[]> OnDataReceived { get; set; }

        public async Task ConnectToServer()
        {
            if (webSocket != null) return;
            webSocket = new ClientWebSocket();
            disposalTokenSource =  new CancellationTokenSource();
            await webSocket.ConnectAsync(new Uri("ws://localhost:5295/securities-list"), disposalTokenSource.Token);
            Logger.LogInformation("connected to server!");

        }

        public void StartLoop()
        { 
            if (this.WebSocketLoop != null) return;
            this.WebSocketLoop = ListenToWebsocket();
            Task.Run(()=>this.WebSocketLoop);
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
                var p = new PositionData();
                p.Ticker = receivedAsText;
                var l = new PositionData[] { p };
                OnDataReceived(l);
                Logger.LogInformation("loop to next...");
                
                
            }
        }
    }
}
