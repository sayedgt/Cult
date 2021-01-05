// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedBitConverter.cs">
//   Copyright (c) 2013 Alexander Logger. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;

// ReSharper disable All

// ReSharper disable All 
namespace Cult.BigMath.Utils
{
    public static class ExtendedBitConverter
    {
        public static readonly bool IsLittleEndian = BitConverter.IsLittleEndian;

        #region Int16
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBytes(this short value, bool? asLittleEndian = null)
        {
            var buffer = new byte[2];
            value.ToBytes(buffer, 0, asLittleEndian);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBytes(this short value, byte[] buffer, int offset = 0, bool? asLittleEndian = null)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            if (asLittleEndian.HasValue ? asLittleEndian.Value : IsLittleEndian)
            {
                buffer[offset] = (byte) value;
                buffer[offset + 1] = (byte) (value >> 8);
            }
            else
            {
                buffer[offset] = (byte) (value >> 8);
                buffer[offset + 1] = (byte) (value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ToInt16(this byte[] bytes, int offset = 0, bool? asLittleEndian = null)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (bytes.Length == 0)
            {
                return 0;
            }
            if (bytes.Length <= offset)
            {
                throw new InvalidOperationException("Array length must be greater than offset.");
            }
            bool ale = GetIsLittleEndian(asLittleEndian);
            EnsureLength(ref bytes, 2, offset, ale);

            return (short) (ale ? bytes[offset] | bytes[offset + 1] << 8 : bytes[offset] << 8 | bytes[offset + 1]);
        }
        #endregion

        #region UInt16
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBytes(this ushort value, bool? asLittleEndian = null)
        {
            return unchecked((short) value).ToBytes(asLittleEndian);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBytes(this ushort value, byte[] buffer, int offset = 0, bool? asLittleEndian = null)
        {
            unchecked((short) value).ToBytes(buffer, offset, asLittleEndian);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ToUInt16(this byte[] bytes, int offset = 0, bool? asLittleEndian = null)
        {
            return (ushort) bytes.ToInt16(offset, asLittleEndian);
        }
        #endregion

        #region Int32
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBytes(this int value, bool? asLittleEndian = null)
        {
            var buffer = new byte[4];
            value.ToBytes(buffer, 0, asLittleEndian);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBytes(this int value, byte[] buffer, int offset = 0, bool? asLittleEndian = null)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            if (asLittleEndian.HasValue ? asLittleEndian.Value : IsLittleEndian)
            {
                buffer[offset] = (byte) value;
                buffer[offset + 1] = (byte) (value >> 8);
                buffer[offset + 2] = (byte) (value >> 16);
                buffer[offset + 3] = (byte) (value >> 24);
            }
            else
            {
                buffer[offset] = (byte) (value >> 24);
                buffer[offset + 1] = (byte) (value >> 16);
                buffer[offset + 2] = (byte) (value >> 8);
                buffer[offset + 3] = (byte) value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToInt32(this byte[] bytes, int offset = 0, bool? asLittleEndian = null)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (bytes.Length == 0)
            {
                return 0;
            }
            if (bytes.Length <= offset)
            {
                throw new InvalidOperationException("Array length must be greater than offset.");
            }
            bool ale = GetIsLittleEndian(asLittleEndian);
            EnsureLength(ref bytes, 4, offset, ale);

            return (ale)
                ? bytes[offset] | bytes[offset + 1] << 8 | bytes[offset + 2] << 16 | bytes[offset + 3] << 24
                : bytes[offset] << 24 | bytes[offset + 1] << 16 | bytes[offset + 2] << 8 | bytes[offset + 3];
        }
        #endregion

        private static void EnsureLength(ref byte[] bytes, int minLength, int offset, bool ale)
        {
            int bytesLength = bytes.Length - offset;
            if (bytesLength < minLength)
            {
                var b = new byte[minLength];
                Buffer.BlockCopy(bytes, offset, b, ale ? 0 : minLength - bytesLength, bytesLength);
                bytes = b;
            }
        }

        private static bool GetIsLittleEndian(bool? asLittleEndian)
        {
            return asLittleEndian.HasValue ? asLittleEndian.Value : IsLittleEndian;
        }

        #region UInt32
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBytes(this uint value, bool? asLittleEndian = null)
        {
            return unchecked((int) value).ToBytes(asLittleEndian);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBytes(this uint value, byte[] buffer, int offset = 0, bool? asLittleEndian = null)
        {
            unchecked((int) value).ToBytes(buffer, offset, asLittleEndian);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ToUInt32(this byte[] bytes, int offset = 0, bool? asLittleEndian = null)
        {
            return (uint) bytes.ToInt32(offset, asLittleEndian);
        }
        #endregion

        #region Int64
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBytes(this long value, bool? asLittleEndian = null)
        {
            var buffer = new byte[8];
            value.ToBytes(buffer, 0, asLittleEndian);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBytes(this long value, byte[] buffer, int offset = 0, bool? asLittleEndian = null)
        {
            if (asLittleEndian.HasValue ? asLittleEndian.Value : IsLittleEndian)
            {
                buffer[offset] = (byte) value;
                buffer[offset + 1] = (byte) (value >> 8);
                buffer[offset + 2] = (byte) (value >> 16);
                buffer[offset + 3] = (byte) (value >> 24);
                buffer[offset + 4] = (byte) (value >> 32);
                buffer[offset + 5] = (byte) (value >> 40);
                buffer[offset + 6] = (byte) (value >> 48);
                buffer[offset + 7] = (byte) (value >> 56);
            }
            else
            {
                buffer[offset] = (byte) (value >> 56);
                buffer[offset + 1] = (byte) (value >> 48);
                buffer[offset + 2] = (byte) (value >> 40);
                buffer[offset + 3] = (byte) (value >> 32);
                buffer[offset + 4] = (byte) (value >> 24);
                buffer[offset + 5] = (byte) (value >> 16);
                buffer[offset + 6] = (byte) (value >> 8);
                buffer[offset + 7] = (byte) value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ToInt64(this byte[] bytes, int offset = 0, bool? asLittleEndian = null)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (bytes.Length == 0)
            {
                return 0;
            }
            if (bytes.Length <= offset)
            {
                throw new InvalidOperationException("Array length must be greater than offset.");
            }
            bool ale = GetIsLittleEndian(asLittleEndian);
            EnsureLength(ref bytes, 8, offset, ale);

            return ale
                ? bytes[offset] | (long) bytes[offset + 1] << 8 | (long) bytes[offset + 2] << 16 | (long) bytes[offset + 3] << 24 | (long) bytes[offset + 4] << 32 |
                    (long) bytes[offset + 5] << 40 | (long) bytes[offset + 6] << 48 | (long) bytes[offset + 7] << 56
                : (long) bytes[offset] << 56 | (long) bytes[offset + 1] << 48 | (long) bytes[offset + 2] << 40 | (long) bytes[offset + 3] << 32 |
                    (long) bytes[offset + 4] << 24 | (long) bytes[offset + 5] << 16 | (long) bytes[offset + 6] << 8 | bytes[offset + 7];
        }
        #endregion

        #region UInt64
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBytes(this ulong value, bool? asLittleEndian = null)
        {
            return unchecked ((long) value).ToBytes(asLittleEndian);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBytes(this ulong value, byte[] buffer, int offset = 0, bool? asLittleEndian = null)
        {
            unchecked((long) value).ToBytes(buffer, offset, asLittleEndian);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ToUInt64(this byte[] bytes, int offset = 0, bool? asLittleEndian = null)
        {
            return (ulong) bytes.ToInt64(offset, asLittleEndian);
        }
        #endregion

        #region Int128
        public static void ToBytes(this Int128 value, byte[] buffer, int offset = 0, bool? asLittleEndian = null)
        {
            bool ale = GetIsLittleEndian(asLittleEndian);
            value.Low.ToBytes(buffer, ale ? offset : offset + 8, ale);
            value.High.ToBytes(buffer, ale ? offset + 8 : offset, ale);
        }

        public static byte[] ToBytes(this Int128 value, bool? asLittleEndian = null, bool trimZeros = false)
        {
            var buffer = new byte[16];
            value.ToBytes(buffer, 0, asLittleEndian);

            if (trimZeros)
            {
                buffer = buffer.TrimZeros(asLittleEndian);
            }

            return buffer;
        }

        public static Int128 ToInt128(this byte[] bytes, int offset = 0, bool? asLittleEndian = null)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (bytes.Length == 0)
            {
                return 0;
            }
            if (bytes.Length <= offset)
            {
                throw new InvalidOperationException("Array length must be greater than offset.");
            }
            bool ale = GetIsLittleEndian(asLittleEndian);
            EnsureLength(ref bytes, 16, offset, ale);

            return new Int128(bytes.ToUInt64(ale ? offset + 8 : offset, ale), bytes.ToUInt64(ale ? offset : offset + 8, ale));
        }
        #endregion

        #region Int256
        public static void ToBytes(this Int256 value, byte[] buffer, int offset = 0, bool? asLittleEndian = null)
        {
            bool ale = GetIsLittleEndian(asLittleEndian);

            value.D.ToBytes(buffer, ale ? offset : offset + 24, ale);
            value.C.ToBytes(buffer, ale ? offset + 8 : offset + 16, ale);
            value.B.ToBytes(buffer, ale ? offset + 16 : offset + 8, ale);
            value.A.ToBytes(buffer, ale ? offset + 24 : offset, ale);
        }

        public static byte[] ToBytes(this Int256 value, bool? asLittleEndian = null, bool trimZeros = false)
        {
            var buffer = new byte[32];
            value.ToBytes(buffer, 0, asLittleEndian);

            if (trimZeros)
            {
                buffer = buffer.TrimZeros(asLittleEndian);
            }

            return buffer;
        }

        public static Int256 ToInt256(this byte[] bytes, int offset = 0, bool? asLittleEndian = null)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (bytes.Length == 0)
            {
                return 0;
            }
            if (bytes.Length <= offset)
            {
                throw new InvalidOperationException("Array length must be greater than offset.");
            }
            bool ale = GetIsLittleEndian(asLittleEndian);
            EnsureLength(ref bytes, 32, offset, ale);

            ulong a = bytes.ToUInt64(ale ? offset + 24 : offset, ale);
            ulong b = bytes.ToUInt64(ale ? offset + 16 : offset + 8, ale);
            ulong c = bytes.ToUInt64(ale ? offset + 8 : offset + 16, ale);
            ulong d = bytes.ToUInt64(ale ? offset : offset + 24, ale);

            return new Int256(a, b, c, d);
        }
        #endregion
    }
}
