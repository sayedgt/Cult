using System;
using System.Collections.Generic;

namespace Cult.SimpleCacheManager
{
    public interface ICacheManager
    {
        object this[string key] { get; set; }
        Dictionary<string, object> CacheData();

        bool Contains(string key);

        int Count();

        object Get(string key);

        T Get<T>(string key);

        object GetOrSet(string key, Func<object> value);

        object GetOrSet(string key, object value);

        T GetOrSet<T>(string key, Func<T> value);

        T GetOrSet<T>(string key, object value);

        IEnumerable<string> Keys();

        bool Remove(string key);

        bool Remove(string key, out object value);

        void Set(string key, object value);

        IEnumerable<object> Values();
        void RemoveAll();
    }
}