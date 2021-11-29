using System;

namespace TradeMonitoringClient.Data
{
    /// <summary>
    /// Data of executed trades
    /// Warning: setters must be public for JSON to be deserialized
    /// </summary>
    public class TradeData
    {
        //TODO same as server code. reuse some data classes between server and client

        public int Id { get; set; }
        /// <summary>
        /// Ticker of the traded position
        /// </summary>
        public string Ticker { get; set; }
        /// <summary>
        /// How many shares is selling/buying
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Type of the trade (sell/buy)
        /// </summary>
        public TradeType TradeType { get; set; }

        public DateTime Timestamp { get; set; }

        public string TimestampString { get; private set; }

        public void Recalculate()
        {
            TimestampString = Timestamp.ToTimeOnly();
        }

    }
}
