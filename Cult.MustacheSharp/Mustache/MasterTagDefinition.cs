using System.Collections.Generic;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal sealed class MasterTagDefinition : ContentTagDefinition
    {
        public MasterTagDefinition()
            : base(string.Empty, true)
        {
        }

        protected override bool GetIsContextSensitive()
        {
            return true;
        }

        protected override IEnumerable<string> GetClosingTags()
        {
            return new string[] { };
        }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            return new TagParameter[0];
        }
    }
}
