using System.Collections.Generic;
using System.Linq;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal sealed class ArgumentCollection
    {
        private readonly Dictionary<TagParameter, IArgument> _argumentLookup = new Dictionary<TagParameter, IArgument>();

        public void AddArgument(TagParameter parameter, IArgument argument)
        {
            _argumentLookup.Add(parameter, argument);
        }

        public string GetKey(TagParameter parameter)
        {
            if (_argumentLookup.TryGetValue(parameter, out IArgument argument) && argument != null)
            {
                return argument.GetKey();
            }
            return null;
        }

        public Dictionary<string, object> GetArguments(Scope keyScope, Scope contextScope)
        {
            Dictionary<string, object> arguments = new Dictionary<string,object>();
            foreach (KeyValuePair<TagParameter, IArgument> pair in _argumentLookup)
            {
                object value;
                if (pair.Value == null)
                {
                    value = pair.Key.DefaultValue;
                }
                else
                {
                    value = pair.Value.GetValue(keyScope, contextScope);
                }
                arguments.Add(pair.Key.Name, value);
            }
            return arguments;
        }

        public Dictionary<string, object> GetArgumentKeyNames()
        {
            return _argumentLookup.ToDictionary(p => p.Key.Name, p => (object)GetKey(p.Key));
        }
    }
}
