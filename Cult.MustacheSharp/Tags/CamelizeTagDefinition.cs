using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Cult.MustacheSharp.Mustache;
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

// ReSharper disable All 
namespace Cult.MustacheSharp.Tags
{
    public class CamelizeTagDefinition : InlineTagDefinition
    {
        public CamelizeTagDefinition()
                    : base("camelize")
        {
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            return new[] { new TagParameter("param") { IsRequired = true } };
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
            writer.Write(ToCamelCase(arguments["param"].ToString()));
        }

        private string ToCamelCase(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            var x = str.Replace("_", "");
            x = Regex.Replace(x, "([A-Z])([A-Z]+)($|[A-Z])",
                m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);
            return char.ToUpper(x[0]) + x.Substring(1);
        }
    }
}
