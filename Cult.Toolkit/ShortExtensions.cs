using System;
using System.Data;
using System.Net;

namespace Cult.Toolkit.ExtraShort
{
    public static class ShortExtensions
    {
        public static short Abs(this short value)
        {
            return Math.Abs(value);
        }

        public static bool Between(this short @this, short minValue, short maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        public static TimeSpan Days(this short @this)
        {
            return TimeSpan.FromDays(@this);
        }

        public static bool FactorOf(this short @this, short factorNumer)
        {
            return factorNumer % @this == 0;
        }

        public static byte[] GetBytes(this short value)
        {
            return BitConverter.GetBytes(value);
        }

        public static short HostToNetworkOrder(this short host)
        {
            return IPAddress.HostToNetworkOrder(host);
        }

        public static TimeSpan Hours(this short @this)
        {
            return TimeSpan.FromHours(@this);
        }

        public static bool In(this short @this, params short[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        public static bool InRange(this short @this, short minValue, short maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        public static bool IsEven(this short @this)
        {
            return @this % 2 == 0;
        }

        public static bool IsMultipleOf(this short @this, short factor)
        {
            return @this % factor == 0;
        }

        public static bool IsOdd(this short @this)
        {
            return @this % 2 != 0;
        }

        public static bool IsPrime(this short @this)
        {
            if (@this == 1 || @this == 2)
            {
                return true;
            }

            if (@this % 2 == 0)
            {
                return false;
            }

            var sqrt = (short)Math.Sqrt(@this);
            for (long t = 3; t <= sqrt; t = t + 2)
            {
                if (@this % t == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static short Max(this short val1, short val2)
        {
            return Math.Max(val1, val2);
        }

        public static TimeSpan Milliseconds(this short @this)
        {
            return TimeSpan.FromMilliseconds(@this);
        }

        public static short Min(this short val1, short val2)
        {
            return Math.Min(val1, val2);
        }

        public static TimeSpan Minutes(this short @this)
        {
            return TimeSpan.FromMinutes(@this);
        }

        public static short NetworkToHostOrder(this short network)
        {
            return IPAddress.NetworkToHostOrder(network);
        }

        public static bool NotIn(this short @this, params short[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }

        public static TimeSpan Seconds(this short @this)
        {
            return TimeSpan.FromSeconds(@this);
        }

        public static int Sign(this short value)
        {
            return Math.Sign(value);
        }

        public static SqlDbType SqlSystemTypeToSqlDbType(this short @this)
        {
            switch (@this)
            {
                case 34: // 34 | "image" | SqlDbType.Image
                    return SqlDbType.Image;

                case 35: // 35 | "text" | SqlDbType.Text
                    return SqlDbType.Text;

                case 36: // 36 | "uniqueidentifier" | SqlDbType.UniqueIdentifier
                    return SqlDbType.UniqueIdentifier;

                case 40: // 40 | "date" | SqlDbType.Date
                    return SqlDbType.Date;

                case 41: // 41 | "time" | SqlDbType.Time
                    return SqlDbType.Time;

                case 42: // 42 | "datetime2" | SqlDbType.DateTime2
                    return SqlDbType.DateTime2;

                case 43: // 43 | "datetimeoffset" | SqlDbType.DateTimeOffset
                    return SqlDbType.DateTimeOffset;

                case 48: // 48 | "tinyint" | SqlDbType.TinyInt
                    return SqlDbType.TinyInt;

                case 52: // 52 | "smallint" | SqlDbType.SmallInt
                    return SqlDbType.SmallInt;

                case 56: // 56 | "int" | SqlDbType.Int
                    return SqlDbType.Int;

                case 58: // 58 | "smalldatetime" | SqlDbType.SmallDateTime
                    return SqlDbType.SmallDateTime;

                case 59: // 59 | "real" | SqlDbType.Real
                    return SqlDbType.Real;

                case 60: // 60 | "money" | SqlDbType.Money
                    return SqlDbType.Money;

                case 61: // 61 | "datetime" | SqlDbType.DateTime
                    return SqlDbType.DateTime;

                case 62: // 62 | "float" | SqlDbType.Float
                    return SqlDbType.Float;

                case 98: // 98 | "sql_variant" | SqlDbType.Variant
                    return SqlDbType.Variant;

                case 99: // 99 | "ntext" | SqlDbType.NText
                    return SqlDbType.NText;

                case 104: // 104 | "bit" | SqlDbType.Bit
                    return SqlDbType.Bit;

                case 106: // 106 | "decimal" | SqlDbType.Decimal
                    return SqlDbType.Decimal;

                case 108: // 108 | "numeric" | SqlDbType.Decimal
                    return SqlDbType.Decimal;

                case 122: // 122 | "smallmoney" | SqlDbType.SmallMoney
                    return SqlDbType.SmallMoney;

                case 127: // 127 | "bigint" | SqlDbType.BigInt
                    return SqlDbType.BigInt;

                case 165: // 165 | "varbinary" | SqlDbType.VarBinary
                    return SqlDbType.VarBinary;

                case 167: // 167 | "varchar" | SqlDbType.VarChar
                    return SqlDbType.VarChar;

                case 173: // 173 | "binary" | SqlDbType.Binary
                    return SqlDbType.Binary;

                case 175: // 175 | "char" | SqlDbType.Char
                    return SqlDbType.Char;

                case 189: // 189 | "timestamp" | SqlDbType.Timestamp
                    return SqlDbType.Timestamp;

                case 231: // 231 | "nvarchar", "sysname" | SqlDbType.NVarChar
                    return SqlDbType.NVarChar;

                case 239: // 239 | "nchar" | SqlDbType.NChar
                    return SqlDbType.NChar;

                case 240: // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                    return SqlDbType.Udt;

                case 241: // 241 | "xml" | SqlDbType.Xml
                    return SqlDbType.Xml;

                default:
                    throw new Exception(
                        $"Unsupported Type: {@this}. Please let us know about this type and we will support it: sales@zzzprojects.com");
            }
        }

        public static TimeSpan Weeks(this short @this)
        {
            return TimeSpan.FromDays(@this * 7);
        }
    }
}
