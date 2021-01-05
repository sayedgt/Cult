using System;
using System.Collections.Generic;
using System.Globalization;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    public sealed class Scope
    {
        private readonly object _source;
        private readonly Scope _parent;

        internal Scope(object source)
            : this(source, null)
        {
        }

        internal Scope(object source, Scope parent)
        {
            _parent = parent;
            _source = source;
        }

        public event EventHandler<KeyFoundEventArgs> KeyFound;

        public event EventHandler<KeyNotFoundEventArgs> KeyNotFound;

        public event EventHandler<ValueRequestEventArgs> ValueRequested;

        public Scope CreateChildScope()
        {
            return CreateChildScope(new Dictionary<string, object>());
        }

        public Scope CreateChildScope(object source)
        {
            Scope scope = new Scope(source, this);
            scope.KeyFound = KeyFound;
            scope.KeyNotFound = KeyNotFound;
            scope.ValueRequested = ValueRequested;
            return scope;
        }

        internal object Find(string name, bool isExtension)
        {
            SearchResults results = tryFind(name);
            if (results.Found)
            {
                return onKeyFound(name, results.Value, isExtension);
            }
            if (onKeyNotFound(name, results.Member, isExtension, out object value))
            {
                return value;
            }
            string message = string.Format(CultureInfo.CurrentCulture, Resources.KeyNotFound, results.Member);
            throw new KeyNotFoundException(message);
        }

        private object onKeyFound(string name, object value, bool isExtension)
        {
            if (KeyFound == null)
            {
                return value;
            }
            KeyFoundEventArgs args = new KeyFoundEventArgs(name, value, isExtension);
            KeyFound(this, args);
            return args.Substitute;
        }

        private bool onKeyNotFound(string name, string member, bool isExtension, out object value)
        {
            if (KeyNotFound == null)
            {
                value = null;
                return false;
            }
            KeyNotFoundEventArgs args = new KeyNotFoundEventArgs(name, member, isExtension);
            KeyNotFound(this, args);
            if (!args.Handled)
            {
                value = null;
                return false;
            }
            value = args.Substitute;
            return true;
        }

        private static IDictionary<string, object> toLookup(object value)
        {
            IDictionary<string, object> lookup = UpcastDictionary.Create(value);
            if (lookup == null)
            {
                lookup = new PropertyDictionary(value);
            }
            return lookup;
        }

        internal void Set(string key)
        {
            SearchResults results = tryFind(key);
            if (ValueRequested == null)
            {
                set(results, results.Value);
                return;
            }

            ValueRequestEventArgs e = new ValueRequestEventArgs();
            if (results.Found)
            {
                e.Value = results.Value;
            }

            ValueRequested(this, e);
            set(results, e.Value);
        }

        internal void Set(string key, object value)
        {
            SearchResults results = tryFind(key);
            set(results, value);
        }

        private void set(SearchResults results, object value)
        {
            // handle setting value in child scope
            while (results.MemberIndex < results.Members.Length - 1)
            {
                Dictionary<string, object> context = new Dictionary<string, object>();
                results.Value = context;
                results.Lookup[results.Member] = results.Value;
                results.Lookup = context;
                ++results.MemberIndex;
            }
            results.Lookup[results.Member] = value;
        }

        public bool TryFind(string name, out object value)
        {
            SearchResults result = tryFind(name);
            value = result.Value;
            return result.Found;
        }

        private SearchResults tryFind(string name)
        {
            SearchResults results = new SearchResults();
            results.Members = name.Split('.');
            results.MemberIndex = 0;
            if (results.Member == "this")
            {
                results.Found = true;
                results.Lookup = toLookup(_source);
                results.Value = _source;
            }
            else
            {
                tryFindFirst(results);
            }
            for (int index = 1; results.Found && index < results.Members.Length; ++index)
            {
                results.Lookup = toLookup(results.Value);
                results.MemberIndex = index;
                results.Found = results.Lookup.TryGetValue(results.Member, out object value);
                results.Value = value;
            }
            return results;
        }

        private void tryFindFirst(SearchResults results)
        {
            results.Lookup = toLookup(_source);
            if (results.Lookup.TryGetValue(results.Member, out object value))
            {
                results.Found = true;
                results.Value = value;
                return;
            }
            if (_parent == null)
            {
                results.Found = false;
                results.Value = null;
                return;
            }
            _parent.tryFindFirst(results);
        }
    }

    internal class SearchResults
    {
        public IDictionary<string, object> Lookup { get; set; }

        public string[] Members { get; set; }

        public int MemberIndex { get; set; }

        public string Member { get { return Members[MemberIndex]; } }

        public bool Found { get; set; }

        public object Value { get; set; }
    }
}
