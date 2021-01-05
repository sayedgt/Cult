using System.Collections.Generic;
using System.IO;
using Cult.MustacheSharp.Mustache;

// ReSharper disable All 
namespace Cult.MustacheSharp.Tags
{
    public class TabTagDefinition : InlineTagDefinition
    {
        public TabTagDefinition()
                    : base("tab")
        {
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
            writer.Write("\t");
        }
    }
}
