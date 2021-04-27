using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Cult.SimpleCacheManager
{
    public class CacheManager : ICacheManager
    {
        private readonly ConcurrentDictionary<string, object> _cache = new ConcurrentDictionary<string, object>();

        public object this[string key]
        {
            get => _cache[key];
            set => _cache[key] = value;
        }

        public bool Contains(string key) => _cache.ContainsKey(key);

        public int Count() => _cache.Count;

        public T Get<T>(string key) => !_cache.ContainsKey(key) ? default : (T) _cache[key];

        public object Get(string key) => !_cache.ContainsKey(key) ? null : _cache[key];

        public T GetOrSet<T>(string key, object value)
        {
            if (_cache.ContainsKey(key))
                return Get<T>(key);
            Set(key, value);
            return (T) value;
        }

        public T GetOrSet<T>(string key, Func<T> value) => GetOrSet<T>(key, value());

        public IEnumerable<string> Keys() => _cache.Keys;

        public bool Remove(string key) => _cache.TryRemove(key, out _);

        public bool Remove(string key, out object value) => _cache.TryRemove(key, out value);

        public void Set(string key, object value) => _cache.AddOrUpdate(key, value, (newKey, newValue) => value);

        public IEnumerable<object> Values() => _cache.Values;
        public void RemoveAll()
        {
            _cache.Clear();
        }

        public Dictionary<string, object> CacheData() => _cache.ToDictionary(entry => entry.Key, entry => entry.Value);

        public object GetOrSet(string key, Func<object> value) => GetOrSet<object>(key, value);

        public object GetOrSet(string key, object value) => GetOrSet<object>(key, value);
    }
}