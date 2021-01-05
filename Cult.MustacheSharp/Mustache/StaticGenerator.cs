using System;
using System.IO;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal sealed class StaticGenerator : IGenerator
    {
        public StaticGenerator(string value, bool removeNewLines)
        {
            if (removeNewLines)
            {
                Value = value.Replace(Environment.NewLine, string.Empty);
            }
            else
            {
                Value = value;
            }
        }

        public string Value { get; }

        void IGenerator.GetText(TextWriter writer, Scope scope, Scope context, Action<Substitution> postProcessor)
        {
            writer.Write(Value);
        }
    }
}
