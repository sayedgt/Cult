using System;
using System.Collections;
using System.Collections.Generic;
// ReSharper disable All

namespace Cult.SimpleCacheManager
{
    public interface ICacheManager<TKey, TValue> : IEnumerable
    {
        TValue this[TKey key] { get; set; }
        Dictionary<TKey, TValue> CacheData();

        bool Contains(TKey key);

        int Count();

        TValue Get(TKey key);
        TValue Get(TKey key, out bool hasKey);

        TValue GetOrSet(TKey key, Func<TValue> value);

        TValue GetOrSet(TKey key, TValue value);

        IEnumerable<TKey> Keys();

        bool Remove(TKey key);

        bool Remove(TKey key, out TValue value);

        void Set(TKey key, TValue value);

        IEnumerable<TValue> Values();

        void RemoveAll();
    }
}