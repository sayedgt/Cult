using System;
using System.Security;
// ReSharper disable UnusedMember.Global

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    public sealed class HtmlFormatCompiler
    {
        private readonly FormatCompiler _compiler = new FormatCompiler();

        public HtmlFormatCompiler()
        {
            _compiler.AreExtensionTagsAllowed = true;
            _compiler.RemoveNewLines = true;
        }

        public event EventHandler<PlaceholderFoundEventArgs> PlaceholderFound
        {
            add => _compiler.PlaceholderFound += value;
            remove => _compiler.PlaceholderFound -= value;
        }

        public event EventHandler<VariableFoundEventArgs> VariableFound
        {
            add => _compiler.VariableFound += value;
            remove => _compiler.VariableFound -= value;
        }

        public void RegisterTag(TagDefinition definition, bool isTopLevel)
        {
            _compiler.RegisterTag(definition, isTopLevel);
        }

        public Generator Compile(string format)
        {
            Generator generator = _compiler.Compile(format);
            generator.TagFormatted += EscapeInvalidHtml;
            return generator;
        }

        private static void EscapeInvalidHtml(object sender, TagFormattedEventArgs e)
        {
            if (e.IsExtension)
            {
                // Do not escape text within triple curly braces
                return;
            }
            e.Substitute = SecurityElement.Escape(e.Substitute);
        }
    }
}
