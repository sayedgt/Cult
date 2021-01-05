using System;
using System.Globalization;
namespace Cult.PersianDateTime
{
    public static class PersianDateTimeExtensions
    {
        public static MD.PersianDateTime.Standard.PersianDateTime? ToPersianDateTime(this DateTime? dt)
        {
            if (!dt.HasValue)
                return null;
            return new MD.PersianDateTime.Standard.PersianDateTime(dt.Value);
        }
        public static MD.PersianDateTime.Standard.PersianDateTime ToPersianDateTime(this DateTime dt)
        {
            return new MD.PersianDateTime.Standard.PersianDateTime(dt);
        }
        public static string ToStringPersianDateTime(this DateTime dt)
        {
            return new MD.PersianDateTime.Standard.PersianDateTime(dt).ToString(CultureInfo.InvariantCulture);
        }
        public static string ToStringPersianDateTime(this DateTime? dt)
        {
            return !dt.HasValue ? string.Empty : new MD.PersianDateTime.Standard.PersianDateTime(dt).ToString(CultureInfo.InvariantCulture);
        }
    }
}
