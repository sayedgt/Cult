using System.Collections.Generic;
using System.IO;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal sealed class WithTagDefinition : ContentTagDefinition
    {
        private const string contextParameter = "context";
        private static readonly TagParameter context = new TagParameter(contextParameter) { IsRequired = true };

        public WithTagDefinition()
            : base("with", true)
        {
        }

        protected override bool GetIsContextSensitive()
        {
            return false;
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            return new TagParameter[] { context };
        }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            return new TagParameter[] { context };
        }

        public override IEnumerable<NestedContext> GetChildContext(
            TextWriter writer, 
            Scope keyScope, 
            Dictionary<string, object> arguments,
            Scope contextScope)
        {
            object contextSource = arguments[contextParameter];
            yield return new NestedContext() 
            { 
                KeyScope = keyScope.CreateChildScope(contextSource), 
                Writer = writer,
                ContextScope = contextScope.CreateChildScope()
            };
        }
    }
}
