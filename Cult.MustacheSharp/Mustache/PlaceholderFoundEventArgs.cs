using System;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    public class PlaceholderFoundEventArgs : EventArgs
    {
        internal PlaceholderFoundEventArgs(string key, string alignment, string formatting, bool isExtension, Context[] context)
        {
            Key = key;
            Alignment = alignment;
            Formatting = formatting;
            Context = context;
        }

        public string Key { get; set; }

        public string Alignment { get; set; }

        public string Formatting { get; set; }

        public bool IsExtension { get; set; }

        public Context[] Context { get; }
    }
}
