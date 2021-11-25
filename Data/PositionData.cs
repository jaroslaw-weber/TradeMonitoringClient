using System;

namespace stock_price_app_client.Data
{
    /// <summary>
    /// Data containing info about security position
    /// </summary>
    public class PositionData
    {
        public int PositionId { get; set; }

        public string Ticker { get; set; }

        public int SpotPrice { get; set; }

        /// <summary>
        /// Qty [T-1]
        /// position at the start of the day
        /// </summary>
        public int DayStartPosition { get; set; }
        /// <summary>
        /// Qty [T-0]
        /// </summary>
        public int LivePosition { get; set; }
        /// <summary>
        /// Qty change
        /// Difference between live position and position at the start of the day
        /// </summary>
        public int ChangeToday { get; set; }
    }
}
