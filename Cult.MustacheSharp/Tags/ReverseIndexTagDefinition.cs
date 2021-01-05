using System.Collections.Generic;
using System.IO;
using Cult.MustacheSharp.Mustache;
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedMember.Global

// ReSharper disable All 
namespace Cult.MustacheSharp.Tags
{
    /// <summary>
    /// Defines a tag that outputs the current index within an each loop.
    /// </summary>
    internal sealed class ReverseIndexTagDefinition : InlineTagDefinition
    {
        /// <summary>
        /// Initializes a new instance of an IndexTagDefinition.
        /// </summary>
        public ReverseIndexTagDefinition()
                    : base("reverseindex", true)
        {
        }

        /// <summary>
        /// Gets the text to output.
        /// </summary>
        /// <param name="writer">The writer to write the output to.</param>
        /// <param name="arguments">The arguments passed to the tag.</param>
        /// <param name="contextScope">Extra data passed along with the context.</param>
        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope contextScope)
        {
            if (contextScope.TryFind("reverseindex", out var reverseIndex))
            {
                writer.Write(reverseIndex);
            }
        }
    }
}
