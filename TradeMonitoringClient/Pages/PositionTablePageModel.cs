namespace TradeMonitoringClient.Data
{
    public class PositionTablePageModel
    {
        public PositionDataMessage Data;

        public string TimestampString { get; private set; }


        /// <summary>
        /// Calculate and cache necessary data (position change, timestamp)
        /// </summary>
        private void Recalculate()
        {
            foreach (var p in Data.Positions)
            {
                p.Recalculate();
            }
            TimestampString = Data.Timestamp.ToString();

        }

        public void OnDataReceived(PositionDataMessage message)
        {
            this.Data = message;
            Recalculate();
        }
    }

}
