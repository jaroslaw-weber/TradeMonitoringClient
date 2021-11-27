namespace TradeMonitoringClient.Data
{
    //TODO share this data class with server

    /// <summary>
    /// Data containing info about security position
    /// </summary>
    public class PositionData
    {
        public int Id { get; set; }

        public string Ticker { get; set; }

        public int Price { get; set; }

        /// <summary>
        /// Qty [T-1]
        /// position at the start of the day
        /// </summary>
        public int DayStartPosition { get; set; }
        /// <summary>
        /// Qty [T-0]
        /// </summary>
        public int CurrentQuantity { get; set; }
        /// <summary>
        /// Qty change
        /// Difference between live position and position at the start of the day
        /// </summary>
        public int ChangeToday { get; set; }
    }
}
