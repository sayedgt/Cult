using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    public sealed class Generator
    {
        private readonly IGenerator _generator;
        private readonly List<EventHandler<KeyFoundEventArgs>> _foundHandlers = new List<EventHandler<KeyFoundEventArgs>>();
        private readonly List<EventHandler<KeyNotFoundEventArgs>> _notFoundHandlers = new List<EventHandler<KeyNotFoundEventArgs>>();
        private readonly List<EventHandler<ValueRequestEventArgs>> _valueRequestedHandlers = new List<EventHandler<ValueRequestEventArgs>>();

        internal Generator(IGenerator generator)
        {
            _generator = generator;
        }

        public event EventHandler<KeyFoundEventArgs> KeyFound
        {
            add { _foundHandlers.Add(value); }
            remove { _foundHandlers.Remove(value); }
        }

        public event EventHandler<KeyNotFoundEventArgs> KeyNotFound
        {
            add { _notFoundHandlers.Add(value); }
            remove { _notFoundHandlers.Remove(value); }
        }

        public event EventHandler<ValueRequestEventArgs> ValueRequested
        {
            add { _valueRequestedHandlers.Add(value); }
            remove { _valueRequestedHandlers.Remove(value); }
        }

        public event EventHandler<TagFormattedEventArgs> TagFormatted;

        public string Render(object source)
        {
            return render(CultureInfo.CurrentCulture, source);
        }

        public string Render(IFormatProvider provider, object source)
        {
            if (provider == null)
            {
                provider = CultureInfo.CurrentCulture;
            }
            return render(provider, source);
        }

        private string render(IFormatProvider provider, object source)
        {
            Scope keyScope = new Scope(source);
            Scope contextScope = new Scope(new Dictionary<string, object>());
            foreach (EventHandler<KeyFoundEventArgs> handler in _foundHandlers)
            {
                keyScope.KeyFound += handler;
                contextScope.KeyFound += handler;
            }
            foreach (EventHandler<KeyNotFoundEventArgs> handler in _notFoundHandlers)
            {
                keyScope.KeyNotFound += handler;
                contextScope.KeyNotFound += handler;
            }
            foreach (EventHandler<ValueRequestEventArgs> handler in _valueRequestedHandlers)
            {
                contextScope.ValueRequested += handler;
            }
            StringWriter writer = new StringWriter(provider);
            _generator.GetText(writer, keyScope, contextScope, postProcess);
            return writer.ToString();
        }

        private void postProcess(Substitution substitution)
        {
            if (TagFormatted == null)
            {
                return;
            }
            TagFormattedEventArgs args = new TagFormattedEventArgs(substitution.Key, substitution.Substitute, substitution.IsExtension);
            TagFormatted(this, args);
            substitution.Substitute = args.Substitute;
        }
    }
}
