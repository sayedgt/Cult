using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
// ReSharper disable UnusedMember.Global

namespace Cult.Extensions
{
    public static class TypeExtensions
    {
        public static bool Any<T>(this T obj, params T[] values)
        {
            return Array.IndexOf(values, obj) != -1;
        }
        public static object CreateInstance(this Type type, params object[] constructorParameters)
        {
            return CreateInstance<object>(type, constructorParameters);
        }

        public static T CreateInstance<T>(this Type type, params object[] constructorParameters)
        {
            var instance = Activator.CreateInstance(type, constructorParameters);
            return (T)instance;
        }

        public static IDictionary<string, int> EnumToDictionary(this Type @this)
        {
            if (@this == null) throw new NullReferenceException();
            if (!@this.IsEnum) throw new InvalidCastException("object is not an Enum.");

            var names = Enum.GetNames(@this);
            var values = Enum.GetValues(@this);

            return (from i in Enumerable.Range(0, names.Length)
                    select new { Key = names[i], Value = (int)values.GetValue(i) })
                .ToDictionary(k => k.Key, k => k.Value);
        }

        private static IEnumerable<Type> GetAllInterfaces(Type type, HashSet<Type> types = null)
        {
            types = types ?? new HashSet<Type>();
            var interfaces = type.GetInterfaces();
            foreach (var it in interfaces)
            {
                types.Add(it);
            }
            foreach (var i in interfaces)
            {
                GetAllInterfaces(i, types);
            }
            return types;
        }

        public static string GetFullTypeName(this Type type)
        {
            if (type == null)
            {
                return string.Empty;
            }
            if (type.IsDotNetSystemType())
            {
                return type.FullName;
            }
            if (type.Assembly.GetName().GetPublicKeyToken().Length <= 0)
            {
                return $"{type.FullName}, {type.Assembly.GetName().Name}";
            }
            return type.AssemblyQualifiedName;
        }

        public static IEnumerable<Type> GetNestedInterfaces(this Type type)
        {
            return GetAllInterfaces(type);
        }

        public static bool HasDefaultConstructor(this Type t)
        {
            return t.IsValueType || t.GetConstructor(Type.EmptyTypes) != null;
        }

        public static bool HasInterface(this Type type)
        {
            var list = GetNestedInterfaces(type);
            foreach (var l in list)
            {
                if (l.FullName == type.FullName) return true;
            }

            return false;
        }

        public static bool IsCollection(this Type type)
        {
            return type.GetInterface("ICollection") != null;
        }

        public static bool IsEnumerable(this Type type)
        {
            return type.GetInterface("IEnumerable") != null;
        }

        public static bool IsDotNetSystemType(this Type type)
        {
            if (type == null)
            {
                return false;
            }
            var nameToCheck = type.Assembly.GetName();
            var exceptions = new[] { "Microsoft.SqlServer.Types" };
            return new[] { "System", "mscorlib", "System.", "Microsoft." }
                       .Any(s => (s.EndsWith(".") && nameToCheck.Name.StartsWith(s)) || (nameToCheck.Name == s)) &&
                   !exceptions.Any(s => nameToCheck.Name.StartsWith(s));
        }

        public static bool IsNullableValueType(this Type toCheck)
        {
            if ((toCheck == null) || !toCheck.IsValueType)
            {
                return false;
            }
            return toCheck.IsGenericType && toCheck.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool None<T>(this T obj, params T[] values)
        {
            return obj.Any(values);
        }

        public static DbType ToDbType(this Type type)
        {
            var typeMap = new Dictionary<Type, DbType>
            {
                [typeof(byte)] = DbType.Byte,
                [typeof(sbyte)] = DbType.SByte,
                [typeof(short)] = DbType.Int16,
                [typeof(ushort)] = DbType.UInt16,
                [typeof(int)] = DbType.Int32,
                [typeof(uint)] = DbType.UInt32,
                [typeof(long)] = DbType.Int64,
                [typeof(ulong)] = DbType.UInt64,
                [typeof(float)] = DbType.Single,
                [typeof(double)] = DbType.Double,
                [typeof(decimal)] = DbType.Decimal,
                [typeof(bool)] = DbType.Boolean,
                [typeof(string)] = DbType.String,
                [typeof(char)] = DbType.StringFixedLength,
                [typeof(Guid)] = DbType.Guid,
                [typeof(DateTime)] = DbType.DateTime,
                [typeof(DateTimeOffset)] = DbType.DateTimeOffset,
                [typeof(byte[])] = DbType.Binary,
                [typeof(byte?)] = DbType.Byte,
                [typeof(sbyte?)] = DbType.SByte,
                [typeof(short?)] = DbType.Int16,
                [typeof(ushort?)] = DbType.UInt16,
                [typeof(int?)] = DbType.Int32,
                [typeof(uint?)] = DbType.UInt32,
                [typeof(long?)] = DbType.Int64,
                [typeof(ulong?)] = DbType.UInt64,
                [typeof(float?)] = DbType.Single,
                [typeof(double?)] = DbType.Double,
                [typeof(decimal?)] = DbType.Decimal,
                [typeof(bool?)] = DbType.Boolean,
                [typeof(char?)] = DbType.StringFixedLength,
                [typeof(Guid?)] = DbType.Guid,
                [typeof(DateTime?)] = DbType.DateTime,
                [typeof(DateTimeOffset?)] = DbType.DateTimeOffset,
                // [typeof(Binary)] = DbType.Binary
            };

            return typeMap[type];
        }

        private static bool IsOneOfAttributes(Type attribute, object[] objects)
        {
            foreach (var attr in objects)
            {
                var currentAttr = attr.GetType();
                if (currentAttr == attribute)
                    return true;
            }
            return false;
        }

        public static bool HasAttribute<TAttribute>(this PropertyInfo @this, bool inherit = false) where TAttribute : Attribute
        {
            var tAttribute = typeof(TAttribute);
            var attrs = @this.GetCustomAttributes(inherit);
            return IsOneOfAttributes(tAttribute, attrs);
        }

        public static bool HasAttribute<TAttribute>(this MemberInfo @this, bool inherit = false) where TAttribute : Attribute
        {
            var tAttribute = typeof(TAttribute);
            var attrs = @this.GetCustomAttributes(inherit);
            return IsOneOfAttributes(tAttribute, attrs);
        }

        public static bool HasAttribute<TAttribute>(this FieldInfo @this, bool inherit = false) where TAttribute : Attribute
        {
            var tAttribute = typeof(TAttribute);
            var attrs = @this.GetCustomAttributes(inherit);
            return IsOneOfAttributes(tAttribute, attrs);
        }

        public static bool HasAttribute<TAttribute>(this MethodInfo @this, bool inherit = false) where TAttribute : Attribute
        {
            var tAttribute = typeof(TAttribute);
            var attrs = @this.GetCustomAttributes(inherit);
            return IsOneOfAttributes(tAttribute, attrs);
        }

        public static bool HasAttribute<TAttribute>(this EventInfo @this, bool inherit = false) where TAttribute : Attribute
        {
            var tAttribute = typeof(TAttribute);
            var attrs = @this.GetCustomAttributes(inherit);
            return IsOneOfAttributes(tAttribute, attrs);
        }

        public static bool HasAttribute<TAttribute>(this Type @this, bool inherit = false) where TAttribute : Attribute
        {
            var tAttribute = typeof(TAttribute);
            var attrs = @this.GetCustomAttributes(inherit);
            return IsOneOfAttributes(tAttribute, attrs);
        }
    }
}
