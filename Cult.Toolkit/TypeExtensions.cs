using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Cult.Toolkit.ExtraObject;
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

namespace Cult.Toolkit.ExtraType
{
    public static class TypeExtensions
    {
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
            types ??= new HashSet<Type>();
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
        public static bool HasDefaultConstructor(this Type t)
        {
            return t.IsValueType || t.GetConstructor(Type.EmptyTypes) != null;
        }
        public static bool HasInterface(this Type type)
        {
            foreach (var l in GetNestedInterfaces(type))
            {
                if (l.FullName == type.FullName) return true;
            }

            return false;
        }
        public static bool IsCollection(this Type type)
        {
            return type.GetInterface("ICollection") != null;
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
        public static bool IsEnumerable(this Type type)
        {
            return type.GetInterface("IEnumerable") != null;
        }
        public static bool IsNullableValueType(this Type toCheck)
        {
            if ((toCheck == null) || !toCheck.IsValueType)
            {
                return false;
            }
            return toCheck.IsGenericType && toCheck.GetGenericTypeDefinition() == typeof(Nullable<>);
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
        public static bool IsRecord(this Type type)
        {
            var isRecord1 = ((TypeInfo)type).DeclaredProperties.FirstOrDefault(x => x.Name == "EqualityContract")?.GetMethod?.GetCustomAttribute(typeof(CompilerGeneratedAttribute)) is object;
            var isRecord2 = type.GetMethod("<Clone>$") is object;
            if (isRecord1 || isRecord2) return true;
            return false;
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

        public static bool IsTuple(this Type type)
        {
            return type.FullName.StartsWith("System.Tuple`", StringComparison.Ordinal);
        }

        public static bool IsTypeOf<T>(this object obj)
        {
            return obj.GetType() == typeof(T);
        }

        public static bool IsDictionary(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
        }

        public static bool IsInstanceOfType(this Type type, object obj)
        {
            return obj != null && type.IsInstanceOfType(obj);
        }

        public static IEnumerable<Type> GetInnerTypes(this Type type, params Type[] simpleTypes)
        {
            var isPrimitive = type.IsPrimitive;
            var status = type.FullName.StartsWith("System.", StringComparison.Ordinal)
                && !type.IsDictionary()
                && !type.IsEnumerable()
                && !type.IsCollection()
                && !type.IsTuple()
                && !type.IsArray
                ;
            if (isPrimitive
                || status
                || type == typeof(string)
                || type.IsEnum
                || type.IsValueType
                || simpleTypes.Contains(type))
            {
                return new List<Type>() { type };
            }

            var list = new List<Type>();
            if (type.IsGenericType)
            {
                if (type.IsDictionary())
                {
                    Type keyType = type.GetGenericArguments()[0];
                    list.Add(keyType);
                    Type valueType = type.GetGenericArguments()[1];
                    list.Add(valueType);
                    list.AddRange(GetInnerTypes(keyType));
                    list.AddRange(GetInnerTypes(valueType));

                }
                if (type.IsEnumerable() || type.IsCollection())
                {
                    Type itemType = type.GetGenericArguments()[0];
                    list.Add(itemType);
                    list.AddRange(GetInnerTypes(itemType));

                }
                if (type.IsTuple())
                {
                    var args = type.GetGenericArguments();
                    foreach (var arg in args)
                    {
                        list.Add(arg);
                        list.AddRange(GetInnerTypes(arg));
                    }
                }
            }
            else
            {
                if (type.IsArray)
                {
                    var t = type.GetElementType();
                    list.Add(t);
                    list.AddRange(GetInnerTypes(t));

                }
                if (type.IsInterface)
                {
                    // TODO
                }
                if (type.IsClass)
                {
                    var ts = type.GetProperties().Select(x => x.PropertyType).ToList();
                    list.AddRange(ts);
                    foreach (var ttss in ts)
                        list.AddRange(GetInnerTypes(ttss));
                }
            }
            var lst = list.Distinct().ToList();
            return SortInnerTypes(lst, simpleTypes);
        }
        private static List<Type> SortInnerTypes(List<Type> types, params Type[] simpleTypes)
        {
            var primitives = new List<Type>();
            var systems = new List<Type>();
            var enums = new List<Type>();
            var str = new List<Type>();
            var simples = new List<Type>();
            var classes = new List<Type>();
            var dictionaries = new List<Type>();
            var enumerables = new List<Type>();
            var tuples = new List<Type>();
            var arrays = new List<Type>();
            var valueTypes = new List<Type>();

            foreach (var item in types)
            {
                if (item.IsPrimitive)
                    primitives.Add(item);
                if (item.FullName.StartsWith("System.", StringComparison.Ordinal))
                    systems.Add(item);
                if (item.IsValueType)
                    valueTypes.Add(item);
                if (item.IsEnum)
                    enums.Add(item);
                if (item == typeof(string))
                    str.Add(item);
                if (simpleTypes.Contains(item))
                    simples.Add(item);
                if (item.IsClass)
                    classes.Add(item);
                if (item.IsDictionary())
                    dictionaries.Add(item);
                if (item.IsEnumerable() || item.IsCollection())
                    enumerables.Add(item);
                if (item.IsTuple())
                    tuples.Add(item);
                if (item.IsArray)
                    arrays.Add(item);
            }

            var final = new List<Type>();

            final.AddRange(primitives);
            final.AddRange(valueTypes);
            final.AddRange(enums);
            final.AddRange(str);
            final.AddRange(simples);
            final.AddRange(classes);
            final.AddRange(systems);
            final.AddRange(arrays);
            final.AddRange(tuples);
            final.AddRange(dictionaries);
            final.AddRange(enumerables);

            return final;
        }
    }
}
