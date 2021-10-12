using System;
using System.Diagnostics;
// ReSharper disable All 
namespace Cult.Toolkit
{
    public static class StopwatchUtility
    {
        public static TimeSpan GetExecutionTime(Action action)
        {
            var start = new Stopwatch();
            start.Start();
            action();
            start.Stop();
            return start.Elapsed;
        }
    }
}
