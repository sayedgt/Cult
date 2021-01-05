using System.Collections.Generic;
using System.IO;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal sealed class SetTagDefinition : InlineTagDefinition
    {
        private const string nameParameter = "name";
        private static readonly TagParameter name = new TagParameter(nameParameter) { IsRequired = true };

        public SetTagDefinition()
            : base("set", true)
        {
        }

        protected override bool GetIsSetter()
        {
            return true;
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            return new TagParameter[] { name };
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope contextScope)
        {
            string name = (string)arguments[nameParameter];
            contextScope.Set(name);
        }
    }
}
