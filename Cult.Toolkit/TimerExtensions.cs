﻿using System;
using System.Timers;
// ReSharper disable All 
namespace Cult.Toolkit.ExtraTimer
{
    public static class TimerExtensions
    {
        public static void Action(this Timer timer, double interval, Action action)
        {
            timer.Elapsed += (sender, e) => action();
            timer.Interval = interval;
            timer.Enabled = true;
        }
        public static void Action(this Timer timer, double interval, Action<object, ElapsedEventArgs> action)
        {
            timer.Elapsed += (sender, e) => action(sender, e);
            timer.Interval = interval;
            timer.Enabled = true;
        }
    }
}
