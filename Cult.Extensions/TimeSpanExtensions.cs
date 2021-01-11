using System;
// ReSharper disable All 
namespace Cult.Extensions.ExtraTimeSpan
{
    public static class TimeSpanExtensions
    {
        public static DateTime Ago(this TimeSpan @this)
        {
            return DateTime.Now.Subtract(@this);
        }
        public static DateTime FromNow(this TimeSpan @this)
        {
            return DateTime.Now.Add(@this);
        }
        public static DateTime UtcAgo(this TimeSpan @this)
        {
            return DateTime.UtcNow.Subtract(@this);
        }
        public static DateTime UtcFromNow(this TimeSpan @this)
        {
            return DateTime.UtcNow.Add(@this);
        }
    }
}
