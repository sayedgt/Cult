using System;
using System.Collections.Generic;
using System.IO;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal sealed class CompoundGenerator : IGenerator
    {
        private readonly TagDefinition _definition;
        private readonly ArgumentCollection _arguments;
        private readonly List<IGenerator> _primaryGenerators = new List<IGenerator>();
        private IGenerator _subGenerator;

        public CompoundGenerator(TagDefinition definition, ArgumentCollection arguments)
        {
            _definition = definition;
            _arguments = arguments;
        }

        public void AddGenerator(IGenerator generator)
        {
            addGenerator(generator, false);
        }

        public void AddGenerator(TagDefinition definition, IGenerator generator)
        {
            bool isSubGenerator = _definition.ShouldCreateSecondaryGroup(definition);
            addGenerator(generator, isSubGenerator);
        }

        private void addGenerator(IGenerator generator, bool isSubGenerator)
        {
            if (isSubGenerator)
            {
                _subGenerator = generator;
            }
            else
            {
                _primaryGenerators.Add(generator);
            }
        }

        void IGenerator.GetText(TextWriter writer, Scope keyScope, Scope contextScope, Action<Substitution> postProcessor)
        {
            Dictionary<string, object> arguments = _arguments.GetArguments(keyScope, contextScope);
            IEnumerable<NestedContext> contexts = _definition.GetChildContext(writer, keyScope, arguments, contextScope);
            List<IGenerator> generators;
            if (_definition.ShouldGeneratePrimaryGroup(arguments))
            {
                generators = _primaryGenerators;
            }
            else
            {
                generators = new List<IGenerator>();
                if (_subGenerator != null)
                {
                    generators.Add(_subGenerator);
                }
            }
            foreach (NestedContext context in contexts)
            {
                foreach (IGenerator generator in generators)
                {
                    generator.GetText(context.Writer ?? writer, context.KeyScope ?? keyScope, context.ContextScope, postProcessor);
                }
                if (context.WriterNeedsConsidated)
                {
                    writer.Write(_definition.ConsolidateWriter(context.Writer ?? writer, arguments));
                }
            }
        }
    }
}