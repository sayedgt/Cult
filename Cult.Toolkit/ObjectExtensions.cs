using Cult.Toolkit.ExtraIEnumerable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

// ReSharper disable All
namespace Cult.Toolkit.ExtraObject
{
    public static partial class ObjectExtensions
    {
        public static T IfNull<T>(this T obj, Action action)
        {
            if (obj == null)
            {
                action();
            }

            return obj;
        }

        public static T IfNull<T>(this T obj, Func<T> func)
        {
            return obj == null ? func() : obj;
        }

        public static T IfNotNull<T>(this T obj, Action<T> action)
        {
            if (obj != null)
            {
                action(obj);
            }

            return obj;
        }

        public static T IfNotNull<T>(this T obj, Action action)
        {
            if (obj != null)
            {
                action();
            }

            return obj;
        }
        public static void IfType<T>(this object item, Action<T> action) where T : class
        {
            if (item is T)
            {
                action(item as T);
            }
        }
        public static void IfNotType<T>(this object item, Action<T> action) where T : class
        {
            if (!(item is T))
            {
                action(item as T);
            }
        }
        public static bool Any<T>(this T obj, params T[] values)
        {
            return Array.IndexOf(values, obj) != -1;
        }
        public static T CastAs<T>(this object @this)
        {
            return (T)@this;
        }
        public static T CastAsOrDefault<T>(this object @this)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return default;
            }
        }
        public static T CastAsOrDefault<T>(this object @this, T defaultValue)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        public static T CastAsOrDefault<T>(this object @this, Func<T> defaultValueFactory)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        public static T CastAsOrDefault<T>(this object @this, Func<object, T> defaultValueFactory)
        {
            try
            {
                return (T)@this;
            }
            catch (Exception)
            {
                return defaultValueFactory(@this);
            }
        }
        public static bool CanConvertTo<T>(this object value)
        {
            if (value != null)
            {
                var targetType = typeof(T);

                var converter = TypeDescriptor.GetConverter(value);
                if (converter.CanConvertTo(targetType))
                    return true;

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter.CanConvertFrom(value.GetType()))
                    return true;
            }
            return false;
        }
        public static object ChangeType(this object value, TypeCode typeCode)
        {
            return Convert.ChangeType(value, typeCode);
        }
        public static object ChangeType(this object value, TypeCode typeCode, IFormatProvider provider)
        {
            return Convert.ChangeType(value, typeCode, provider);
        }
        public static object ChangeType(this object value, Type conversionType)
        {
            return Convert.ChangeType(value, conversionType);
        }
        public static object ChangeType(this object value, Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(value, conversionType, provider);
        }
        public static object ChangeType<T>(this object value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        public static object ChangeType<T>(this object value, IFormatProvider provider)
        {
            return (T)Convert.ChangeType(value, typeof(T), provider);
        }
        public static T Coalesce<T>(this T @this, params T[] values) where T : class
        {
            if (@this != null)
            {
                return @this;
            }

            foreach (var value in values)
            {
                if (value != null)
                {
                    return value;
                }
            }

            return null;
        }
        public static T CoalesceOrDefault<T>(this T @this, params T[] values) where T : class
        {
            if (@this != null)
            {
                return @this;
            }

            foreach (var value in values)
            {
                if (value != null)
                {
                    return value;
                }
            }

            return default;
        }
        public static T CoalesceOrDefault<T>(this T @this, Func<T> defaultValueFactory, params T[] values) where T : class
        {
            if (@this != null)
            {
                return @this;
            }

            foreach (var value in values)
            {
                if (value != null)
                {
                    return value;
                }
            }

            return defaultValueFactory();
        }
        public static T ConvertTo<T>(this object value)
        {
            return value.ConvertTo(default(T));
        }
        public static T ConvertTo<T>(this object value, T defaultValue)
        {
            if (value != null)
            {
                var targetType = typeof(T);

                if (value.GetType() == targetType) return (T)value;

                var converter = TypeDescriptor.GetConverter(value);
                if (converter.CanConvertTo(targetType))
                    return (T)converter.ConvertTo(value, targetType);

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter.CanConvertFrom(value.GetType()))
                    return (T)converter.ConvertFrom(value);
            }
            return defaultValue;
        }
        public static T ConvertTo<T>(this object value, T defaultValue, bool ignoreException)
        {
            if (!ignoreException) return value.ConvertTo<T>();
            try
            {
                return value.ConvertTo<T>();
            }
            catch
            {
                return defaultValue;
            }
        }
        public static T ConvertToAndIgnoreException<T>(this object value)
        {
            return value.ConvertToAndIgnoreException(default(T));
        }
        public static T ConvertToAndIgnoreException<T>(this object value, T defaultValue)
        {
            return value.ConvertTo(defaultValue, true);
        }

        public static T[] CreateArray<T>(this T obj)
        {
            return new[] { obj };
        }

        public static ICollection<T> CreateCollection<T>(this T obj)
        {
            return CreateList(obj);
        }
        public static IList<T> CreateList<T>(this T obj)
        {
            return new List<T>() { obj };
        }
        public static IDictionary<TKey, TValue> CreateDictionary<TKey, TValue>(this TValue value, TKey key)
        {
            var dictionary = new Dictionary<TKey, TValue>();
            dictionary.Add(key, value);
            return dictionary;
        }
        public static IEnumerable<T> CreateEnumerable<T>(this T obj)
        {
            yield return obj;
        }
        public static T Clone<T>(this T @this) where T : ISerializable
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, @this);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
        public static TypeCode GetTypeCode(this object value)
        {
            return Convert.GetTypeCode(value);
        }
        public static TResult GetValueOrDefault<T, TResult>(this T @this, Func<T, TResult> func)
        {
            try
            {
                return func(@this);
            }
            catch (Exception)
            {
                return default;
            }
        }
        public static TResult GetValueOrDefault<T, TResult>(this T @this, Func<T, TResult> func, TResult defaultValue)
        {
            try
            {
                return func(@this);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        public static TResult GetValueOrDefault<T, TResult>(this T @this, Func<T, TResult> func, Func<TResult> defaultValueFactory)
        {
            try
            {
                return func(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory();
            }
        }
        public static TResult GetValueOrDefault<T, TResult>(this T @this, Func<T, TResult> func, Func<T, TResult> defaultValueFactory)
        {
            try
            {
                return func(@this);
            }
            catch (Exception)
            {
                return defaultValueFactory(@this);
            }
        }
        public static TResult IfNotNull<T, TResult>(this T @this, Func<T, TResult> func) where T : class
        {
            return @this != null ? func(@this) : default;
        }
        public static TResult IfNotNull<T, TResult>(this T @this, Func<T, TResult> func, TResult defaultValue) where T : class
        {
            return @this != null ? func(@this) : defaultValue;
        }
        public static TResult IfNotNull<T, TResult>(this T @this, Func<T, TResult> func, Func<TResult> defaultValueFactory) where T : class
        {
            return @this != null ? func(@this) : defaultValueFactory();
        }
        public static bool In<T>(this T @this, params T[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }
        public static bool InRange<T>(this T @this, T minValue, T maxValue) where T : IComparable<T>
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }
        public static object InvokeMethod(this object obj, string methodName, params object[] parameters)
        {
            return InvokeMethod<object>(obj, methodName, parameters);
        }
        public static T InvokeMethod<T>(this object obj, string methodName, params object[] parameters)
        {
            var type = obj.GetType();
            var method = type.GetMethod(methodName, parameters.Select(o => o.GetType()).ToArray());

            if (method == null)
                throw new ArgumentException($"Method '{methodName}' not found.", methodName);

            var value = method.Invoke(obj, parameters);
            return value is T value1 ? value1 : default;
        }
        public static bool IsArray(this object obj)
        {
            return obj.GetType().IsArray;
        }
        public static bool IsAssignableFrom<T>(this object @this)
        {
            var type = @this.GetType();
            return type.IsAssignableFrom(typeof(T));
        }
        public static bool IsAssignableFrom(this object @this, Type targetType)
        {
            var type = @this.GetType();
            return type.IsAssignableFrom(targetType);
        }
        public static bool IsAssignableTo<T>(this object obj)
        {
            return obj.IsAssignableTo(typeof(T));
        }
        public static bool IsAssignableTo(this object obj, Type type)
        {
            var objectType = obj.GetType();
            return type.IsAssignableFrom(objectType);
        }
        public static bool IsBetween<T>(this T value, T minValue, T maxValue) where T : IComparable<T>
        {
            return minValue.CompareTo(value) == -1 && value.CompareTo(maxValue) == -1;
        }

        public static bool IsBetweenInclusive<T>(this T value, T minValue, T maxValue) where T : IComparable<T>
        {
            return value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) <= 0;
        }
        public static bool IsGreaterThan<T>(this T value, T compareValue) where T : IComparable<T>
        {

            return value.CompareTo(compareValue) > 0;
        }
        public static bool IsGreaterOrEqualsThan<T>(this T value, T compareValue) where T : IComparable<T>
        {
            return value.CompareTo(compareValue) >= 0;
        }

        public static bool IsSmallerThan<T>(this T value, T compareValue) where T : IComparable<T>
        {
            return value.CompareTo(compareValue) == -1;
        }
        public static bool IsSmallerOrEqualsThan<T>(this T value, T compareValue) where T : IComparable<T>
        {
            return value.CompareTo(compareValue) <= 0;
        }

        public static bool IsDate(this object obj)
        {
            return obj.IsTypeOf(typeof(DateTime));
        }
        public static bool IsDBNull<T>(this T value) where T : class
        {
            return Convert.IsDBNull(value);
        }
        public static bool IsDBNull(this object obj)
        {
            return obj.IsTypeOf(typeof(DBNull));
        }
        public static bool IsDefault<T>(this T source)
        {
            return EqualityComparer<T>.Default.Equals(source, default) || source == null;
        }
        public static bool IsNotNull<T>(this T @this) where T : class
        {
            return @this != null;
        }
        public static bool IsNull<T>(this T @this) where T : class
        {
            return @this == null;
        }
        public static bool IsOfType<T>(this object obj)
        {
            return obj.IsOfType(typeof(T));
        }
        public static bool IsOfType(this object obj, Type type)
        {
            return obj.GetType() == type;
        }
        public static bool IsOfTypeOrInherits<T>(this object obj)
        {
            return obj.IsOfTypeOrInherits(typeof(T));
        }
        public static bool IsOfTypeOrInherits(this object obj, Type type)
        {
            var objectType = obj.GetType();
            do
            {
                if (objectType == type)
                    return true;
                if (objectType == objectType.BaseType || objectType.BaseType == null)
                    return false;
                objectType = objectType.BaseType;
            } while (true);
        }
        public static bool IsSameType(this object obj1, object obj2)
        {
            return obj1.GetType().IsInstanceOfType(obj2) || obj2.GetType().IsInstanceOfType(obj1);
        }
        public static bool IsTypeOf(this object obj, Type type)
        {
            return obj.GetType() == type;
        }
        public static bool IsTypeOf<T>(this object obj)
        {
            return obj.GetType() == typeof(T);
        }
        public static bool NotIn<T>(this T @this, params T[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
        public static T NullIf<T>(this T @this, Func<T, bool> predicate) where T : class
        {
            return predicate(@this) ? null : @this;
        }
        public static T NullIfEquals<T>(this T @this, T value) where T : class
        {
            return @this.Equals(value) ? null : @this;
        }
        public static T NullIfEqualsAny<T>(this T @this, params T[] values) where T : class
        {
            return Array.IndexOf(values, @this) != -1 ? null : @this;
        }
        public static T ShallowCopy<T>(this T @this)
        {
            var method = @this.GetType().GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)method?.Invoke(@this, null);
        }
        public static T[] ToArrayObject<T>(this T obj)
        {
            return new[] { obj };
        }
        public static byte[] ToByteArray<T>(this T obj)
        {
            if (obj == null)
                return null;
            var bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        public static IEnumerable<T> ToListObject<T>(this T obj)
        {
            return new[] { obj };
        }
        public static TResult Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction)
        {
            try
            {
                return tryFunction(@this);
            }
            catch
            {
                return default;
            }
        }
        public static TResult Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction, TResult catchValue)
        {
            try
            {
                return tryFunction(@this);
            }
            catch
            {
                return catchValue;
            }
        }
        public static TResult Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction, Func<TType, TResult> catchValueFactory)
        {
            try
            {
                return tryFunction(@this);
            }
            catch
            {
                return catchValueFactory(@this);
            }
        }
        public static bool Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction, out TResult result)
        {
            try
            {
                result = tryFunction(@this);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }
        public static bool Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction, TResult catchValue, out TResult result)
        {
            try
            {
                result = tryFunction(@this);
                return true;
            }
            catch
            {
                result = catchValue;
                return false;
            }
        }
        public static bool Try<TType, TResult>(this TType @this, Func<TType, TResult> tryFunction, Func<TType, TResult> catchValueFactory, out TResult result)
        {
            try
            {
                result = tryFunction(@this);
                return true;
            }
            catch
            {
                result = catchValueFactory(@this);
                return false;
            }
        }
        public static bool Try<TType>(this TType @this, Action<TType> tryAction)
        {
            try
            {
                tryAction(@this);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Try<TType>(this TType @this, Action<TType> tryAction, Action<TType> catchAction)
        {
            try
            {
                tryAction(@this);
                return true;
            }
            catch
            {
                catchAction(@this);
                return false;
            }
        }
        public static bool TryDispose(this object toDispose)
        {
            var disposable = toDispose as IDisposable;
            if (disposable == null)
                return false;

            disposable.Dispose();
            return true;
        }
        public static Lazy<T> AsLazy<T>(this T obj)
        {
            return new Lazy<T>();
        }
        public static string ToJson<T>(this T obj, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Serialize<T>(obj, options);
        }

        public static string ToFormattedJson<T>(this T obj)
        {
            return JsonSerializer.Serialize<T>(obj, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
        }

        public static dynamic ToDynamic(this string jsonText)
        {
            return JsonSerializer.Deserialize<dynamic>(jsonText);
        }

        public static byte[] Serialize<T>(this T o) where T : class, ISerializable
        {
            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, o);
                return ms.ToArray();
            }
        }
    }
}
