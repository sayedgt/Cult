using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable All 
namespace Cult.Utilities
{
    public static class EnumUtility
    {
        private static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
        public static IEnumerable<string> GetNames<TEnum>() where TEnum : Enum
        {
            return Enum.GetNames(typeof(TEnum));
        }
        public static IEnumerable<TEnum> GetValues<TEnum>() where TEnum : Enum
        {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }
        public static IEnumerable<string> GetValuesAsString<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select(e => e.ToString());
        }
        public static bool IsDefined<TEnum>(string name) where TEnum : Enum
        {
            return Enum.IsDefined(typeof(TEnum), name);
        }
        public static bool IsDefined<TEnum>(TEnum value) where TEnum : Enum
        {
            return Enum.IsDefined(typeof(TEnum), value);
        }
        public static bool ContainsName<TEnum>(string name, bool ignoreCase = false) where TEnum : Enum
        {
            var stringComparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            foreach (var item in GetNames<TEnum>())
            {
                if (item.Contains(name, stringComparison))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool ContainsValue<TEnum>(string value, bool ignoreCase = false) where TEnum : Enum
        {
            var stringComparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            foreach (var item in GetValuesAsString<TEnum>())
            {
                if (item.Contains(value, stringComparison))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool ContainsValue<TEnum>(TEnum value) where TEnum : Enum
        {
            return GetValues<TEnum>().Contains(value);
        }
    }
}
