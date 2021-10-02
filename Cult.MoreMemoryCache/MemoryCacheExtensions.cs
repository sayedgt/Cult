﻿using System;
using System.Runtime.Caching;
using Microsoft.Extensions.Caching.Memory;

// ReSharper disable All 
namespace Cult.MoreMemoryCache
{
    public static class MemoryCacheExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(this ObjectCache @this, TKey key, Func<TKey, TValue> valueFactory, CacheItemPolicy policy)
        {
            var lazy = new Lazy<TValue>(() => valueFactory(key), true);
            return ((Lazy<TValue>)@this.AddOrGetExisting(key.ToString(), lazy, policy) ?? lazy).Value;
        }
        public static TReturn SafeGet<TReturn>(this MemoryCache memoryCache, string key, Func<TReturn> objectToCache)
        {
            if (memoryCache.Contains(key))
                return (TReturn)memoryCache[key];

            return (TReturn)(memoryCache[key] = objectToCache());
        }
        public static TReturn SafeGet<TReturn>(this MemoryCache memoryCache, string key, TReturn objectToCache)
        {
            if (memoryCache.Contains(key))
                return (TReturn)memoryCache[key];

            return (TReturn)(memoryCache[key] = objectToCache);
        }
        public static object SafeGet(this MemoryCache memoryCache, string key, Func<object> objectToCache)
        {
            if (memoryCache.Contains(key))
                return memoryCache[key];

            return memoryCache[key] = objectToCache();
        }
        public static object SafeGet(this MemoryCache memoryCache, string key, object objectToCache)
        {
            if (memoryCache.Contains(key))
                return memoryCache[key];

            return memoryCache[key] = objectToCache;
        }

        public static TReturn SafeGet<TReturn>(this IMemoryCache memoryCache, string key, Func<TReturn> objectToCache)
        {
            if (memoryCache.TryGetValue(key, out TReturn cached))
            {
                return cached;
            }
            else
            {
                TReturn data = objectToCache();
                memoryCache.Set(key, data);
                return data;
            }
        }
        public static TReturn SafeGet<TReturn>(this IMemoryCache memoryCache, string key, TReturn objectToCache)
        {
            if (memoryCache.TryGetValue(key, out TReturn cached))
            {
                return cached;
            }
            else
            {
                memoryCache.Set(key, objectToCache);
                return objectToCache;
            }
        }
        public static object SafeGet(this IMemoryCache memoryCache, string key, Func<object> objectToCache)
        {

            if (memoryCache.TryGetValue(key, out var cached))
            {
                return cached;
            }
            else
            {
                var data = objectToCache();
                memoryCache.Set(key, data);
                return data;
            }

        }
        public static object SafeGet(this IMemoryCache memoryCache, string key, object objectToCache)
        {
            if (memoryCache.TryGetValue(key, out var cached))
            {
                return cached;
            }
            else
            {
                memoryCache.Set(key, objectToCache);
                return objectToCache;
            }
        }
    }
}
