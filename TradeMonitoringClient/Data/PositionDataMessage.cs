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

        public string TimestampString { get; private set; }



        /// <summary>
        /// Calculate and cache necessary data (position change, timestamp)
        /// </summary>
        public void Recalculate()
        {
            foreach (var p in Positions)
            {
                p.Recalculate();
            }
            TimestampString = Timestamp.ToString();

        }
    }

}
