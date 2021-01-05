using System;
using System.IO;
using System.Text;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal sealed class KeyGenerator : IGenerator
    {
        private readonly string _key;
        private readonly string _format;
        private readonly bool _isVariable;
        private readonly bool _isExtension;

        public KeyGenerator(string key, string alignment, string formatting, bool isExtension)
        {
            if (key.StartsWith("@"))
            {
                _key = key.Substring(1);
                _isVariable = true;
            }
            else
            {
                _key = key;
                _isVariable = false;
            }
            _format = getFormat(alignment, formatting);
            _isExtension = isExtension;
        }

        private static string getFormat(string alignment, string formatting)
        {
            StringBuilder formatBuilder = new StringBuilder();
            formatBuilder.Append("{0");
            if (!string.IsNullOrWhiteSpace(alignment))
            {
                formatBuilder.Append(",");
                formatBuilder.Append(alignment.TrimStart('+'));
            }
            if (!string.IsNullOrWhiteSpace(formatting))
            {
                formatBuilder.Append(":");
                formatBuilder.Append(formatting);
            }
            formatBuilder.Append("}");
            return formatBuilder.ToString();
        }

        void IGenerator.GetText(TextWriter writer, Scope scope, Scope context, Action<Substitution> postProcessor)
        {
            object value = _isVariable ? context.Find(_key, _isExtension) : scope.Find(_key, _isExtension);
            string result = string.Format(writer.FormatProvider, _format, value);
            Substitution substitution = new Substitution()
            {
                Key = _key,
                Substitute = result,
                IsExtension = _isExtension
            };
            postProcessor(substitution);
            writer.Write(substitution.Substitute);
        }
    }
}
