using System;
// ReSharper disable All 
namespace Cult.Extensions.ExtraDateTimeOffset
{
    public static class DateTimeOffsetExtensions
    {
        public static bool Between(this DateTimeOffset @this, DateTimeOffset minValue, DateTimeOffset maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }
        public static DateTimeOffset ConvertTime(this DateTimeOffset dateTimeOffset, TimeZoneInfo destinationTimeZone)
        {
            return TimeZoneInfo.ConvertTime(dateTimeOffset, destinationTimeZone);
        }
        public static DateTimeOffset ConvertTimeBySystemTimeZoneId(this DateTimeOffset dateTimeOffset, string destinationTimeZoneId)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTimeOffset, destinationTimeZoneId);
        }
        public static bool In(this DateTimeOffset @this, params DateTimeOffset[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }
        public static bool InRange(this DateTimeOffset @this, DateTimeOffset minValue, DateTimeOffset maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }
        public static bool NotIn(this DateTimeOffset @this, params DateTimeOffset[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
    }
}
