using System.Collections.Generic;
using System.IO;
using Cult.MustacheSharp.Mustache;
// ReSharper disable UnusedMember.Global

namespace Cult.MustacheSharp.Tags
{
    /// <summary>
    /// Defines a tag that outputs the current index within an each loop.
    /// </summary>
    internal sealed class LengthTagDefinition : InlineTagDefinition
    {
        /// <summary>
        /// Initializes a new instance of an IndexTagDefinition.
        /// </summary>
        public LengthTagDefinition()
                    : base("length", true)
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
            if (contextScope.TryFind("length", out var length))
            {
                writer.Write(length);
            }
        }
    }
}
