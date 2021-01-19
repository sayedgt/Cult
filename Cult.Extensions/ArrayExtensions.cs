using Cult.Extensions.ExtraInt;
using Cult.Extensions.ExtraObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
// ReSharper disable All 
namespace Cult.Extensions.ExtraArray
{
    public static class ArrayExtensions
    {
        public static List<List<T>> Split<T>(this T[] source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
        public static bool All<T>(this T[] array, Func<T, bool> predicate)
        {
            foreach (var item in array)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool Any<T>(this T[] array, Func<T, bool> predicate)
        {
            foreach (var item in array)
            {
                if (predicate(item))
                {
                    return true;
                }
            }
            return false;
        }
        public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
        {
            return Array.AsReadOnly(array);
        }
        public static void Clear(this Array array, int index, int length)
        {
            Array.Clear(array, index, length);
        }
        public static T[] ClearAll<T>(this T[] arrayToClear)
        {
            if (arrayToClear != null)
                for (var i = arrayToClear.GetLowerBound(0); i <= arrayToClear.GetUpperBound(0); ++i)
                    arrayToClear[i] = default(T);
            return arrayToClear;
        }
        public static void ClearAll(this Array @this)
        {
            Array.Clear(@this, 0, @this.Length);
        }
        public static Array ClearAt(this Array arrayToClear, int at)
        {
            if (arrayToClear != null)
            {
                int arrayIndex = at.GetArrayIndex();
                if (arrayIndex.IsIndexInArray(arrayToClear))
                    Array.Clear(arrayToClear, arrayIndex, 1);
            }
            return arrayToClear;
        }
        public static T[] ClearAt<T>(this T[] arrayToClear, int at)
        {
            if (arrayToClear != null)
            {
                int arrayIndex = at.GetArrayIndex();
                if (arrayIndex.IsIndexInArray(arrayToClear))
                    arrayToClear[arrayIndex] = default(T);
            }
            return arrayToClear;
        }
        public static T[] CombineArray<T>(this T[] combineWith, T[] arrayToCombine)
        {
            if (combineWith != default(T[]) && arrayToCombine != default(T[]))
            {
                var initialSize = combineWith.Length;
                Array.Resize(ref combineWith, initialSize + arrayToCombine.Length);
                Array.Copy(arrayToCombine, arrayToCombine.GetLowerBound(0), combineWith, initialSize, arrayToCombine.Length);
            }
            return combineWith;
        }
        public static void Copy(this Array sourceArray, Array destinationArray, int length)
        {
            Array.Copy(sourceArray, destinationArray, length);
        }
        public static void Copy(this Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
        {
            Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
        }
        public static void Copy(this Array sourceArray, Array destinationArray, long length)
        {
            Array.Copy(sourceArray, destinationArray, length);
        }
        public static void Copy(this Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)
        {
            Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
        }
        public static bool Exists<T>(this T[] array, Predicate<T> match)
        {
            return Array.Exists(array, match);
        }
        public static T Find<T>(this T[] array, Predicate<T> match)
        {
            return Array.Find(array, match);
        }
        public static T[] FindAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindAll(array, match);
        }
        public static int FindIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindIndex(array, match);
        }
        public static int FindIndex<T>(this T[] array, int startIndex, Predicate<T> match)
        {
            return Array.FindIndex(array, startIndex, match);
        }
        public static int FindIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match)
        {
            return Array.FindIndex(array, startIndex, count, match);
        }
        public static T FindLast<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindLast(array, match);
        }
        public static int FindLastIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindLastIndex(array, match);
        }
        public static int FindLastIndex<T>(this T[] array, int startIndex, Predicate<T> match)
        {
            return Array.FindLastIndex(array, startIndex, match);
        }
        public static int FindLastIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match)
        {
            return Array.FindLastIndex(array, startIndex, count, match);
        }
        public static void ForEach<T>(this T[] source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }
        public static void ForEach<T>(this T[] source, Action<T> action, Func<T, bool> predicate)
        {
            foreach (var item in source.Where(predicate))
                action(item);
        }
        public static void ForEachReverse<T>(this T[] source, Action<T> action)
        {
            var items = source.ToList();
            for (var i = items.Count - 1; i >= 0; i--)
            {
                action(items[i]);
            }
        }
        public static void ForEachReverse<T>(this T[] source, Action<T> action, Func<T, bool> predicate)
        {
            var items = source.ToList();
            for (var i = items.Count - 1; i >= 0; i--)
            {
                if (predicate(items[i]))
                    action(items[i]);
            }
        }
        public static byte GetByte(this Array array, int index)
        {
            return Buffer.GetByte(array, index);
        }
        public static T[] GetDuplicateItems<T>(this T[] @this)
        {
            var hashset = new HashSet<T>();
            var duplicates = @this.Where(e => !hashset.Add(e));
            return duplicates.ToArray();
        }
        public static int IndexOf(this Array array, object value)
        {
            return Array.IndexOf(array, value);
        }
        public static int IndexOf(this Array array, object value, int startIndex)
        {
            return Array.IndexOf(array, value, startIndex);
        }
        public static int IndexOf(this Array array, object value, int startIndex, int count)
        {
            return Array.IndexOf(array, value, startIndex, count);
        }
        public static bool IsEmpty(this Array array)
        {
            if (array.IsNull())
            {
                throw new ArgumentNullException(nameof(array));
            }
            return array.Length == 0;
        }
        public static int LastIndexOf(this Array array, object value)
        {
            return Array.LastIndexOf(array, value);
        }
        public static int LastIndexOf(this Array array, object value, int startIndex)
        {
            return Array.LastIndexOf(array, value, startIndex);
        }
        public static int LastIndexOf(this Array array, object value, int startIndex, int count)
        {
            return Array.LastIndexOf(array, value, startIndex, count);
        }
        public static T[] Remove<T>(this T[] source, T item)
        {
            var result = new T[source.Length - source.Count(s => s.Equals(item))];
            var x = 0;
            foreach (var i in source.Where(i => !Equals(i, item)))
            {
                result[x] = i;
                x++;
            }
            return result;
        }
        public static T[] RemoveAll<T>(this T[] source, Predicate<T> predicate)
        {
            var result = new T[source.Length - source.Count(s => predicate(s))];
            var i = 0;
            foreach (var item in source.Where(item => !predicate(item)))
            {
                result[i] = item;
                i++;
            }
            return result;
        }
        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            var result = new T[source.Length - 1];
            var x = 0;
            for (var i = 0; i < source.Length; i++)
            {
                if (i == index) continue;
                result[x] = source[i];
                x++;
            }
            return result;
        }
        public static T[] RemoveDuplicateItems<T>(this T[] @this)
        {
            var hashset = new HashSet<T>();
            foreach (var item in @this)
            {
                hashset.Add(item);
            }
            return hashset.ToArray();
        }
        public static IEnumerable<T> Reverse<T>(this T[] source)
        {
            for (var i = source.Length - 1; i >= 0; i--)
            {
                yield return source[i];
            }
        }
        public static void Reverse(this Array array)
        {
            Array.Reverse(array);
        }
        public static void Reverse(this Array array, int index, int length)
        {
            Array.Reverse(array, index, length);
        }
        public static void Sort(this Array array)
        {
            Array.Sort(array);
        }
        public static void Sort(this Array array, Array items)
        {
            Array.Sort(array, items);
        }
        public static void Sort(this Array array, int index, int length)
        {
            Array.Sort(array, index, length);
        }
        public static void Sort(this Array array, Array items, int index, int length)
        {
            Array.Sort(array, items, index, length);
        }
        public static void Sort(this Array array, IComparer comparer)
        {
            Array.Sort(array, comparer);
        }
        public static void Sort(this Array array, Array items, IComparer comparer)
        {
            Array.Sort(array, items, comparer);
        }
        public static void Sort(this Array array, int index, int length, IComparer comparer)
        {
            Array.Sort(array, index, length, comparer);
        }
        public static void Sort(this Array array, Array items, int index, int length, IComparer comparer)
        {
            Array.Sort(array, items, index, length, comparer);
        }
        public static DataTable ToDataTable<T>(this T[] @this)
        {
            var type = typeof(T);

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var dt = new DataTable();

            foreach (var property in properties)
            {
                dt.Columns.Add(property.Name, property.PropertyType);
            }

            foreach (var field in fields)
            {
                dt.Columns.Add(field.Name, field.FieldType);
            }

            foreach (var item in @this)
            {
                var dr = dt.NewRow();

                foreach (var property in properties)
                {
                    dr[property.Name] = property.GetValue(item, null);
                }

                foreach (var field in fields)
                {
                    dr[field.Name] = field.GetValue(item);
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }
        public static bool TrueForAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.TrueForAll(array, match);
        }
        public static bool WithinIndex(this Array @this, int index)
        {
            return index >= 0 && index < @this.Length;
        }
        public static bool WithinIndex(this Array @this, int index, int dimension)
        {
            var d = 0;
            if (dimension > 0)
                d = dimension;
            return index >= @this.GetLowerBound(d) && index <= @this.GetUpperBound(d);
        }
    }
}
