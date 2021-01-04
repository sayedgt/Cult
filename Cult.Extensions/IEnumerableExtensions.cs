using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cult.Extensions.Pagination;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable PossibleMultipleEnumeration

namespace Cult.Extensions
{
    public static class IEnumerableExtensions
    {
        public static string PathCombine(this IEnumerable<string> @this)
        {
            return Path.Combine(@this.ToArray());
        }

        public static void Delete(this IEnumerable<FileInfo> @this)
        {
            foreach (FileInfo t in @this)
            {
                t.Delete();
            }
        }

        public static IEnumerable<FileInfo> ForEach(this IEnumerable<FileInfo> @this, Action<FileInfo> action)
        {
            foreach (FileInfo t in @this)
            {
                action(t);
            }
            return @this;
        }
        public static void Delete(this IEnumerable<DirectoryInfo> @this)
        {
            foreach (DirectoryInfo t in @this)
            {
                t.Delete();
            }
        }

        public static IEnumerable<DirectoryInfo> ForEach(this IEnumerable<DirectoryInfo> @this, Action<DirectoryInfo> action)
        {
            foreach (DirectoryInfo t in @this)
            {
                action(t);
            }
            return @this;
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

        public static IEnumerable<T> DistinctBy<T>(this IEnumerable<T> list, Func<T, object> propertySelector)
        {
            return list.GroupBy(propertySelector).Select(x => x.First());
        }

        public static IEnumerable<TKey> DistinctBy<T, TKey>(this IEnumerable<T> list, Func<T, TKey> selector)
        {
            return list.GroupBy(selector).Select(x => x.Key);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
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

        public static bool HasCountOf<T>(this IEnumerable<T> source, int count)
        {
            return source.Count() == count;
        }

        public static bool HasCountOf<T>(this IEnumerable<T> source, Func<T, bool> query, int count)
        {
            return source.Count(query) == count;
        }

        public static bool HasMany<T>(this IEnumerable<T> source)
        {
            return source.Count() > 1;
        }

        public static bool HasMany<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Count(query) > 1;
        }

        public static bool HasNone<T>(this IEnumerable<T> source)
        {
            return source.Any() == false;
        }

        public static bool HasNone<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Any(query) == false;
        }

        public static bool HasOnlyOne<T>(this IEnumerable<T> source)
        {
            return source.Count() == 1;
        }

        public static bool HasOnlyOne<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Count(query) == 1;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> @this)
        {
            return !@this.Any();
        }

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

        public static string StringJoin<T>(this IEnumerable<T> @this, string separator)
        {
            return string.Join(separator, @this);
        }

        public static string StringJoin<T>(this IEnumerable<T> @this, char separator)
        {
            return string.Join(separator.ToString(), @this);
        }

        public static Collection<T> ToCollection<T>(this IEnumerable<T> enumerable)
        {
            var collection = new Collection<T>();
            foreach (var i in enumerable)
                collection.Add(i);
            return collection;
        }

        public static string ToCsv<T>(this IEnumerable<T> source, char separator)
        {
            if (source == null)
                return string.Empty;

            var csv = new StringBuilder();
            source.ForEach(value => csv.AppendFormat("{0}{1}", value, separator));
            return csv.ToString(0, csv.Length - 1);
        }

        public static string ToCSV<T>(this IEnumerable<T> source)
        {
            return source.ToCsv(',');
        }

        public static HashSet<TDestination> ToHashSet<TDestination>(this IEnumerable<TDestination> source)
        {
            return new HashSet<TDestination>(source);
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

        public static IEnumerable<T> GetDuplicateItems<T>(this IEnumerable<T> @this)
        {
            HashSet<T> hashset = new HashSet<T>();
            IEnumerable<T> duplicates = @this.Where(e => !hashset.Add(e));
            return duplicates;
        }

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> items, int pageIndex, int pageSize)
        {
            return items.AsQueryable().ToPagedList(pageIndex, pageSize);
        }

        public static PagedList<TEntity> ToPagedList<TEntity, TProperty>(this IEnumerable<TEntity> items, int pageIndex, int pageSize, PagedSortDirection sortDirection = PagedSortDirection.Ascending, Expression<Func<TEntity, TProperty>> orderbyExpression = null)
        {
            return items.AsQueryable().ToPagedList(pageIndex, pageSize, sortDirection, orderbyExpression);
        }

        public static IEnumerable<T> ToPaged<T>(this IEnumerable<T> query, int pageIndex, int pageSize)
        {
            return query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
        }
        public static IEnumerable<IEnumerable<T>> GetPermutationsWithRepetition<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new[] { t });
            return GetPermutationsWithRepetition(list, length - 1)
                .SelectMany(t => list,
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }

        public static IEnumerable<IEnumerable<T>> GetKCombinationsWithRepetition<T>(this IEnumerable<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new[] { t });
            return GetKCombinationsWithRepetition(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) >= 0),
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }

        public static IEnumerable<IEnumerable<T>> GetKCombinations<T>(this IEnumerable<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new[] { t });
            return GetKCombinations(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }
    }
}
