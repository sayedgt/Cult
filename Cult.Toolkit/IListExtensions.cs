using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable All 
namespace Cult.Toolkit.ExtraIList
{
    public static class IListExtensions
    {
        public static IList<K> Map<T, K>(this IList<T> list, Func<T, K> function)
        {
            var newList = new List<K>(list.Count);
            for (var i = 0; i < list.Count; ++i)
            {
                newList.Add(function(list[i]));
            }
            return newList;
        }
        public static bool IsNullOrEmpty<T>(this IList<T> toCheck)
        {
            return (toCheck == null) || (toCheck.Count <= 0);
        }
        public static void RemoveLast<T>(this IList<T> source, int n = 1)
        {
            for (int i = 0; i < n; i++)
            {
                source.RemoveAt(source.Count - 1);
            }
        }
        public static int BinarySearch<T>(this IList sortedList, T element, IComparer<T> comparer)
        {
            if (sortedList is null)
                throw new ArgumentNullException(nameof(sortedList));

            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            if (sortedList.Count <= 0)
                return -1;

            int left = 0;
            int right = sortedList.Count - 1;
            while (left <= right)
            {
                // determine the index in the list to compare with. This is the middle of the segment we're searching in.
                int index = left + (right - left) / 2;
                int compareResult = comparer.Compare((T)sortedList[index], element);
                if (compareResult == 0)
                {
                    // found it, done. Return the index
                    return index;
                }
                if (compareResult < 0)
                {
                    // element is bigger than the element at index, so we can skip all elements at the left of index including the element at index.
                    left = index + 1;
                }
                else
                {
                    // element is smaller than the element at index, so we can skip all elements at the right of index including the element at index.
                    right = index - 1;
                }
            }
            return ~left;
        }
        public static void AddRange<T>(this IList<T> container, IEnumerable<T> rangeToAdd)
        {
            if ((container == null) || (rangeToAdd == null))
            {
                return;
            }
            foreach (T toAdd in rangeToAdd)
            {
                container.Add(toAdd);
            }
        }
        public static void SwapValues<T>(this IList<T> source, int indexA, int indexB)
        {
            if ((indexA < 0) || (indexA >= source.Count))
            {
                throw new IndexOutOfRangeException("indexA is out of range");
            }
            if ((indexB < 0) || (indexB >= source.Count))
            {
                throw new IndexOutOfRangeException("indexB is out of range");
            }

            if (indexA == indexB)
            {
                return;
            }

            T tempValue = source[indexA];
            source[indexA] = source[indexB];
            source[indexB] = tempValue;
        }
        public static List<List<T>> Split<T>(this IList<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
        public static void AddToFront<T>(this IList<T> list, T item)
        {
            list.Insert(0, item);
        }
        public static void AddDistinct<T>(this IList<T> list, T item) where T : class
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }
        public static void AddDistinctRange<T>(this IList<T> list, T[] items) where T : class
        {
            foreach (var item in items)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
        }
        public static void AddDistinctRange<T>(this IList<T> list, IEnumerable<T> items) where T : class
        {
            foreach (var item in items)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
        }
       
        public static int IndexOf<T>(this IList<T> list, Func<T, bool> comparison)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (comparison(list[i]))
                    return i;
            }
            return -1;
        }
        public static bool IsEmpty(this IList collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            return collection.Count == 0;
        }
        public static bool IsEmpty<T>(this IList<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            return collection.Count == 0;
        }
        public static bool IsFirst<T>(this IList<T> list, T element)
        {
            return list.IndexOf(element) == 0;
        }
        public static bool IsLast<T>(this IList<T> list, T element)
        {
            return list.IndexOf(element) == list.Count - 1;
        }
        public static T OneOf<T>(this IList<T> list)
        {
            var rng = new Random();
            return list[rng.Next(list.Count)];
        }
        public static T OneOf<T>(this T[] list)
        {
            var rng = new Random();
            return list[rng.Next(list.Length)];
        }
        public static void Replace<T>(this IList<T> @this, T oldValue, T newValue)
        {
            var oldIndex = @this.IndexOf(oldValue);
            while (oldIndex > 0)
            {
                @this.RemoveAt(oldIndex);
                @this.Insert(oldIndex, newValue);
                oldIndex = @this.IndexOf(oldValue);
            }
        }
        public static void Shuffle<T>(this IList<T> list)
        {
            var rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static void Swap<T>(this IList<T> source, int indexA, int indexB)
        {
            if (indexA < 0 || indexA >= source.Count)
            {
                throw new IndexOutOfRangeException("indexA is out of range");
            }
            if (indexB < 0 || indexB >= source.Count)
            {
                throw new IndexOutOfRangeException("indexB is out of range");
            }

            if (indexA == indexB)
            {
                return;
            }

            var tempValue = source[indexA];
            source[indexA] = source[indexB];
            source[indexB] = tempValue;
        }

        public static IEnumerable<T> TakeLast<T>(this IList<T> list, int n)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (list.Count - n < 0)
            {
                n = list.Count;
            }

            for (var i = list.Count - n; i < list.Count; i++)
            {
                yield return list[i];
            }
        }
        public static bool All<T>(this IList<T> list, Func<T, bool> predicate)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (!predicate(list[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Any<T>(this IList<T> list, Func<T, bool> predicate)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static T First<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                return default;
            }
            else
            {
                return list[0];
            }
        }

        public static T Last<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                return default;
            }
            else
            {
                return list[list.Count - 1];
            }
        }
        public static void RemoveFirst<T>(this IList<T> list)
        {
            if (list.Count > 0)
            {
                list.RemoveAt(0);
            }
        }
    }
}
