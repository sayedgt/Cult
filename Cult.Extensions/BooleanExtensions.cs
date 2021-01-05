using System;
// ReSharper disable All 
namespace Cult.Extensions
{
    public static class BooleanExtensions
    {
        public static void IfFalse(this bool @this, Action action)
        {
            if (!@this)
            {
                action();
            }
        }
        public static void IfTrue(this bool @this, Action action)
        {
            if (@this)
            {
                action();
            }
        }
        public static byte ToBinary(this bool @this)
        {
            return Convert.ToByte(@this);
        }
        public static char ToSpecificChar(this bool @this, char trueValue, char falseValue)
        {
            return @this ? trueValue : falseValue;
        }
        public static string ToSpecificString(this bool @this, string trueValue, string falseValue)
        {
            return @this ? trueValue : falseValue;
        }
    }
}
