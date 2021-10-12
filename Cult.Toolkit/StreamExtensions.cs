using Cult.Toolkit.ExtraByteArray;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable All 
namespace Cult.Toolkit.ExtraStream
{
    public static class StreamExtensions
    {
        public static List<string> ReadLines(this Stream stream)
        {
            var lines = new List<string>();
            using (var sr = new StreamReader(stream))
            {
                while (sr.Peek() >= 0)
                {
                    lines.Add(sr.ReadLine());
                }
            }
            return lines;
        }

        public static string ReadAll(this Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }
        public static void Clear(this Stream stream) => stream.SetLength(0);

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

        public static string ReadBlockAsString(this Stream stream, int bufferSize = 1024, EncodingType encoding = EncodingType.UTF8)
        {
            var sb = new StringBuilder();
            var buffer = new byte[bufferSize];
            while (true)
            {
                var count = stream.Read(buffer, 0, bufferSize);
                if (count == 0) break;
                sb.Append(buffer.ConvertByteArrayToString(encoding).ToCharArray(), 0, count);
            }
            var text = sb.ToString();
            return text;
        }

        public static async Task<string> ReadBlockAsStringAsync(this Stream stream, int bufferSize = 1024, EncodingType encoding = EncodingType.UTF8)
        {
            var sb = new StringBuilder();
            var buffer = new byte[bufferSize];
            while (true)
            {
                var count = await stream.ReadAsync(buffer, 0, bufferSize);
                if (count == 0) break;
                sb.Append(buffer.ConvertByteArrayToString(encoding).ToCharArray(), 0, count);
            }
            var text = sb.ToString();
            return text;
        }

        public static void ReadBlock(this Stream stream, Action<string> action, int bufferSize = 1024, EncodingType encoding = EncodingType.UTF8)
        {
            var buffer = new byte[bufferSize];
            while (true)
            {
                var count = stream.Read(buffer, 0, bufferSize);
                if (count == 0) break;
                var text = buffer.ConvertByteArrayToString(encoding, 0, count);
                action(text);
            }
        }

        public static IEnumerable<string> ReadBlock(this Stream stream, int bufferSize = 1024, EncodingType encoding = EncodingType.UTF8)
        {
            var buffer = new byte[bufferSize];
            while (true)
            {
                var count = stream.Read(buffer, 0, bufferSize);
                if (count == 0) break;
                var text = buffer.ConvertByteArrayToString(encoding, 0, count);
                yield return text;
            }
        }

        public static void ReadBlock(this Stream stream, Action<byte[]> action, int bufferSize = 1024)
        {
            var buffer = new byte[bufferSize];
            while (true)
            {
                var count = stream.Read(buffer, 0, bufferSize);
                if (count == 0) break;
                action(buffer);
            }
        }

        public static async void ReadBlockAsync(this Stream stream, Action<string> action, int bufferSize = 1024, EncodingType encoding = EncodingType.UTF8)
        {
            var buffer = new byte[bufferSize];
            while (true)
            {
                var count = await stream.ReadAsync(buffer, 0, bufferSize);
                if (count == 0) break;
                var text = buffer.ConvertByteArrayToString(encoding, 0, count);
                action(text);
            }
        }

        public static async void ReadBlockAsync(this Stream stream, Action<byte[]> action, int bufferSize = 1024)
        {
            var buffer = new byte[bufferSize];
            while (true)
            {
                var count = await stream.ReadAsync(buffer, 0, bufferSize);
                if (count == 0) break;
                action(buffer);
            }
        }

        public static async IAsyncEnumerable<string> ReadBlockAsync(this Stream stream, int bufferSize = 1024, EncodingType encoding = EncodingType.UTF8)
        {
            var buffer = new byte[bufferSize];
            while (true)
            {
                var count = await stream.ReadAsync(buffer, 0, bufferSize);
                if (count == 0) break;
                yield return buffer.ConvertByteArrayToString(encoding, 0, count);
            }
        }

        public static IEnumerable<byte[]> ReadBlockAsBytes(this Stream stream, int bufferSize = 1024)
        {
            var buffer = new byte[bufferSize];
            while (true)
            {
                var count = stream.Read(buffer, 0, bufferSize);
                if (count == 0) break;
                yield return buffer;
            }
        }

        public static async IAsyncEnumerable<byte[]> ReadBlockAsBytesAsync(this Stream stream, int bufferSize = 1024)
        {
            var buffer = new byte[bufferSize];
            while (true)
            {
                var count = await stream.ReadAsync(buffer, 0, bufferSize);
                if (count == 0) break;
                yield return buffer;
            }
        }

        public static bool IsNullOrEmpty(this Stream stream)
        {
            if (stream == null)
                return true;
            return stream.Length <= 0;
        }

        public static bool IsNotNullOrEmpty(this Stream stream)
        {
            return !stream.IsNullOrEmpty();
        }

        public static bool IsNull(this Stream stream)
        {
            return stream == null;
        }

        public static bool IsNotNull(this Stream stream)
        {
            return stream != null;
        }

        public static bool IsEmpty(this Stream stream)
        {
            return stream.Length <= 0;
        }

        public static bool IsNotEmpty(this Stream stream)
        {
            return !stream.IsEmpty();
        }
    }
}
