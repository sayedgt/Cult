using System.Diagnostics;
// ReSharper disable All

namespace Cult.Extensions.ExtraStopwatch
{
    public static class StopwatchExtensions
    {
        public static long ElapsedSeconds(this Stopwatch sw)
        {
            return sw.ElapsedMilliseconds / 1000;
        }
    }
}