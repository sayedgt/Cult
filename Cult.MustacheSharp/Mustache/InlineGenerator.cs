using System;
using System.Collections.Generic;
using System.IO;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal sealed class InlineGenerator : IGenerator
    {
        private readonly TagDefinition _definition;
        private readonly ArgumentCollection _arguments;

        public InlineGenerator(TagDefinition definition, ArgumentCollection arguments)
        {
            _definition = definition;
            _arguments = arguments;
        }

        void IGenerator.GetText(TextWriter writer, Scope scope, Scope context, Action<Substitution> postProcessor)
        {
            var arguments = GetArguments(scope, context);
            _definition.GetText(writer, arguments, context);
        }

        private Dictionary<string, object> GetArguments(Scope scope, Scope context)
        {
            if (_definition.IsSetter)
            {
                return _arguments.GetArgumentKeyNames();
            }
            else
            {
                return _arguments.GetArguments(scope, context);
            }
        }
    }
}
