using System;

namespace TradeMonitoringClient.Data
{
    /// <summary>
    /// Trade list
    /// This data is being send from the server
    /// </summary>
    public class TradeDataMessage
    {
        public DateTime Timestamp { get; set; }
        public TradeData[] Trades { get; set; }
    }

}
