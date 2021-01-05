using System.Collections.Generic;
using System.IO;
using Cult.MustacheSharp.Mustache;
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedMember.Global

// ReSharper disable All 
namespace Cult.MustacheSharp.Tags
{
    internal sealed class ReverseIndexTagDefinition : InlineTagDefinition
    {
        public ReverseIndexTagDefinition()
                    : base("reverseindex", true)
        {
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope contextScope)
        {
            if (contextScope.TryFind("reverseindex", out var reverseIndex))
            {
                writer.Write(reverseIndex);
            }
        }
    }
}
