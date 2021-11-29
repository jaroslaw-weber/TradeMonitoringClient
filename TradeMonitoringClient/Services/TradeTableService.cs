using TradeMonitoringClient.Data;

namespace TradeMonitoringClient
{
    public class TradeTableService : WebsocketListenService<TradeDataMessage>
    {
        public TradeTableService()
        {
            this.Endpoint = "trades";
        }
    }
}
