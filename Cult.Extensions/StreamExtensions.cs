using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
// ReSharper disable UnusedMember.Global

namespace Cult.Extensions
{
    public static class StreamExtensions
    {
        public static void CopyTo(this Stream fromStream, Stream toStream)
        {
            if (fromStream == null)
                throw new ArgumentNullException(nameof(fromStream));
            if (toStream == null)
                throw new ArgumentNullException(nameof(toStream));
            var bytes = new byte[8092];
            int dataRead;
            while ((dataRead = fromStream.Read(bytes, 0, bytes.Length)) > 0)
                toStream.Write(bytes, 0, dataRead);
        }

        public static byte[] ToByteArray(this Stream @this)
        {
            using (var ms = new MemoryStream())
            {
                @this.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static string ToMd5Hash(this Stream @this)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(@this);
                var sb = new StringBuilder();
                foreach (var bytes in hashBytes)
                {
                    sb.Append(bytes.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        public static string ToText(this Stream @this)
        {
            using (var sr = new StreamReader(@this, Encoding.Default))
            {
                return sr.ReadToEnd();
            }
        }

        public static string ToText(this Stream @this, Encoding encoding)
        {
            using (var sr = new StreamReader(@this, encoding))
            {
                return sr.ReadToEnd();
            }
        }

        public static string ToText(this Stream @this, long position)
        {
            @this.Position = position;

            using (var sr = new StreamReader(@this, Encoding.Default))
            {
                return sr.ReadToEnd();
            }
        }

        public static string ToText(this Stream @this, Encoding encoding, long position)
        {
            @this.Position = position;

            using (var sr = new StreamReader(@this, encoding))
            {
                return sr.ReadToEnd();
            }
        }

        public static MemoryStream ToMemoryStream(this Stream stream)
        {
            var ret = new MemoryStream();
            var buffer = new byte[8192];
            int bytesRead;
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                ret.Write(buffer, 0, bytesRead);
            ret.Position = 0;
            return ret;
        }
    }
}
