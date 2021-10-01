using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

// ReSharper disable All 
namespace Cult.Toolkit.ExtraEnum
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum @enum, bool replaceNullWithEnumName = false)
        {
            return
                @enum
                    .GetType()
                    .GetMember(@enum.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description
                ?? (replaceNullWithEnumName ? null : @enum.ToString());
        }
        public static bool HasFlags<TEnum>(this TEnum @this, params TEnum[] flags)
                    where TEnum : Enum
        {
            foreach (var flag in flags)
            {
                if (!Enum.IsDefined(typeof(TEnum), flag))
                    return false;

                var numFlag = Convert.ToUInt64(flag);
                if ((Convert.ToUInt64(@this) & numFlag) != numFlag)
                    return false;
            }

            return true;
        }
        public static bool In(this Enum @this, params Enum[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }
        public static bool NotIn(this Enum @this, params Enum[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
    }
}
