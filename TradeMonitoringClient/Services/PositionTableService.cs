using TradeMonitoringClient.Data;

/// <summary>
/// Receiving position list from server
/// </summary>
public class PositionTableService : WebsocketListenService<PositionDataMessage>
{
    public PositionTableService()
    {
        this.Endpoint = "position-list";
    }
}
