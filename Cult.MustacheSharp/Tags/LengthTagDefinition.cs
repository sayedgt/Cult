using System.Collections.Generic;
using System.IO;
using Cult.MustacheSharp.Mustache;
// ReSharper disable UnusedMember.Global

// ReSharper disable All 
namespace Cult.MustacheSharp.Tags
{
    internal sealed class LengthTagDefinition : InlineTagDefinition
    {
        public LengthTagDefinition()
                    : base("length", true)
        {
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope contextScope)
        {
            if (contextScope.TryFind("length", out var length))
            {
                writer.Write(length);
            }
        }
    }
}
