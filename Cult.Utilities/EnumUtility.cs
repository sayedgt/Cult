using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable All

namespace Cult.Utilities
{
    public static class EnumUtility
    {
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
    }
}
