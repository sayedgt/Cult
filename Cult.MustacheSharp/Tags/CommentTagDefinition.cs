using System.Collections.Generic;
using System.IO;
using Cult.MustacheSharp.Mustache;

namespace Cult.MustacheSharp.Tags
{
    public class CommentTagDefinition : ContentTagDefinition
    {
        private const string CommentText = "commenttext";
        private static readonly TagParameter CommentParameter = new TagParameter(CommentText) { IsRequired = true };

        /// <summary>
        /// Initializes a new instance of an IndexTagDefinition.
        /// </summary>
        public CommentTagDefinition()
                    : base("comment")
        {
        }

        public override IEnumerable<NestedContext> GetChildContext(TextWriter writer, Scope keyScope, Dictionary<string, object> arguments, Scope contextScope)
        {
            return new List<NestedContext>();
        }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            return new List<TagParameter>() { CommentParameter };
        }
    }
}
