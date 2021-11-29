using System;

namespace TradeMonitoringClient.Data
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Convert DateTime to string "HH:mm:ss"
        /// </summary>
        /// <returns></returns>
        public static string ToTimeOnly(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss");
        }
    }
}
