﻿using System.Collections.Generic;
// ReSharper disable All 
namespace Cult.Extensions.ExtraHashSet
{
    public static class HashSetExtensions
    {
        public static bool AddRange<T>(this HashSet<T> @this, IEnumerable<T> items)
        {
            if (items == null) return false;
            var allAdded = true;
            foreach (var item in items)
            {
                allAdded &= @this.Add(item);
            }
            return allAdded;
        }
        public static bool AddRange<T>(this HashSet<T> @this, T[] items)
        {
            if (items == null) return false;
            var allAdded = true;
            foreach (var item in items)
            {
                allAdded &= @this.Add(item);
            }
            return allAdded;
        }
    }
}
