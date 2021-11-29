using TradeMonitoringClient.Data;

namespace TradeMonitoringClient
{
    /// <summary>
    /// Receiving position list from server
    /// </summary>
    public class PositionTableService : WebsocketListenService<PositionDataMessage>
    {
        public PositionTableService()
        {
            this.Endpoint = "positions";
        }
    }
}
