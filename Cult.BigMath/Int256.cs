// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Int256.cs">
//   Copyright (c) 2013 Alexander Logger. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Cult.BigMath.Utils;

// ReSharper disable All

// ReSharper disable All 
namespace System
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 32)]
    public struct Int256 : IComparable<Int256>, IComparable, IEquatable<Int256>, IFormattable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [FieldOffset(0)] private ulong _d;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [FieldOffset(8)] private ulong _c;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [FieldOffset(16)] private ulong _b;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [FieldOffset(32)] private ulong _a;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return "0x" + ToString("X1"); }
        }

        private const ulong NegativeSignMask = 0x1UL << 63;

        public static Int256 Zero = GetZero();

        public static Int256 MaxValue = GetMaxValue();

        public static Int256 MinValue = GetMinValue();

        private static Int256 GetMaxValue()
        {
            return new Int256(long.MaxValue, ulong.MaxValue, ulong.MaxValue, ulong.MaxValue);
        }

        private static Int256 GetMinValue()
        {
            return -GetMaxValue();
        }

        private static Int256 GetZero()
        {
            return new Int256();
        }

        public Int256(byte value)
        {
            _a = 0;
            _b = 0;
            _c = 0;
            _d = value;
        }

        public Int256(bool value)
        {
            _a = 0;
            _b = 0;
            _c = 0;
            _d = (ulong) (value ? 1 : 0);
        }

        public Int256(char value)
        {
            _a = 0;
            _b = 0;
            _c = 0;
            _d = value;
        }

        public Int256(decimal value)
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

            _a = 0;
            _b = 0;
            _c = bits[2];
            _d = bits[0] | (ulong) bits[1] << 32;

            if (isNegative)
            {
                Negate();
            }
        }

        public Int256(double value) : this((decimal) value)
        {
        }

        public Int256(float value) : this((decimal) value)
        {
        }

        public Int256(short value) : this((int) value)
        {
        }

        public Int256(int value) : this((long) value)
        {
        }

        public Int256(long value)
        {
            _a = _b = _c = unchecked((ulong) (value < 0 ? ~0 : 0));
            _d = (ulong) value;
        }

        public Int256(sbyte value) : this((long) value)
        {
        }

        public Int256(ushort value)
        {
            _a = 0;
            _b = 0;
            _c = 0;
            _d = value;
        }

        public Int256(uint value)
        {
            _a = 0;
            _b = 0;
            _c = 0;
            _d = value;
        }

        public Int256(ulong value)
        {
            _a = 0;
            _b = 0;
            _c = 0;
            _d = value;
        }

        public Int256(Guid value)
        {
            var int256 = value.ToByteArray().ToInt256(0);
            _a = int256.A;
            _b = int256.B;
            _c = int256.C;
            _d = int256.D;
        }

        public Int256(Int128 value)
        {
            ulong[] values = value.ToUIn64Array();
            _a = _b = unchecked((ulong) (value.Sign < 0 ? ~0 : 0));
            _c = values[1];
            _d = values[0];
        }

        public Int256(ulong a, ulong b, ulong c, ulong d)
        {
            _a = a;
            _b = b;
            _c = c;
            _d = d;
        }

        public Int256(int sign, uint[] ints)
        {
            if (ints == null)
            {
                throw new ArgumentNullException("ints");
            }

            var value = new ulong[4];
            for (int i = 0; i < ints.Length && i < 8; i++)
            {
                Buffer.BlockCopy(ints[i].ToBytes(), 0, value, i*4, 4);
            }

            _a = value[3];
            _b = value[2];
            _c = value[1];
            _d = value[0];

            if (sign < 0 && (_d > 0 || _c > 0 || _b > 0 || _a > 0))
            {
                // We use here two's complement numbers representation,
                // hence such operations for negative numbers.
                Negate();
                _a |= NegativeSignMask; // Ensure negative sign.
            }
        }

        public ulong A
        {
            get { return _a; }
        }

        public ulong B
        {
            get { return _b; }
        }

        public ulong C
        {
            get { return _c; }
        }

        public ulong D
        {
            get { return _d; }
        }

        public int Sign
        {
            get
            {
                if (_a == 0 && _b == 0 && _c == 0 && _d == 0)
                {
                    return 0;
                }

                return ((_a & NegativeSignMask) == 0) ? 1 : -1;
            }
        }

        public override int GetHashCode()
        {
            return _a.GetHashCode() ^ _b.GetHashCode() ^ _c.GetHashCode() ^ _d.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(Int256 obj)
        {
            return _a == obj._a && _b == obj._b && _c == obj._c && _d == obj._d;
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
            var ten = new Int256(10);
            Int256 current = Sign < 0 ? -this : this;
            while (true)
            {
                Int256 r;
                current = DivRem(current, ten, out r);
                if (r._d > 0 || current.Sign != 0 || (sb.Length == 0))
                {
                    sb.Insert(0, (char) ('0' + r._d));
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

            if (conversionType == typeof (Int128))
            {
                value = (Int128) this;
                return true;
            }

            value = null;
            return false;
        }

        public static Int256 Parse(string value)
        {
            return Parse(value, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
        }

        public static Int256 Parse(string value, NumberStyles style)
        {
            return Parse(value, style, NumberFormatInfo.CurrentInfo);
        }

        public static Int256 Parse(string value, IFormatProvider provider)
        {
            return Parse(value, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
        }

        public static Int256 Parse(string value, NumberStyles style, IFormatProvider provider)
        {
            Int256 result;
            if (!TryParse(value, style, provider, out result))
            {
                throw new ArgumentException(null, "value");
            }

            return result;
        }

        public static bool TryParse(string value, out Int256 result)
        {
            return TryParse(value, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
        }

        public static bool TryParse(string value, NumberStyles style, IFormatProvider provider, out Int256 result)
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

        private static bool TryParseHex(string value, out Int256 result)
        {
            if (value.Length > 64)
            {
                throw new OverflowException();
            }

            result = Zero;
            int pos = 0;
            for (int i = value.Length - 1; i >= 0; i--)
            {
                char ch = value[i];
                ulong bch;
                if ((ch >= '0') && (ch <= '9'))
                {
                    bch = (ulong) (ch - '0');
                }
                else if ((ch >= 'A') && (ch <= 'F'))
                {
                    bch = (ulong) (ch - 'A' + 10);
                }
                else if ((ch >= 'a') && (ch <= 'f'))
                {
                    bch = (ulong) (ch - 'a' + 10);
                }
                else
                {
                    return false;
                }

                if (pos < 64)
                {
                    result._d |= bch << pos;
                }
                else if (pos < 128)
                {
                    result._c |= bch << pos;
                }
                else if (pos < 192)
                {
                    result._b |= bch << pos;
                }
                else if (pos < 256)
                {
                    result._a |= bch << pos;
                }
                pos += 4;
            }
            return true;
        }

        private static bool TryParseNum(string value, out Int256 result)
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

        public static int Compare(Int256 left, object right)
        {
            if (right is Int256)
            {
                return Compare(left, (Int256) right);
            }

            // NOTE: this could be optimized type per type
            if (right is bool)
            {
                return Compare(left, new Int256((bool) right));
            }

            if (right is byte)
            {
                return Compare(left, new Int256((byte) right));
            }

            if (right is char)
            {
                return Compare(left, new Int256((char) right));
            }

            if (right is decimal)
            {
                return Compare(left, new Int256((decimal) right));
            }

            if (right is double)
            {
                return Compare(left, new Int256((double) right));
            }

            if (right is short)
            {
                return Compare(left, new Int256((short) right));
            }

            if (right is int)
            {
                return Compare(left, new Int256((int) right));
            }

            if (right is long)
            {
                return Compare(left, new Int256((long) right));
            }

            if (right is sbyte)
            {
                return Compare(left, new Int256((sbyte) right));
            }

            if (right is float)
            {
                return Compare(left, new Int256((float) right));
            }

            if (right is ushort)
            {
                return Compare(left, new Int256((ushort) right));
            }

            if (right is uint)
            {
                return Compare(left, new Int256((uint) right));
            }

            if (right is ulong)
            {
                return Compare(left, new Int256((ulong) right));
            }

            var bytes = right as byte[];
            if ((bytes != null) && (bytes.Length == 32))
            {
                // TODO: ensure endian.
                return Compare(left, bytes.ToInt256(0));
            }

            if (right is Guid)
            {
                return Compare(left, new Int256((Guid) right));
            }

            throw new ArgumentException();
        }

        public static int Compare(Int256 left, Int256 right)
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

            if (left._a != right._a)
            {
                return left._a.CompareTo(right._a);
            }
            if (left._b != right._b)
            {
                return left._b.CompareTo(right._b);
            }
            if (left._c != right._c)
            {
                return left._c.CompareTo(right._c);
            }

            return left._d.CompareTo(right._d);
        }

        public int CompareTo(Int256 value)
        {
            return Compare(this, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Not()
        {
            _a = ~_a;
            _b = ~_b;
            _c = ~_c;
            _d = ~_d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Negate()
        {
            Not();
            this++;
        }

        public static Int256 Negate(Int256 value)
        {
            value.Negate();
            return value;
        }

        public Int256 ToAbs()
        {
            return Abs(this);
        }

        public static Int256 Abs(Int256 value)
        {
            if (value.Sign < 0)
            {
                return -value;
            }

            return value;
        }

        public static Int256 Add(Int256 left, Int256 right)
        {
            return left + right;
        }

        public static Int256 Subtract(Int256 left, Int256 right)
        {
            return left - right;
        }

        public static Int256 Divide(Int256 dividend, Int256 divisor)
        {
            Int256 integer;
            return DivRem(dividend, divisor, out integer);
        }

        public static Int256 Remainder(Int256 dividend, Int256 divisor)
        {
            Int256 remainder;
            DivRem(dividend, divisor, out remainder);
            return remainder;
        }

        public static Int256 DivRem(Int256 dividend, Int256 divisor, out Int256 remainder)
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
            remainder = new Int256(1, rem);
            return new Int256(dividendSign*divisorSign, quotient);
        }

        public ulong[] ToUIn64Array()
        {
            return new[] {_d, _c, _b, _a};
        }

        public uint[] ToUIn32Array()
        {
            var ints = new uint[8];
            ulong[] ulongs = ToUIn64Array();
            Buffer.BlockCopy(ulongs, 0, ints, 0, 32);
            return ints;
        }

        public static Int256 Multiply(Int256 left, Int256 right)
        {
            int leftSign = left.Sign;
            left = leftSign < 0 ? -left : left;
            int rightSign = right.Sign;
            right = rightSign < 0 ? -right : right;

            uint[] xInts = left.ToUIn32Array();
            uint[] yInts = right.ToUIn32Array();
            var mulInts = new uint[16];

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
            return new Int256(leftSign*rightSign, mulInts);
        }

        public static implicit operator Int256(bool value)
        {
            return new Int256(value);
        }

        public static implicit operator Int256(byte value)
        {
            return new Int256(value);
        }

        public static implicit operator Int256(char value)
        {
            return new Int256(value);
        }

        public static explicit operator Int256(decimal value)
        {
            return new Int256(value);
        }

        public static explicit operator Int256(double value)
        {
            return new Int256(value);
        }

        public static implicit operator Int256(short value)
        {
            return new Int256(value);
        }

        public static implicit operator Int256(int value)
        {
            return new Int256(value);
        }

        public static implicit operator Int256(long value)
        {
            return new Int256(value);
        }

        public static implicit operator Int256(sbyte value)
        {
            return new Int256(value);
        }

        public static explicit operator Int256(float value)
        {
            return new Int256(value);
        }

        public static implicit operator Int256(ushort value)
        {
            return new Int256(value);
        }

        public static implicit operator Int256(uint value)
        {
            return new Int256(value);
        }

        public static implicit operator Int256(ulong value)
        {
            return new Int256(value);
        }

        public static explicit operator bool(Int256 value)
        {
            return value.Sign != 0;
        }

        public static explicit operator byte(Int256 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if ((value < byte.MinValue) || (value > byte.MaxValue))
            {
                throw new OverflowException();
            }

            return (byte) value._d;
        }

        public static explicit operator char(Int256 value)
        {
            if (value.Sign == 0)
            {
                return (char) 0;
            }

            if ((value < char.MinValue) || (value > char.MaxValue))
            {
                throw new OverflowException();
            }

            return (char) (ushort) value._d;
        }

        public static explicit operator decimal(Int256 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if ((value < (Int256) decimal.MinValue) || (value > (Int256) decimal.MaxValue))
            {
                throw new OverflowException();
            }

            return new decimal((int) (value._d & 0xFFFFFFFF), (int) (value._d >> 32), (int) (value._c & 0xFFFFFFFF), value.Sign < 0, 0);
        }

        public static explicit operator double(Int256 value)
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

        public static explicit operator float(Int256 value)
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

        public static explicit operator short(Int256 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if ((value < short.MinValue) || (value > short.MaxValue))
            {
                throw new OverflowException();
            }

            return (short) value._d;
        }

        public static explicit operator int(Int256 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if ((value < int.MinValue) || (value > int.MaxValue))
            {
                throw new OverflowException();
            }

            return ((int) value._d);
        }

        public static explicit operator long(Int256 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if ((value < long.MinValue) || (value > long.MaxValue))
            {
                throw new OverflowException();
            }

            return (long) value._d;
        }

        public static explicit operator uint(Int256 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if ((value < uint.MinValue) || (value > uint.MaxValue))
            {
                throw new OverflowException();
            }

            return (uint) value._d;
        }

        public static explicit operator ushort(Int256 value)
        {
            if (value.Sign == 0)
            {
                return 0;
            }

            if ((value < ushort.MinValue) || (value > ushort.MaxValue))
            {
                throw new OverflowException();
            }

            return (ushort) value._d;
        }

        public static explicit operator ulong(Int256 value)
        {
            if ((value < ushort.MinValue) || (value > ushort.MaxValue))
            {
                throw new OverflowException();
            }

            return value._d;
        }

        public static bool operator >(Int256 left, Int256 right)
        {
            return Compare(left, right) > 0;
        }

        public static bool operator <(Int256 left, Int256 right)
        {
            return Compare(left, right) < 0;
        }

        public static bool operator >=(Int256 left, Int256 right)
        {
            return Compare(left, right) >= 0;
        }

        public static bool operator <=(Int256 left, Int256 right)
        {
            return Compare(left, right) <= 0;
        }

        public static bool operator !=(Int256 left, Int256 right)
        {
            return Compare(left, right) != 0;
        }

        public static bool operator ==(Int256 left, Int256 right)
        {
            return Compare(left, right) == 0;
        }

        public static Int256 operator +(Int256 value)
        {
            return value;
        }

        public static Int256 operator -(Int256 value)
        {
            return Negate(value);
        }

        public static Int256 operator +(Int256 left, Int256 right)
        {
            left._a += right._a;
            left._b += right._b;
            if (left._b < right._b)
            {
                left._a++;
            }
            left._c += right._c;
            if (left._c < right._c)
            {
                left._b++;
                if (left._b < left._b - 1)
                {
                    left._a++;
                }
            }
            left._d += right._d;
            if (left._d < right._d)
            {
                left._c++;
                if (left._c < left._c - 1)
                {
                    left._b++;
                    if (left._b < left._b - 1)
                    {
                        left._a++;
                    }
                }
            }

            return left;
        }

        public static Int256 operator -(Int256 left, Int256 right)
        {
            return left + -right;
        }

        public static Int256 operator %(Int256 dividend, Int256 divisor)
        {
            return Remainder(dividend, divisor);
        }

        public static Int256 operator /(Int256 dividend, Int256 divisor)
        {
            return Divide(dividend, divisor);
        }

        public static Int256 operator *(Int256 left, Int256 right)
        {
            return Multiply(left, right);
        }

        public static Int256 operator >>(Int256 value, int shift)
        {
            if (shift == 0)
            {
                return value;
            }

            ulong[] bits = MathUtils.ShiftRightSigned(value.ToUIn64Array(), shift);
            value._a = bits[3];
            value._b = bits[2];
            value._c = bits[1];
            value._d = bits[0];     //lo is stored in array entry 0

            return value;
        }

        public static Int256 operator <<(Int256 value, int shift)
        {
            if (shift == 0)
            {
                return value;
            }

            ulong[] bits = MathUtils.ShiftLeft(value.ToUIn64Array(), shift);
            value._a = bits[3];
            value._b = bits[2];
            value._c = bits[1];
            value._d = bits[0];     //lo is stored in array entry 0

            return value;
        }

        public static Int256 operator |(Int256 left, Int256 right)
        {
            if (left == 0)
            {
                return right;
            }

            if (right == 0)
            {
                return left;
            }

            left._a |= right._a;
            left._b |= right._b;
            left._c |= right._c;
            left._d |= right._d;
            return left;
        }

        public static Int256 operator &(Int256 left, Int256 right)
        {
            if (left == 0 || right == 0)
            {
                return Zero;
            }

            left._a &= right._a;
            left._b &= right._b;
            left._c &= right._c;
            left._d &= right._d;
            return left;
        }

        public static Int256 operator ~(Int256 value)
        {
            return new Int256(~value._a, ~value._b, ~value._c, ~value._d);
        }

        public static Int256 operator ++(Int256 value)
        {
            return value + 1;
        }

        public static Int256 operator --(Int256 value)
        {
            return value - 1;
        }
    }
}
