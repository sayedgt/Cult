using System;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    public class VariableFoundEventArgs : EventArgs
    {
        internal VariableFoundEventArgs(string name, string alignment, string formatting, bool isExtension, Context[] context)
        {
            Name = name;
            Alignment = alignment;
            Formatting = formatting;
            IsExtension = isExtension;
            Context = context;
        }

        public string Name { get; set; }

        public string Alignment { get; set; }

        public string Formatting { get; set; }

        public bool IsExtension { get; set; }

        public Context[] Context { get; }
    }
}
