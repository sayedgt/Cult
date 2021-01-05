using System;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    public class ValueRequestEventArgs : EventArgs
    {
        public object Value { get; set; }
    }
}
