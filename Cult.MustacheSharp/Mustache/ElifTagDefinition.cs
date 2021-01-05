using System.Collections.Generic;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal sealed class ElifTagDefinition : ConditionTagDefinition
    {
        public ElifTagDefinition()
            : base("elif")
        {
        }

        protected override bool GetIsContextSensitive()
        {
            return true;
        }
        
        protected override IEnumerable<string> GetClosingTags()
        {
            return new string[] { "if" };
        }
    }
}
