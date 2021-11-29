using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TradeMonitoringClient.Data
{
    public class PositionTablePageModel
    {
        public PositionDataMessage Data;

        public string TimestampString { get; private set; }

        public PositionData[] SortedPositions { get; private set; }

        public PositionSortType CurrentSortType { get;  set; }

        public SortDirection CurrentSortDirection { get; set; }

        public ILogger Logger { get; set; }


        /// <summary>
        /// Run this when data in model change
        /// It will update page view
        /// </summary>
        public System.Action OnRecalculated { get; set; }

        public enum PositionSortType
        {
            Id,
            Ticker,
            Quantity,
            StartQuantity,
            QuantityTraded,
            Change
        }


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
            this.SortedPositions = Sort().ToArray();

            OnRecalculated?.Invoke();

        }

        /// <summary>
        /// Call this after receiving new data from server
        /// </summary>
        public void OnDataReceived(PositionDataMessage message)
        {
            this.Data = message;
            Recalculate();
        }


        private IEnumerable<PositionData> Sort()
        {
            Logger?.LogInformation("sorting...");
            IEnumerable<PositionData> result = null;
            var positions = Data.Positions;
            switch (this.CurrentSortType)
            {
                case PositionSortType.Id:
                    result = positions.OrderBy(x => x.Id);
                    break;
                case PositionSortType.Quantity:
                    result = positions.OrderBy(x => x.CurrentQuantity);
                    break;
                case PositionSortType.StartQuantity:
                    result = positions.OrderBy(x => x.DayStartQuantity);
                    break;
                case PositionSortType.QuantityTraded:
                    result = positions.OrderBy(x => x.QuantityTraded);
                    break;
                case PositionSortType.Change:
                    result = positions.OrderBy(x => x.ChangeToday);
                    break;
                case PositionSortType.Ticker:
                    result = positions.OrderBy(x => x.Ticker);
                    break;
                default:
                    throw new System.NotImplementedException("todo implement sort");
            }
            if(this.CurrentSortDirection== SortDirection.Ascending)
                return result;
            return result.Reverse();
        }

        private bool IsAscendingSort() => CurrentSortDirection == SortDirection.Ascending;

        /// <summary>
        /// Set sort and recalculate data
        /// </summary>
        public void SortBy(PositionSortType sortType)
        {
            //if clicked same column just switch direction
            if(sortType == CurrentSortType)
            {
                if (IsAscendingSort())
                    CurrentSortDirection = SortDirection.Descending;
                else CurrentSortDirection = SortDirection.Ascending;
            }
            else
            {
                CurrentSortType = sortType;
            }
            Logger?.LogInformation("current sort: " + CurrentSortType);
            Recalculate();
        }
    }

}
