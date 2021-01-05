using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
// ReSharper disable All 
namespace Cult.Extensions
{
    public static class ByteArrayExtensions
    {
        public static int FindArrayInArray(this byte[] array1, byte[] array2)
        {
            if (array2 == null)
                throw new ArgumentNullException(nameof(array2));

            if (array1 == null)
                throw new ArgumentNullException(nameof(array1));

            if (array2.Length == 0)
                return 0;       // by definition empty sets match immediately

            var j = -1;
            var end = array1.Length - array2.Length;
            while ((j = Array.IndexOf(array1, array2[0], j + 1)) <= end && j != -1)
            {
                var i = 1;
                while (array1[j + i] == array2[i])
                {
                    if (++i == array2.Length)
                        return j;
                }
            }
            return -1;
        }
        public static T FromByteArray<T>(this byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
        public static string ToAsciiString(this byte[] @this)
        {
            return System.Text.Encoding.ASCII.GetString(@this, 0, @this.Length);
        }
        public static int ToBase64CharArray(this byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut)
        {
            return Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut);
        }
        public static int ToBase64CharArray(this byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut, Base64FormattingOptions options)
        {
            return Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut, options);
        }
        public static string ToBase64String(this byte[] inArray)
        {
            return Convert.ToBase64String(inArray);
        }
        public static string ToBase64String(this byte[] inArray, Base64FormattingOptions options)
        {
            return Convert.ToBase64String(inArray, options);
        }
        public static string ToBase64String(this byte[] inArray, int offset, int length)
        {
            return Convert.ToBase64String(inArray, offset, length);
        }
        public static string ToBase64String(this byte[] inArray, int offset, int length, Base64FormattingOptions options)
        {
            return Convert.ToBase64String(inArray, offset, length, options);
        }
        public static MemoryStream ToMemoryStream(this byte[] @this)
        {
            return new MemoryStream(@this);
        }
        public static Stream ToStream(this byte[] @this)
        {
            return new MemoryStream(@this);
        }
        public static string ToUtf8String(this byte[] @this)
        {
            return System.Text.Encoding.UTF8.GetString(@this, 0, @this.Length);
        }
    }
}
