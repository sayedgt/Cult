using System;
using System.Runtime.Caching;
namespace Cult.MemoryCache
{
    public static class MemoryCacheExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(this ObjectCache @this, TKey key, Func<TKey, TValue> valueFactory, CacheItemPolicy policy)
        {
            var lazy = new Lazy<TValue>(() => valueFactory(key), true);
            return ((Lazy<TValue>)@this.AddOrGetExisting(key.ToString(), lazy, policy) ?? lazy).Value;
        }
        public static TReturn SafeGet<TReturn>(this System.Runtime.Caching.MemoryCache memoryCache, string key, Func<TReturn> objectToCache)
        {
            if (memoryCache.Contains(key))
                return (TReturn)memoryCache[key];

            return (TReturn)(memoryCache[key] = objectToCache());
        }
        public static TReturn SafeGet<TReturn>(this System.Runtime.Caching.MemoryCache memoryCache, string key, TReturn objectToCache)
        {
            if (memoryCache.Contains(key))
                return (TReturn)memoryCache[key];

            return (TReturn)(memoryCache[key] = objectToCache);
        }
        public static object SafeGet(this System.Runtime.Caching.MemoryCache memoryCache, string key, Func<object> objectToCache)
        {
            if (memoryCache.Contains(key))
                return memoryCache[key];

            return memoryCache[key] = objectToCache();
        }
        public static object SafeGet(this System.Runtime.Caching.MemoryCache memoryCache, string key, object objectToCache)
        {
            if (memoryCache.Contains(key))
                return memoryCache[key];

            return memoryCache[key] = objectToCache;
        }
    }
}
