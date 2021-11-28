namespace TradeMonitoringClient.Data
{
    //TODO share this data class with server

    /// <summary>
    /// Data containing info about security/position
    /// </summary>
    public class PositionData
    {
        public int Id { get; set; }

        public string Ticker { get; set; }

        /// <summary>
        /// Current price
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Qty [T-1]
        /// How many shares had at the start of the day
        /// </summary>
        public int DayStartQuantity { get; set; }

        /// <summary>
        /// Qty [T-0]
        /// How many shares have
        /// </summary>
        public int CurrentQuantity { get; set; }

        /// <summary>
        /// Qty Traded
        /// How many shares were sold/bought
        /// </summary>
        public int QuantityTraded { get; set; }

        /// <summary>
        /// Qty change
        /// Difference between live position and position at the start of the day
        /// </summary>
        public int ChangeToday { get; private set; }

        public string Color { get; private set; }

        /// <summary>
        /// Calculate and cache necessary data
        /// </summary>
        public void Recalculate()
        {
            ChangeToday = CurrentQuantity - DayStartQuantity;
            RecalculateColor();
        }

        /// <summary>
        /// Calculate color of change
        /// </summary>
        private void RecalculateColor()
        {
            Color = "black";
            if (ChangeToday > 0) Color = "green";
            if (ChangeToday < 0) Color = "red";

        }
    }
}
