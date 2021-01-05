using System;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    public class TagFormattedEventArgs : EventArgs
    {
        internal TagFormattedEventArgs(string key, string value, bool isExtension)
        {
            Key = key;
            Substitute = value;
            IsExtension = isExtension;
        }

        public string Key { get; }

        public bool IsExtension { get; }

        public string Substitute { get; set; }
    }
}
