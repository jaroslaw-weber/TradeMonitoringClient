namespace TradeMonitoringClient.Data
{
    public enum TradeType
    {
        //TODO same as server code. reuse some data classes between server and client

        // Invalid type
        // Just in case field/variable is left with default value
        Invalid,
        /// <summary>
        /// Selling a position
        /// </summary>
        Sell,
        /// <summary>
        /// Buying a position
        /// </summary>
        Buy
    }
}
