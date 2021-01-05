// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Int128.cs">
//   Copyright (c) 2013 Alexander Logger. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
// ReSharper disable All

using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Cult.BigMath.Utils;

// ReSharper disable All 
namespace System
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 16)]
    public struct Int128 : IComparable<Int128>, IComparable, IEquatable<Int128>, IFormattable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [FieldOffset(0)] private ulong _lo;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [FieldOffset(8)] private ulong _hi;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return "0x" + ToString("X1"); }
        }

        private const ulong NegativeSignMask = 0x1UL << 63;

        public static Int128 Zero = GetZero();

        public static Int128 MaxValue = GetMaxValue();

        public static Int128 MinValue = GetMinValue();

        private static Int128 GetMaxValue()
        {
            return new Int128(long.MaxValue, ulong.MaxValue);
        }

        private static Int128 GetMinValue()
        {
            return -GetMaxValue();
        }

        private static Int128 GetZero()
        {
            return new Int128();
        }

        public Int128(byte value)
        {
            _hi = 0;
            _lo = value;
        }

        public Int128(bool value)
        {
            _hi = 0;
            _lo = (ulong) (value ? 1 : 0);
        }

        public Int128(char value)
        {
            _hi = 0;
            _lo = value;
        }
        
        public Int128(decimal value)
        {
            bool isNegative = value < 0;
            uint[] bits = decimal.GetBits(value).ConvertAll(i => (uint) i);
            uint scale = (bits[3] >> 16) & 0x1F;
            if (scale > 0)
            {
                uint[] quotient;
                uint[] reminder;
                MathUtils.DivModUnsigned(bits, new[] { 10U * scale }, out quotient, out reminder);

                bits = quotient;
            }

            _hi = bits[2];
            _lo = bits[0] | (ulong) bits[1] << 32;

            if (isNegative)
            {
                Negate();
            }
        }

        public Int128(double value) : this((decimal) value)
        {
        }

        public Int128(float value) : this((decimal) value)
        {
        }

        public Int128(short value) : this((int) value)
        {
        }

        public Int128(int value) : this((long) value)
        {
        }

        public Int128(long value)
        {
            _hi = unchecked((ulong) (value < 0 ? ~0 : 0));
            _lo = (ulong) value;
        }

        public Int128(sbyte value) : this((long) value)
        {
        }

        public Int128(ushort value)
        {
            _hi = 0;
            _lo = value;
        }

        public Int128(uint value)
        {
            _hi = 0;
            _lo = value;
        }

        public Int128(ulong value)
        {
            _hi = 0;
            _lo = value;
        }

        public Int128(Guid value)
        {
            var int128 = value.ToByteArray().ToInt128(0);
            _hi = int128.High;
            _lo = int128.Low;
        }

        public Int128(Int256 value)
        {
            ulong[] values = value.ToUIn64Array();
            _hi = values[1];
            _lo = values[0];
        }

        public Int128(ulong hi, ulong lo)
        {
            _hi = hi;
            _lo = lo;
        }

        public Int128(int sign, uint[] ints)
        {
            if (ints == null)
            {
                throw new ArgumentNullException("ints");
            }

            var value = new ulong[2];
            for (int i = 0; i < ints.Length && i < 4; i++)
            {
                Buffer.BlockCopy(ints[i].ToBytes(), 0, value, i*4, 4);
            }

            _hi = value[1];
            _lo = value[0];

            if (sign < 0 && (_hi > 0 || _lo > 0))
            {
                // We use here two's complement numbers representation,
                // hence such operations for negative numbers.
                Negate();
                _hi |= NegativeSignMask; // Ensure negative sign.
            }
        }

        public ulong High
        {
            get { return _hi; }
        }

        public ulong Low
        {
            get { return _lo; }
        }

        public int Sign
        {
            get
            {
                if (_hi == 0 && _lo == 0)
                {
                    return 0;
                }

                return ((_hi & NegativeSignMask) == 0) ? 1 : -1;
            }
        }

        public override int GetHashCode()
        {
            return _hi.GetHashCode() ^ _lo.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(Int128 obj)
        {
            return _hi == obj._hi && _lo == obj._lo;
        }

        public override string ToString()
        {
            return ToString(null, null);
        }

        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (formatProvider == null)
            {
                formatProvider = CultureInfo.CurrentCulture;
            }

            if (!string.IsNullOrEmpty(format))
            {
                char ch = format[0];
                if ((ch == 'x') || (ch == 'X'))
                {
                    int min;
                    int.TryParse(format.Substring(1).Trim(), out min);
                    return this.ToBytes(false).ToHexString(ch == 'X', min, trimZeros: true);
                }

                if (((ch != 'G') && (ch != 'g')) && ((ch != 'D') && (ch != 'd')))
                {
                    throw new NotSupportedException("Not supported format: " + format);
                }
            }

            return ToString((NumberFormatInfo) formatProvider.GetFormat(typeof (NumberFormatInfo)));
        }

        private string ToString(NumberFormatInfo info)
        {
            if (Sign == 0)
            {
                return "0";
            }

            var sb = new StringBuilder();
            var ten = new Int128(10);
            Int128 current = Sign < 0 ? -this : this;
            Int128 r;
            while (true)
            {
                current = DivRem(current, ten, out r);
                if (r._lo > 0 || current.Sign != 0 || (sb.Length == 0))
                {
                    sb.Insert(0, (char) ('0' + r._lo));
                }
                if (current.Sign == 0)
                {
                    break;
                }
            }

            string s = sb.ToString();
            if ((Sign < 0) && (s != "0"))
            {
                return info.NegativeSign + s;
            }

            return s;
        }

        public bool TryConvert(Type conversionType, IFormatProvider provider, bool asLittleEndian, out object value)
        {
            if (conversionType == typeof (bool))
            {
                value = (bool) this;
                return true;
            }

            if (conversionType == typeof (byte))
            {
                value = (byte) this;
                return true;
            }

            if (conversionType == typeof (char))
            {
                value = (char) this;
                return true;
            }

            if (conversionType == typeof (decimal))
            {
                value = (decimal) this;
                return true;
            }

            if (conversionType == typeof (double))
            {
                value = (double) this;
                return true;
            }

            if (conversionType == typeof (short))
            {
                value = (short) this;
                return true;
            }

            if (conversionType == typeof (int))
            {
                value = (int) this;
                return true;
            }

            if (conversionType == typeof (long))
            {
                value = (long) this;
                return true;
            }

            if (conversionType == typeof (sbyte))
            {
                value = (sbyte) this;
                return true;
            }

            if (conversionType == typeof (float))
            {
                value = (float) this;
                return true;
            }

            if (conversionType == typeof (string))
            {
                value = ToString(null, provider);
                return true;
            }

            if (conversionType == typeof (ushort))
            {
                value = (ushort) this;
                return true;
            }

            if (conversionType == typeof (uint))
            {
                value = (uint) this;
                return true;
            }

            if (conversionType == typeof (ulong))
            {
                value = (ulong) this;
                return true;
            }

            if (conversionType == typeof (byte[]))
            {
                value = this.ToBytes(asLittleEndian);
                return true;
            }

            if (conversionType == typeof (Guid))
            {
                value = new Guid(this.ToBytes(asLittleEndian));
                return true;
            }

            if (conversionType == typeof (Int256))
            {
                value = (Int256) this;
                return true;
            }

            value = null;
            return false;
        }

        public static Int128 Parse(string value)
        {
            return Parse(value, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
        }

        public static Int128 Parse(string value, NumberStyles style)
        {
            return Parse(value, style, NumberFormatInfo.CurrentInfo);
        }

        public static Int128 Parse(string value, IFormatProvider provider)
        {
            return Parse(value, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
        }

        public static Int128 Parse(string value, NumberStyles style, IFormatProvider provider)
        {
            Int128 result;
            if (!TryParse(value, style, provider, out result))
            {
                throw new ArgumentException(null, "value");
            }

            return result;
        }

        public static bool TryParse(string value, out Int128 result)
        {
            return TryParse(value, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
        }

        public static bool TryParse(string value, NumberStyles style, IFormatProvider provider, out Int128 result)
        {
            result = Zero;
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (value.StartsWith("x", StringComparison.OrdinalIgnoreCase))
            {
                style |= NumberStyles.AllowHexSpecifier;
                value = value.Substring(1);
            }
            else if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                style |= NumberStyles.AllowHexSpecifier;
                value = value.Substring(2);
            }

            if ((style & NumberStyles.AllowHexSpecifier) == NumberStyles.AllowHexSpecifier)
            {
                return TryParseHex(value, out result);
            }

            return TryParseNum(value, out result);
        }

        private static bool TryParseHex(string value, out Int128 result)
        {
            if (value.Length > 32)
            {
                throw new OverflowException();
            }

            result = Zero;
            bool hi = false;
            int pos = 0;
            for (int i = value.Length - 1; i >= 0; i--)
            {
                char ch = value[i];
                ulong b;
                if ((ch >= '0') && (ch <= '9'))
                {
                    b = (ulong) (ch - '0');
                }
                else if ((ch >= 'A') && (ch <= 'F'))
                {
                    b = (ulong) (ch - 'A' + 10);
                }
                else if ((ch >= 'a') && (ch <= 'f'))
                {
                    b = (ulong) (ch - 'a' + 10);
                }
                else
                {
                    return false;
                }

                if (hi)
                {
                    result._hi |= b << pos;
                    pos += 4;
                }
                else
                {
                    result._lo |= b << pos;
                    pos += 4;
                    if (pos == 64)
                    {
                        pos = 0;
                        hi = true;
                    }
                }
            }
            return true;
        }

        private static bool TryParseNum(string value, out Int128 result)
        {
            result = Zero;
            foreach (char ch in value)
            {
                byte b;
                if ((ch >= '0') && (ch <= '9'))
                {
                    b = (byte) (ch - '0');
                }
                else
                {
                    return false;
                }

                result = 10*result;
                result += b;
            }
            return true;
        }

        public object ToType(Type conversionType, IFormatProvider provider, bool asLittleEndian)
        {
            object value;
            if (TryConvert(conversionType, provider, asLittleEndian, out value))
            {
                return value;
            }

            throw new InvalidCastException();
        }

        int IComparable.CompareTo(object obj)
        {
            return Compare(this, obj);
        }

        public static int Compare(Int128 left, object right)
        {
            if (right is Int128)
            {
                return Compare(left, (Int128) right);
            }

            // NOTE: this could be optimized type per type
            if (right is bool)
            {
                return Compare(left, new Int128((bool) right));
            }

            if (right is byte)
            {
                return Compare(left, new Int128((byte) right));
            }

            if (right is char)
            {
                return Compare(left, new Int128((char) right));
            }

            if (right is decimal)
            {
                return Compare(left, new Int128((decimal) right));
            }

            if (right is double)
            {
                return Compare(left, new Int128((double) right));
            }

            if (right is short)
            {
                return Compare(left, new Int128((short) right));
            }

            if (right is int)
            {
                return Compare(left, new Int128((int) right));
            }

            if (right is long)
            {
                return Compare(left, new Int128((long) right));
            }

            if (right is sbyte)
            {
                return Compare(left, new Int128((sbyte) right));
            }

            if (right is float)
            {
                return Compare(left, new Int128((float) right));
            }

            if (right is ushort)
            {
                return Compare(left, new Int128((ushort) right));
            }

            if (right is uint)
            {
                return Compare(left, new Int128((uint) right));
            }

            if (right is ulong)
            {
                return Compare(left, new Int128((ulong) right));
            }

            var bytes = right as byte[];
            if ((bytes != null) && (bytes.Length == 16))
            {
                // TODO: ensure endian.
                return Compare(left, bytes.ToInt128(0));
            }

            if (right is Guid)
            {
                return Compare(left, new Int128((Guid) right));
            }

            throw new ArgumentException();
        }

        public static int Compare(Int128 left, Int128 right)
        {
            int leftSign = left.Sign;
            int rightSign = right.Sign;

            if (leftSign == 0 && rightSign == 0)
            {
                return 0;
            }

            if (leftSign >= 0 && rightSign < 0)
            {
                return 1;
            }

            if (leftSign < 0 && rightSign >= 0)
            {
                return -1;
            }

            if (left._hi != right._hi)
            {
                return left._hi.CompareTo(right._hi);
            }

            return left._lo.CompareTo(right._lo);
        }

        public int CompareTo(Int128 value)
        {
            return Compare(this, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Not()
        {
            _hi = ~_hi;
            _lo = ~_lo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Negate()
        {
            Not();
            this++;
        }

        public static Int128 Negate(Int128 value)
        {
            value.Negate();
            return value;
        }

        public Int128 ToAbs()
        {
            return Abs(this);
        }

        public static Int128 Abs(Int128 value)
        {
            if (value.Sign < 0)
            {
                return -value;
            }

            return value;
        }

        public static Int128 Add(Int128 left, Int128 right)
        {
            return left + right;
        }

        public static Int128 Subtract(Int128 left, Int128 right)
        {
            return left - right;
        }

        public static Int128 Divide(Int128 dividend, Int128 divisor)
        {
            Int128 integer;
            return DivRem(dividend, divisor, out integer);
        }

        public static Int128 DivRem(Int128 dividend, Int128 divisor, out Int128 remainder)
        {
            if (divisor == 0)
            {
                throw new DivideByZeroException();
            }
            int dividendSign = dividend.Sign;
            dividend = dividendSign < 0 ? -dividend : dividend;
            int divisorSign = divisor.Sign;
            divisor = divisorSign < 0 ? -divisor : divisor;

            uint[] quotient;
            uint[] rem;
            MathUtils.DivModUnsigned(dividend.ToUIn32Array(), divisor.ToUIn32Array(), out quotient, out rem);
            remainder = new Int128(1, rem);
            return new Int128(dividendSign*divisorSign, quotient);
        }

        public static Int128 Remainder(Int128 dividend, Int128 divisor)
        {
            Int128 remainder;
            DivRem(dividend, divisor, out remainder);
            return remainder;
        }

        public ulong[] ToUIn64Array()
        {
            return new[] {_lo, _hi};
        }

        public uint[] ToUIn32Array()
        {
            var ints = new uint[4];
            ulong[] ulongs = ToUIn64Array();
            Buffer.BlockCopy(ulongs, 0, ints, 0, 16);
            return ints;
        }

        public static Int128 Multiply(Int128 left, Int128 right)
        {
            int leftSign = left.Sign;
            left = leftSign < 0 ? -left : left;
            int rightSign = right.Sign;
            right = rightSign < 0 ? -right : right;

            uint[] xInts = left.ToUIn32Array();
            uint[] yInts = right.ToUIn32Array();
            var mulInts = new uint[8];

            for (int i = 0; i < xInts.Length; i++)
            {
                int index = i;
                ulong remainder = 0;
                foreach (uint yi in yInts)
                {
                    remainder = remainder + (ulong) xInts[i]*yi + mulInts[index];
                    mulInts[index++] = (uint) remainder;
                    remainder = remainder >> 32;
                }

                while (remainder != 0)
                {
                    remainder += mulInts[index];
                    mulInts[index++] = (uint) remainder;
                    remainder = remainder >> 32;
                }
            }
            return new Int128(leftSign*rightSign, mulInts);
        }

        public static implicit operator Int128(bool value)
        {
            return new Int128(value);
        }

        public static implicit operator Int128(byte value)
        {
            return new Int128(value);
        }

        public static implicit operator Int128(char value)
        {
            return new Int128(value);
        }

        public static explicit operator Int128(decimal value)
        {
            return new Int128(value);
        }

        public static explicit operator Int128(double value)
        {
            return new Int128(value);
        }

        public static implicit operator Int128(short value)
        {
            return new Int128(value);
        }

        public static implicit operator Int128(int value)
        {
            return new Int128(value);
        }

        public static implicit operator Int128(long value)
        {
            return new Int128(value);
        }

        public static implicit operator Int128(sbyte value)
        {
            return new Int128(value);
        }

        public static explicit operator Int128(float value)
        {
            return new Int128(value);
        }

        public static implicit operator Int128(ushort value)
        {
            return new Int128(value);
        }

        public static implicit operator Int128(uint value)
        {
            return new Int128(value);
        }

        public static implicit operator Int128(ulong value)
        {
            return new Int128(value);
        }

        public static explicit operator Int128(Int256 value)
        {
            return new Int128(value);
        }

        public static explicit operator Int256(Int128 value)
        {
            return new Int256(value);
        }

        public static explicit operator bool(Int128 value)
        {
            return value.Sign != 0;
        }

        public static explicit operator byte(Int128 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if ((value < byte.MinValue) || (value > byte.MaxValue))
            {
                throw new OverflowException();
            }

            return (byte) value._lo;
        }

        public static explicit operator char(Int128 value)
        {
            if (value.Sign == 0)
            {
                return (char) 0;
            }

            if ((value < char.MinValue) || (value > char.MaxValue))
            {
                throw new OverflowException();
            }

            return (char) (ushort) value._lo;
        }

        public static explicit operator decimal(Int128 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            return new decimal((int) (value._lo & 0xFFFFFFFF), (int) (value._lo >> 32), (int) (value._hi & 0xFFFFFFFF), value.Sign < 0, 0);
        }

        public static explicit operator double(Int128 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            double d;
            NumberFormatInfo nfi = CultureInfo.InvariantCulture.NumberFormat;
            if (!double.TryParse(value.ToString(nfi), NumberStyles.Number, nfi, out d))
            {
                throw new OverflowException();
            }

            return d;
        }

        public static explicit operator float(Int128 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            float f;
            NumberFormatInfo nfi = CultureInfo.InvariantCulture.NumberFormat;
            if (!float.TryParse(value.ToString(nfi), NumberStyles.Number, nfi, out f))
            {
                throw new OverflowException();
            }

            return f;
        }

        public static explicit operator short(Int128 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if ((value < short.MinValue) || (value > short.MaxValue))
            {
                throw new OverflowException();
            }

            return (short) value._lo;
        }

        public static explicit operator int(Int128 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if ((value < int.MinValue) || (value > int.MaxValue))
            {
                throw new OverflowException();
            }

            return (int) value._lo;
        }

        public static explicit operator long(Int128 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if ((value < long.MinValue) || (value > long.MaxValue))
            {
                throw new OverflowException();
            }

            return (long) value._lo;
        }

        public static explicit operator uint(Int128 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if ((value < uint.MinValue) || (value > uint.MaxValue))
            {
                throw new OverflowException();
            }

            return (uint) value._lo;
        }

        public static explicit operator ushort(Int128 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if ((value < ushort.MinValue) || (value > ushort.MaxValue))
            {
                throw new OverflowException();
            }

            return (ushort) value._lo;
        }

        public static explicit operator ulong(Int128 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if ((value < ulong.MinValue) || (value > ulong.MaxValue))
            {
                throw new OverflowException();
            }

            return value._lo;
        }

        public static bool operator >(Int128 left, Int128 right)
        {
            return Compare(left, right) > 0;
        }

        public static bool operator <(Int128 left, Int128 right)
        {
            return Compare(left, right) < 0;
        }

        public static bool operator >=(Int128 left, Int128 right)
        {
            return Compare(left, right) >= 0;
        }

        public static bool operator <=(Int128 left, Int128 right)
        {
            return Compare(left, right) <= 0;
        }

        public static bool operator !=(Int128 left, Int128 right)
        {
            return Compare(left, right) != 0;
        }

        public static bool operator ==(Int128 left, Int128 right)
        {
            return Compare(left, right) == 0;
        }

        public static Int128 operator +(Int128 value)
        {
            return value;
        }

        public static Int128 operator -(Int128 value)
        {
            return Negate(value);
        }

        public static Int128 operator +(Int128 left, Int128 right)
        {
            left._hi += right._hi;
            left._lo += right._lo;

            if (left._lo < right._lo)
            {
                left._hi++;
            }

            return left;
        }

        public static Int128 operator -(Int128 left, Int128 right)
        {
            return left + -right;
        }


        public static Int128 operator %(Int128 dividend, Int128 divisor)
        {
            return Remainder(dividend, divisor);
        }

        public static Int128 operator /(Int128 dividend, Int128 divisor)
        {
            return Divide(dividend, divisor);
        }

        public static Int128 operator *(Int128 left, Int128 right)
        {
            return Multiply(left, right);
        }

        public static Int128 operator >>(Int128 value, int shift)
        {
            if (shift == 0)
            {
                return value;
            }

            ulong[] bits = MathUtils.ShiftRightSigned(value.ToUIn64Array(), shift);
            value._hi = bits[1];
            value._lo = bits[0];    //lo is stored in array entry 0

            return value;
        }

        public static Int128 operator <<(Int128 value, int shift)
        {
            if (shift == 0)
            {
                return value;
            }

            ulong[] bits = MathUtils.ShiftLeft(value.ToUIn64Array(), shift);
            value._hi = bits[1];
            value._lo = bits[0];    //lo is stored in array entry 0

            return value;
        }

        public static Int128 operator |(Int128 left, Int128 right)
        {
            if (left == 0)
            {
                return right;
            }

            if (right == 0)
            {
                return left;
            }

            Int128 result = left;
            result._hi |= right._hi;
            result._lo |= right._lo;
            return result;
        }

        public static Int128 operator &(Int128 left, Int128 right)
        {
            if (left == 0 || right == 0)
            {
                return Zero;
            }

            Int128 result = left;
            result._hi &= right._hi;
            result._lo &= right._lo;
            return result;
        }

        public static Int128 operator ~(Int128 value)
        {
            return new Int128(~value._hi, ~value._lo);
        }

        public static Int128 operator ++(Int128 value)
        {
            return value + 1;
        }

        public static Int128 operator --(Int128 value)
        {
            return value - 1;
        }
    }
}
