using System;

namespace TradeMonitoringClient.Data
{
    /// <summary>
    /// Position list
    /// This data is being send from the server
    /// </summary>
    public class PositionDataMessage
    {
        public DateTime Timestamp { get; set; }
        public PositionData[] Positions { get; set; }
    }

}
