using System;

namespace TradeMonitoringClient.Data
{
    /// <summary>
    /// Position list
    /// Message from server
    /// </summary>
    public class PositionDataMessage
    {
        
        public DateTime Timestamp { get; set; }
        public PositionData[] Positions { get; set; }
    }

}
