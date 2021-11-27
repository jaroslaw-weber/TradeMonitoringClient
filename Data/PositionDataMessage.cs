using System;

namespace TradeMonitoringClient.Data
{
    public class PositionDataMessage
    {
        public DateTime Timestamp { get; set; }
        public PositionData[] Positions { get; set; }
    }
}
