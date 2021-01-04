using System;
using System.Globalization;
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedMember.Global

namespace Cult.Extensions
{
    public static class DateTimeExtensions
    {
        public static int Age(this DateTime @this)
        {
            if (DateTime.Today.Month < @this.Month ||
                DateTime.Today.Month == @this.Month &&
                DateTime.Today.Day < @this.Day)
            {
                return DateTime.Today.Year - @this.Year - 1;
            }
            return DateTime.Today.Year - @this.Year;
        }
        public static DateTime AddWeeks(this DateTime date, int value)
        {
            return date.AddDays(value * 7);
        }
        public static bool Between(this DateTime @this, DateTime minValue, DateTime maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        public static DateTime ConvertTime(this DateTime dateTime, TimeZoneInfo destinationTimeZone)
        {
            return TimeZoneInfo.ConvertTime(dateTime, destinationTimeZone);
        }

        public static DateTime ConvertTime(this DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
        {
            return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone);
        }

        public static DateTime ConvertTimeBySystemTimeZoneId(this DateTime dateTime, string destinationTimeZoneId)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, destinationTimeZoneId);
        }

        public static DateTime ConvertTimeBySystemTimeZoneId(this DateTime dateTime, string sourceTimeZoneId, string destinationTimeZoneId)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, sourceTimeZoneId, destinationTimeZoneId);
        }

        public static DateTime ConvertTimeFromUtc(this DateTime dateTime, TimeZoneInfo destinationTimeZone)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, destinationTimeZone);
        }

        public static DateTime ConvertTimeToUtc(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(dateTime);
        }

        public static DateTime ConvertTimeToUtc(this DateTime dateTime, TimeZoneInfo sourceTimeZone)
        {
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, sourceTimeZone);
        }

        public static TimeSpan Elapsed(this DateTime datetime)
        {
            return DateTime.Now - datetime;
        }

        public static DateTime EndOfDay(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day).AddDays(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }

        public static DateTime EndOfMonth(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, 1).AddMonths(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }

        public static DateTime EndOfWeek(this DateTime dt, DayOfWeek startDayOfWeek = DayOfWeek.Sunday)
        {
            var end = dt;
            var endDayOfWeek = startDayOfWeek - 1;
            if (endDayOfWeek < 0)
            {
                endDayOfWeek = DayOfWeek.Saturday;
            }

            if (end.DayOfWeek != endDayOfWeek)
            {
                end = endDayOfWeek < end.DayOfWeek ? end.AddDays(7 - (end.DayOfWeek - endDayOfWeek)) : end.AddDays(endDayOfWeek - end.DayOfWeek);
            }

            return new DateTime(end.Year, end.Month, end.Day, 23, 59, 59, 999);
        }

        public static DateTime EndOfYear(this DateTime @this)
        {
            return new DateTime(@this.Year, 1, 1).AddYears(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }

        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime FirstDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            var dt = date.FirstDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek)
                dt = dt.AddDays(1);
            return dt;
        }

        public static DateTime FirstDayOfWeek(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day).AddDays(-(int)@this.DayOfWeek);
        }

        public static DateTime FirstDayOfWeek(this DateTime date, CultureInfo cultureInfo)
        {
            cultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;

            var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            while (date.DayOfWeek != firstDayOfWeek)
                date = date.AddDays(-1);

            return date;
        }

        public static int GetCountDaysOfMonth(this DateTime date)
        {
            var nextMonth = date.AddMonths(1);
            return new DateTime(nextMonth.Year, nextMonth.Month, 1).AddDays(-1).Day;
        }

        public static bool In(this DateTime @this, params DateTime[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        public static bool InRange(this DateTime @this, DateTime minValue, DateTime maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        public static bool IsAfter(this DateTime source, DateTime other)
        {
            return source.CompareTo(other) > 0;
        }

        public static bool IsBefore(this DateTime source, DateTime other)
        {
            return source.CompareTo(other) < 0;
        }

        public static bool IsDateEqual(this DateTime date, DateTime dateToCompare)
        {
            return (date.Date == dateToCompare.Date);
        }

        public static bool IsDaylightSavingTime(this DateTime time, DateTime daylightTimes)
        {
            return TimeZoneInfo.Local.IsDaylightSavingTime(daylightTimes);
        }

        public static bool IsFuture(this DateTime @this)
        {
            return @this > DateTime.Now;
        }

        public static bool IsMorning(this DateTime @this)
        {
            return @this.TimeOfDay < new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;
        }

        public static bool IsNow(this DateTime @this)
        {
            return @this == DateTime.Now;
        }

        public static bool IsPast(this DateTime @this)
        {
            return @this < DateTime.Now;
        }

        public static bool IsTimeEqual(this DateTime time, DateTime timeToCompare)
        {
            return (time.TimeOfDay == timeToCompare.TimeOfDay);
        }

        public static bool IsToday(this DateTime @this)
        {
            return @this.Date == DateTime.Today;
        }

        public static bool IsWeekDay(this DateTime @this)
        {
            return !(@this.DayOfWeek == DayOfWeek.Saturday || @this.DayOfWeek == DayOfWeek.Sunday);
        }

        public static bool IsWeekend(this DateTime @this)
        {
            return (@this.DayOfWeek == DayOfWeek.Saturday || @this.DayOfWeek == DayOfWeek.Sunday);
        }

        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, GetCountDaysOfMonth(date));
        }

        public static DateTime LastDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            var dt = date.LastDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek)
                dt = dt.AddDays(-1);
            return dt;
        }

        public static DateTime LastDayOfWeek(this DateTime date)
        {
            return date.LastDayOfWeek(CultureInfo.CurrentCulture);
        }

        public static DateTime LastDayOfWeek(this DateTime date, CultureInfo cultureInfo)
        {
            return date.FirstDayOfWeek(cultureInfo).AddDays(6);
        }

        public static DateTime Midnight(this DateTime time)
        {
            return time.SetTime(0, 0, 0, 0);
        }

        public static double MillisecondsSince1970(this DateTime dt)
        {
            return dt.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        }

        public static DateTime NextWeekDay(this DateTime dt)
        {
            var dayOfWeek = dt.DayOfWeek;
            double daysToAdd = 1;

            if (dayOfWeek == DayOfWeek.Friday)
            {
                daysToAdd = 3;
            }
            else if (dayOfWeek == DayOfWeek.Saturday)
            {
                daysToAdd = 2;
            }

            return dt.AddDays(daysToAdd);
        }

        public static DateTime NextWeekDay(this DateTime date, DayOfWeek weekday)
        {
            while (date.DayOfWeek != weekday)
                date = date.AddDays(1);
            return date;
        }

        public static DateTime Noon(this DateTime time)
        {
            return time.SetTime(12, 0, 0);
        }

        public static bool NotIn(this DateTime @this, params DateTime[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }

        public static DateTime PreviousWeekDay(this DateTime dt)
        {
            var dayOfWeek = dt.DayOfWeek;
            double daysToAdd = -1;

            if (dayOfWeek == DayOfWeek.Monday)
            {
                daysToAdd = -3;
            }
            else if (dayOfWeek == DayOfWeek.Sunday)
            {
                daysToAdd = -2;
            }

            return dt.AddDays(daysToAdd);
        }

        public static DateTime PreviousWeekDay(this DateTime date, DayOfWeek weekday)
        {
            while (date.DayOfWeek != weekday)
                date = date.AddDays(-1);
            return date;
        }

        public static DateTime RoundToNearest(this DateTime dt, int minutes)
        {
            return dt.RoundToNearest(minutes.Minutes());
        }

        public static DateTime RoundToNearest(this DateTime dt, TimeSpan timeSpan)
        {
            var roundDown = false;
            var mod = dt.Ticks % timeSpan.Ticks;
            if (mod != 0 && mod < timeSpan.Ticks / 2)
            {
                roundDown = true;
            }

            var ticks = ((dt.Ticks + timeSpan.Ticks - 1) / timeSpan.Ticks - (roundDown ? 1 : 0)) * timeSpan.Ticks;

            var addTicks = ticks - dt.Ticks;

            return dt.AddTicks(addTicks);
        }

        public static DateTime SetTime(this DateTime current, int hour)
        {
            return SetTime(current, hour, 0, 0, 0);
        }

        public static DateTime SetTime(this DateTime current, int hour, int minute)
        {
            return SetTime(current, hour, minute, 0, 0);
        }

        public static DateTime SetTime(this DateTime current, int hour, int minute, int second)
        {
            return SetTime(current, hour, minute, second, 0);
        }

        public static DateTime SetTime(this DateTime current, int hour, int minute, int second, int millisecond)
        {
            return new DateTime(current.Year, current.Month, current.Day, hour, minute, second, millisecond);
        }

        public static DateTime StartOfDay(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day);
        }

        public static DateTime StartOfMonth(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, 1);
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startDayOfWeek = DayOfWeek.Sunday)
        {
            var start = new DateTime(dt.Year, dt.Month, dt.Day);

            if (start.DayOfWeek != startDayOfWeek)
            {
                var d = startDayOfWeek - start.DayOfWeek;
                if (startDayOfWeek <= start.DayOfWeek)
                {
                    return start.AddDays(d);
                }
                return start.AddDays(-7 + d);
            }

            return start;
        }

        public static DateTime StartOfYear(this DateTime @this)
        {
            return new DateTime(@this.Year, 1, 1);
        }

        public static string TimeStamp(this DateTime @this)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
        }

        public static DateTimeOffset ToDateTimeOffset(this DateTime localDateTime)
        {
            return localDateTime.ToDateTimeOffset(null);
        }

        public static DateTimeOffset ToDateTimeOffset(this DateTime localDateTime, TimeZoneInfo localTimeZone)
        {
            localTimeZone = localTimeZone ?? TimeZoneInfo.Local;

            if (localDateTime.Kind != DateTimeKind.Unspecified)
                localDateTime = new DateTime(localDateTime.Ticks, DateTimeKind.Unspecified);

            return TimeZoneInfo.ConvertTimeToUtc(localDateTime, localTimeZone);
        }

        public static TimeSpan ToEpochTimeSpan(this DateTime @this)
        {
            return @this.Subtract(new DateTime(1970, 1, 1));
        }

        public static string ToFullDateTimeString(this DateTime @this)
        {
            return @this.ToString("F", DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToFullDateTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("F", new CultureInfo(culture));
        }

        public static string ToFullDateTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("F", culture);
        }

        public static string ToLongDateShortTimeString(this DateTime @this)
        {
            return @this.ToString("f", DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToLongDateShortTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("f", new CultureInfo(culture));
        }

        public static string ToLongDateShortTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("f", culture);
        }

        public static string ToLongDateString(this DateTime @this)
        {
            return @this.ToString("D", DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToLongDateString(this DateTime @this, string culture)
        {
            return @this.ToString("D", new CultureInfo(culture));
        }

        public static string ToLongDateString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("D", culture);
        }

        public static string ToLongDateTimeString(this DateTime @this)
        {
            return @this.ToString("F", DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToLongDateTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("F", new CultureInfo(culture));
        }

        public static string ToLongDateTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("F", culture);
        }

        public static string ToLongTimeString(this DateTime @this)
        {
            return @this.ToString("T", DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToLongTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("T", new CultureInfo(culture));
        }

        public static string ToLongTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("T", culture);
        }

        public static string ToMonthDayString(this DateTime @this)
        {
            return @this.ToString("m", DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToMonthDayString(this DateTime @this, string culture)
        {
            return @this.ToString("m", new CultureInfo(culture));
        }

        public static string ToMonthDayString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("m", culture);
        }

        public static DateTime Tomorrow(this DateTime @this)
        {
            return @this.AddDays(1);
        }

        public static string ToReadableTime(this DateTime @this)
        {
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - @this.Ticks);
            var delta = ts.TotalSeconds;
            if (delta < 60)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 120)
            {
                return "a minute ago";
            }
            if (delta < 2700) // 45 * 60
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 5400) // 90 * 60
            {
                return "an hour ago";
            }
            if (delta < 86400) // 24 * 60 * 60
            {
                return ts.Hours + " hours ago";
            }
            if (delta < 172800) // 48 * 60 * 60
            {
                return "yesterday";
            }
            if (delta < 2592000) // 30 * 24 * 60 * 60
            {
                return ts.Days + " days ago";
            }
            if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
            {
                var months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }

        public static string ToRfc1123String(this DateTime @this)
        {
            return @this.ToString("r", DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToRfc1123String(this DateTime @this, string culture)
        {
            return @this.ToString("r", new CultureInfo(culture));
        }

        public static string ToRfc1123String(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("r", culture);
        }

        public static string ToShortDateLongTimeString(this DateTime @this)
        {
            return @this.ToString("G", DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToShortDateLongTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("G", new CultureInfo(culture));
        }

        public static string ToShortDateLongTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("G", culture);
        }

        public static string ToShortDateString(this DateTime @this)
        {
            return @this.ToString("d", DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToShortDateString(this DateTime @this, string culture)
        {
            return @this.ToString("d", new CultureInfo(culture));
        }

        public static string ToShortDateString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("d", culture);
        }

        public static string ToShortDateTimeString(this DateTime @this)
        {
            return @this.ToString("g", DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToShortDateTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("g", new CultureInfo(culture));
        }

        public static string ToShortDateTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("g", culture);
        }

        public static string ToShortTimeString(this DateTime @this)
        {
            return @this.ToString("t", DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToShortTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("t", new CultureInfo(culture));
        }

        public static string ToShortTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("t", culture);
        }

        public static string ToSortableDateTimeString(this DateTime @this)
        {
            return @this.ToString("s", DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToSortableDateTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("s", new CultureInfo(culture));
        }

        public static string ToSortableDateTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("s", culture);
        }

        public static string ToUniversalSortableDateTimeString(this DateTime @this)
        {
            return @this.ToString("u", DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToUniversalSortableDateTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("u", new CultureInfo(culture));
        }

        public static string ToUniversalSortableDateTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("u", culture);
        }

        public static string ToUniversalSortableLongDateTimeString(this DateTime @this)
        {
            return @this.ToString("U", DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToUniversalSortableLongDateTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("U", new CultureInfo(culture));
        }

        public static string ToUniversalSortableLongDateTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("U", culture);
        }

        public static long ToUnixEpoch(this DateTime dateTime)
        {
            return (long)dateTime.MillisecondsSince1970();
        }

        public static string ToYearMonthString(this DateTime @this)
        {
            return @this.ToString("y", DateTimeFormatInfo.CurrentInfo);
        }

        public static string ToYearMonthString(this DateTime @this, string culture)
        {
            return @this.ToString("y", new CultureInfo(culture));
        }

        public static string ToYearMonthString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("y", culture);
        }

        public static string UtcTimeStamp(this DateTime @this)
        {
            return DateTime.UtcNow.ToString("yyyyMMddHHmmssfffffff");
        }

        public static DateTime WeeksWeekDay(this DateTime date, DayOfWeek weekday)
        {
            return date.WeeksWeekDay(weekday, CultureInfo.CurrentCulture);
        }

        public static DateTime WeeksWeekDay(this DateTime date, DayOfWeek weekday, CultureInfo cultureInfo)
        {
            var firstDayOfWeek = date.FirstDayOfWeek(cultureInfo);
            return firstDayOfWeek.NextWeekDay(weekday);
        }

        public static DateTime Yesterday(this DateTime @this)
        {
            return @this.AddDays(-1);
        }

        public static int WeekOfYear(this DateTime dateTime, CultureInfo culture)
        {
            var calendar = culture.Calendar;
            var dateTimeFormat = culture.DateTimeFormat;

            return calendar.GetWeekOfYear(dateTime, dateTimeFormat.CalendarWeekRule, dateTimeFormat.FirstDayOfWeek);
        }

        public static int WeekOfYear(this DateTime dateTime)
        {
            return dateTime.WeekOfYear(CultureInfo.CurrentCulture);
        }
    }
}
