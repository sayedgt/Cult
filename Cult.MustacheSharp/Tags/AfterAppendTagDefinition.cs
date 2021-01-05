using System.Collections.Generic;
using System.IO;
using Cult.MustacheSharp.Mustache;
// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

// ReSharper disable All 
namespace Cult.MustacheSharp.Tags
{
    /// <summary>
    /// Defines a tag that outputs the current index within an each loop.
    /// </summary>
    internal sealed class AfterAppendTagDefinition : ContentTagDefinition
    {
        private const string afterAppend = "afterappendtext";
        private static readonly TagParameter afterAppendParameter = new TagParameter(afterAppend) { IsRequired = true };

        /// <summary>
        /// Initializes a new instance of an IndexTagDefinition.
        /// </summary>
        public AfterAppendTagDefinition()
                    : base("afterappend", true)
        {
        }

        public override IEnumerable<NestedContext> GetChildContext(TextWriter writer, Scope keyScope, Dictionary<string, object> arguments, Scope contextScope)
        {
            if (contextScope.TryFind("afterappend", out var appendable))
            {
                if (!string.Equals(appendable.ToString(), "true", System.StringComparison.InvariantCultureIgnoreCase))
                    return new List<NestedContext>();
                return base.GetChildContext(writer, keyScope, arguments, contextScope);
            }
            return base.GetChildContext(writer, keyScope, arguments, contextScope);

        }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            return new List<TagParameter>() { afterAppendParameter };
        }
    }
}
