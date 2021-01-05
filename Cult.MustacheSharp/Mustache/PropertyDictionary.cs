using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal sealed class PropertyDictionary : IDictionary<string, object>
    {
        private static readonly Dictionary<Type, Dictionary<string, Func<object, object>>> _cache = new Dictionary<Type, Dictionary<string, Func<object, object>>>();
        private readonly Dictionary<string, Func<object, object>> _typeCache;

        public PropertyDictionary(object instance)
        {
            Instance = instance;
            if (instance == null)
            {
                _typeCache = new Dictionary<string, Func<object, object>>();
            }
            else
            {
                lock (_cache)
                {
                    _typeCache = getCacheType(instance);
                }
            }
        }

        private static Dictionary<string, Func<object, object>> getCacheType(object instance)
        {
            Type type = instance.GetType();
            Dictionary<string, Func<object, object>> typeCache;
            if (!_cache.TryGetValue(type, out typeCache))
            {
                typeCache = new Dictionary<string, Func<object, object>>();
                
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;
                
                foreach (PropertyInfo propertyInfo in getMembers(type, type.GetProperties(flags).Where(p => !p.IsSpecialName)))
                {
                    typeCache.Add(propertyInfo.Name, i => propertyInfo.GetValue(i, null));
                }

                foreach (FieldInfo fieldInfo in getMembers(type, type.GetFields(flags).Where(f => !f.IsSpecialName)))
                {
                    typeCache.Add(fieldInfo.Name, i => fieldInfo.GetValue(i));
                }
                
                _cache.Add(type, typeCache);
            }
            return typeCache;
        }

        private static IEnumerable<TMember> getMembers<TMember>(Type type, IEnumerable<TMember> members)
            where TMember : MemberInfo
        {
            var singles = from member in members
                          group member by member.Name into nameGroup
                          where nameGroup.Count() == 1
                          from property in nameGroup
                          select property;
            var multiples = from member in members
                            group member by member.Name into nameGroup
                            where nameGroup.Count() > 1
                            select
                            (
                                from member in nameGroup
                                orderby getDistance(type, member)
                                select member
                            ).First();
            return singles.Concat(multiples);
        }

        private static int getDistance(Type type, MemberInfo memberInfo)
        {
            int distance = 0;
            for (; type != null && type != memberInfo.DeclaringType; type = type.BaseType)
            {
                ++distance;
            }
            return distance;
        }

        public object Instance { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void IDictionary<string, object>.Add(string key, object value)
        {
            throw new NotSupportedException();
        }

        public bool ContainsKey(string key)
        {
            return _typeCache.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return _typeCache.Keys; }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        bool IDictionary<string, object>.Remove(string key)
        {
            throw new NotSupportedException();
        }

        public bool TryGetValue(string key, out object value)
        {
            Func<object, object> getter;
            if (!_typeCache.TryGetValue(key, out getter))
            {
                value = null;
                return false;
            }
            value = getter(Instance);
            return true;
        }

        public ICollection<object> Values
        {
            get
            {
                ICollection<Func<object, object>> getters = _typeCache.Values;
                List<object> values = new List<object>();
                foreach (Func<object, object> getter in getters)
                {
                    object value = getter(Instance);
                    values.Add(value);
                }
                return values.AsReadOnly();
            }
        }

        public object this[string key]
        {
            get
            {
                Func<object, object> getter = _typeCache[key];
                return getter(Instance);
            }
            [EditorBrowsable(EditorBrowsableState.Never)]
            set
            {
                throw new NotSupportedException();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
        {
            throw new NotSupportedException();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void ICollection<KeyValuePair<string, object>>.Clear()
        {
            throw new NotSupportedException();
        }

        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            if (!_typeCache.TryGetValue(item.Key, out Func<object, object> getter))
            {
                return false;
            }
            object value = getter(Instance);
            return Equals(item.Value, value);
        }

        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            List<KeyValuePair<string, object>> pairs = new List<KeyValuePair<string, object>>();
            foreach (KeyValuePair<string, Func<object, object>> pair in _typeCache)
            {
                Func<object, object> getter = pair.Value;
                object value = getter(Instance);
                pairs.Add(new KeyValuePair<string, object>(pair.Key, value));
            }
            pairs.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _typeCache.Count; }
        }

        bool ICollection<KeyValuePair<string, object>>.IsReadOnly
        {
            get { return true; }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            throw new NotSupportedException();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (KeyValuePair<string, Func<object, object>> pair in _typeCache)
            {
                Func<object, object> getter = pair.Value;
                object value = getter(Instance);
                yield return new KeyValuePair<string, object>(pair.Key, value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}