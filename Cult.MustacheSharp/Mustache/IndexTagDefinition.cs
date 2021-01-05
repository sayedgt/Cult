using System.Collections.Generic;
using System.IO;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal sealed class IndexTagDefinition : InlineTagDefinition
    {
        public IndexTagDefinition()
            : base("index", true)
        {
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope contextScope)
        {
            if (contextScope.TryFind("index", out object index))
            {
                writer.Write(index);
            }
        }
    }
}
