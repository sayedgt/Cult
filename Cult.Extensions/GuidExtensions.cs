using System;
// ReSharper disable All 
namespace Cult.Extensions.ExtraGuid
{
    public static class GuidExtensions
    {
        public static bool In(this Guid @this, params Guid[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }
        public static bool IsEmpty(this Guid @this)
        {
            return @this == Guid.Empty;
        }
        public static bool IsNotEmpty(this Guid @this)
        {
            return @this != Guid.Empty;
        }
        public static bool NotIn(this Guid @this, params Guid[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
    }
}
