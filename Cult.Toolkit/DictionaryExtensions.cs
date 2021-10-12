using System.Collections.Generic;

namespace Cult.Toolkit.ExtraDictionary
{
    public static class DictionaryExtensions
    {
        public static SortedDictionary<TKey, TValue> Sort<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            return new SortedDictionary<TKey, TValue>(dict);
        }

        public static SortedDictionary<TKey, TValue> Sort<TKey, TValue>(this Dictionary<TKey, TValue> dict, IComparer<TKey> comparer)
        {
            return new SortedDictionary<TKey, TValue>(dict, comparer);
        }
    }
}
