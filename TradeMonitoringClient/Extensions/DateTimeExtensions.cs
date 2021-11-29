using System;

namespace TradeMonitoringClient.Data
{
    public static class DateTimeExtensions
    {
        public static string ToTimeOnly(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss");
        }
    }
}
