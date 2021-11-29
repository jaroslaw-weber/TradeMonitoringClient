using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TradeMonitoringClient.Data
{
    public class TradeTablePageModel
    {
        public TradeDataMessage Data;

        public string TimestampString { get; private set; }

        public ILogger Logger { get; set; }

        /// <summary>
        /// Run this when data in model change
        /// It will update page view
        /// </summary>
        public System.Action OnRecalculated { get; set; }

        /// <summary>
        /// Calculate and cache necessary data (position change, timestamp)
        /// </summary>
        private void Recalculate()
        {
            TimestampString = Data.Timestamp.ToString();
            foreach(var trade in Data.Trades)
            {
                trade.Recalculate();
            }
            Logger.LogInformation("on recalculated");
            OnRecalculated?.Invoke();

        }

        /// <summary>
        /// Call this after receiving new data from server
        /// </summary>
        public void OnDataReceived(TradeDataMessage message)
        {
            Logger.LogInformation("on data received");
            this.Data = message;
            Recalculate();
        }


    }

}
