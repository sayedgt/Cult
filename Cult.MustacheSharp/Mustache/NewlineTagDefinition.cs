using System;
using System.Collections.Generic;
using System.IO;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal sealed class NewlineTagDefinition : InlineTagDefinition
    {
        public NewlineTagDefinition()
            : base("newline")
        {
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
            writer.Write(Environment.NewLine);
        }
    }
}
