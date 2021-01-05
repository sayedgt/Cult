using System;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    public class KeyNotFoundEventArgs : EventArgs
    {
        internal KeyNotFoundEventArgs(string key, string missingMember, bool isExtension)
        {
            Key = key;
            MissingMember = missingMember;
            IsExtension = isExtension;
        }

        public string Key { get; }

        public string MissingMember { get; }

        public bool IsExtension { get; }

        public bool Handled { get; set; }

        public object Substitute { get; set; }
    }
}
