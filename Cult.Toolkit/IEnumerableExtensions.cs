using Cult.Toolkit.ExtraString;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

// ReSharper disable All
namespace Cult.Toolkit.ExtraIEnumerable
{
    public static class IEnumerableExtensions
    {
        public static bool Contains<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            return collection.Count(predicate) > 0;
        }
        public static IEnumerable<string> ContainsFuzzy(this IEnumerable<string> text, string search)
        {
            return text.Where(x => FuzzyMatcher.FuzzyMatch(search, x));
        }
        public static IEnumerable<string> IsLike(this IEnumerable<string> @this, string pattern)
        {
            return @this.Where(x => x.IsLike(pattern));
        }
        public static BigInteger BigIntCount<T>(this IEnumerable<T> source)
        {
            BigInteger count = 0;
            foreach (var item in source)
            {
                count++;
            }
            return count;
        }
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int chunkSize)
        {
            var count = 0;
            var chunk = new List<T>(chunkSize);
            foreach (var item in source)
            {
                chunk.Add(item);
                count++;
                if (count == chunkSize)
                {
                    yield return chunk.AsEnumerable();
                    chunk = new List<T>(chunkSize);
                    count = 0;
                }
            }
            if (count > 0)
            {
                yield return chunk.AsEnumerable();
            }
        }
        public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> groupings)
        {
            return groupings.ToDictionary(group => group.Key, group => group.ToList());
        }
        public static Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>> Pivot<TSource, TFirstKey, TSecondKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TFirstKey> firstKeySelector, Func<TSource, TSecondKey> secondKeySelector, Func<IEnumerable<TSource>, TValue> aggregate)
        {
            var retVal = new Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>();

            var l = source.ToLookup(firstKeySelector);
            foreach (var item in l)
            {
                var dict = new Dictionary<TSecondKey, TValue>();
                retVal.Add(item.Key, dict);
                var subdict = item.ToLookup(secondKeySelector);
                foreach (var subitem in subdict)
                {
                    dict.Add(subitem.Key, aggregate(subitem));
                }
            }

            return retVal;
        }
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T element)
        {
            foreach (var item in source)
            {
                yield return item;
            }
            yield return element;
        }
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, T element)
        {
            yield return element;
            foreach (var item in source)
            {
                yield return item;
            }
        }

        public static bool ContainsAll<T>(this IEnumerable<T> @this, params T[] values)
        {
            T[] list = @this.ToArray();
            foreach (T value in values)
            {
                if (!list.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }
        public static bool ContainsAny<T>(this IEnumerable<T> @this, params T[] values)
        {
            T[] list = @this.ToArray();
            foreach (T value in values)
            {
                if (list.Contains(value))
                {
                    return true;
                }
            }

            return false;
        }
        public static void Delete(this IEnumerable<FileInfo> @this)
        {
            foreach (FileInfo t in @this)
            {
                t.Delete();
            }
        }
        public static void Delete(this IEnumerable<DirectoryInfo> @this)
        {
            foreach (DirectoryInfo t in @this)
            {
                t.Delete();
            }
        }
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }
        public static IEnumerable<FileInfo> ForEach(this IEnumerable<FileInfo> @this, Action<FileInfo> action)
        {
            foreach (FileInfo t in @this)
            {
                action(t);
            }
            return @this;
        }
        public static IEnumerable<DirectoryInfo> ForEach(this IEnumerable<DirectoryInfo> @this, Action<DirectoryInfo> action)
        {
            foreach (DirectoryInfo t in @this)
            {
                action(t);
            }
            return @this;
        }
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action, Func<T, bool> predicate)
        {
            foreach (var item in source.Where(predicate))
                action(item);
        }
        public static void ForEachReverse<T>(this IEnumerable<T> source, Action<T> action)
        {
            var items = source.ToList();
            for (int i = items.Count - 1; i >= 0; i--)
            {
                action(items[i]);
            }
        }
        public static void ForEachReverse<T>(this IEnumerable<T> source, Action<T> action, Func<T, bool> predicate)
        {
            var items = source.ToList();
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (predicate(items[i]))
                    action(items[i]);
            }
        }
        public static IEnumerable<T> GetDuplicateItems<T>(this IEnumerable<T> @this)
        {
            HashSet<T> hashset = new HashSet<T>();
            return @this.Where(e => !hashset.Add(e));
        }
        public static IEnumerable<IEnumerable<T>> GetKCombinations<T>(this IEnumerable<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new[] { t });
            return GetKCombinations(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }
        public static IEnumerable<IEnumerable<T>> GetKCombinationsWithRepetition<T>(this IEnumerable<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new[] { t });
            return GetKCombinationsWithRepetition(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) >= 0),
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }
        public static IEnumerable<IEnumerable<T>> GetPermutationsWithRepetition<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new[] { t });
            return GetPermutationsWithRepetition(list, length - 1)
                .SelectMany(_ => list,
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }
        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> collection, Func<T, bool> endCondition)
        {
            return collection.TakeWhile(item => !endCondition(item));
        }
        public static IEnumerable<T> Concat<T>(IEnumerable<T> target, T element)
        {
            foreach (T e in target) yield return e;
            yield return element;
        }
        public static bool XOf<T>(this IEnumerable<T> source, int count)
        {
            return source.Count() == count;
        }
        public static bool XOf<T>(this IEnumerable<T> source, Func<T, bool> query, int count)
        {
            return source.Count(query) == count;
        }
        public static bool Many<T>(this IEnumerable<T> source)
        {
            return source.Count() > 1;
        }
        public static bool Many<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Count(query) > 1;
        }
        public static bool None<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }
        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return !source.Any(query);
        }
        public static bool OneOf<T>(this IEnumerable<T> source)
        {
            return source.Count() == 1;
        }
        public static bool OneOf<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Count(query) == 1;
        }
        public static bool IsEmpty(this IEnumerable collection)
        {
            foreach (var _ in collection)
            {
                return false;
            }
            return true;
        }
        public static bool IsEmpty<T>(this IEnumerable<T> @this) => !@this.Any();
        public static bool IsEqual<T>(this IEnumerable<T> source, IEnumerable<T> toCompareWith)
        {
            if ((source == null) || (toCompareWith == null))
            {
                return false;
            }
            return source.IsEqual(toCompareWith, null);
        }
        public static bool IsEqual<T>(this IEnumerable<T> source, IEnumerable<T> toCompareWith,
                                        IEqualityComparer<T> comparer)
        {
            if ((source == null) || (toCompareWith == null))
            {
                return false;
            }
            var countSource = source.Count();
            var countToCompareWith = toCompareWith.Count();
            if (countSource != countToCompareWith)
            {
                return false;
            }
            if (countSource == 0)
            {
                return true;
            }

            var comparerToUse = comparer ?? EqualityComparer<T>.Default;
            // check whether the intersection of both sequences has the same number of elements
            return source.Intersect(toCompareWith, comparerToUse).Count() == countSource;
        }
        public static bool IsNotEmpty<T>(this IEnumerable<T> @this)
        {
            return @this.Any();
        }
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> @this)
        {
            return @this != null && @this.Any();
        }
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> @this)
        {
            return @this == null || !@this.Any();
        }
        public static IEnumerable<T> MergeDistinctInnerEnumerable<T>(this IEnumerable<IEnumerable<T>> @this)
        {
            List<IEnumerable<T>> listItem = @this.ToList();

            var list = new List<T>();

            foreach (var item in listItem)
            {
                list = list.Union(item).ToList();
            }

            return list;
        }
        public static IEnumerable<T> MergeInnerEnumerable<T>(this IEnumerable<IEnumerable<T>> @this)
        {
            List<IEnumerable<T>> listItem = @this.ToList();

            var list = new List<T>();

            foreach (var item in listItem)
            {
                list.AddRange(item);
            }

            return list;
        }
        public static string PathCombine(this IEnumerable<string> @this)
        {
            return Path.Combine(@this.ToArray());
        }
        public static string StringJoin<T>(this IEnumerable<T> @this, string separator)
        {
            return string.Join(separator, @this);
        }
        public static string StringJoin<T>(this IEnumerable<T> @this, char separator)
        {
            return string.Join(separator.ToString(), @this);
        }
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> list, int n)
        {
            return list.ToList().TakeLast(n);
        }
        public static Collection<T> ToCollection<T>(this IEnumerable<T> enumerable)
        {
            var collection = new Collection<T>();
            foreach (var i in enumerable)
                collection.Add(i);
            return collection;
        }
        public static string ToCsv<T>(this IEnumerable<T> items, char separator = ',', bool trim = true)
        {
            var list = trim ? items.Select(x => x.ToString().Trim()) : items.Select(x => x.ToString());
            return list.Aggregate((a, b) => a + separator + b);
        }
        public static HashSet<TDestination> ToHashSet<TDestination>(this IEnumerable<TDestination> source)
        {
            return new HashSet<TDestination>(source);
        }
        public static IEnumerable<T> ToPaged<T>(this IEnumerable<T> query, int pageIndex, int pageSize)
        {
            return query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                ;
        }

        public static bool SetEqual<T>(this IEnumerable<T> source, IEnumerable<T> toCompareWith)
        {
            if ((source == null) || (toCompareWith == null))
            {
                return false;
            }
            return source.SetEqual(toCompareWith, null);
        }

        public static bool SetEqual<T>(this IEnumerable<T> source, IEnumerable<T> toCompareWith, IEqualityComparer<T> comparer)
        {
            if ((source == null) || (toCompareWith == null))
            {
                return false;
            }
            int countSource = source.Count();
            int countToCompareWith = toCompareWith.Count();
            if (countSource != countToCompareWith)
            {
                return false;
            }
            if (countSource == 0)
            {
                return true;
            }

            IEqualityComparer<T> comparerToUse = comparer ?? EqualityComparer<T>.Default;
            return source.Intersect(toCompareWith, comparerToUse).Count() == countSource;
        }

        public static ReadOnlyCollection<TDestination> ToReadOnlyCollection<TDestination>(this IEnumerable source)
        {
            var sourceAsDestination = new List<TDestination>();
            if (source != null)
            {
                foreach (TDestination toAdd in source)
                {
                    sourceAsDestination.Add(toAdd);
                }
            }
            return new ReadOnlyCollection<TDestination>(sourceAsDestination);
        }
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
        {
            return source.Select((item, index) => (item, index));
        }

        public static bool AreAllSame<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

            using (var enumerator = enumerable.GetEnumerator())
            {
                var toCompare = default(T);
                if (enumerator.MoveNext())
                {
                    toCompare = enumerator.Current;
                }

                while (enumerator.MoveNext())
                {
                    if (toCompare != null && !toCompare.Equals(enumerator.Current))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static IEnumerable<T> Process<T>(this IEnumerable<T> collection, Func<T, T> func)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            foreach (var item in collection)
            {
                yield return func(item);
            }
        }
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            foreach (var item in collection)
            {
                action(item);
            }
        }
        public static void ForEach(this IEnumerable collection, Action<object> action)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}
