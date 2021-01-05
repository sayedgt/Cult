using System.Collections;
using System.Collections.Generic;
using System.IO;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal sealed class EachTagDefinition : ContentTagDefinition
    {
        private const string collectionParameter = "collection";
        private static readonly TagParameter collection = new TagParameter(collectionParameter) { IsRequired = true };

        public EachTagDefinition()
            : base("each", true)
        {
        }

        protected override bool GetIsContextSensitive()
        {
            return false;
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            return new TagParameter[] { collection };
        }

        public override IEnumerable<NestedContext> GetChildContext(
            TextWriter writer, 
            Scope keyScope, 
            Dictionary<string, object> arguments,
            Scope contextScope)
        {
            object value = arguments[collectionParameter];
            if (!(value is IEnumerable enumerable))
            {
                yield break;
            }
            int index = 0;
            foreach (object item in enumerable)
            {
                NestedContext childContext = new NestedContext() 
                { 
                    KeyScope = keyScope.CreateChildScope(item), 
                    Writer = writer, 
                    ContextScope = contextScope.CreateChildScope(),
                };
                childContext.ContextScope.Set("index", index);
                yield return childContext;
                ++index;
            }
        }

        protected override IEnumerable<string> GetChildTags()
        {
            return new string[] { "index" };
        }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            return new TagParameter[] { collection };
        }
    }
}
