using System.IO;
using System.Text;

namespace Cult.Cryptography
{
    // Crc32
    // Rijndael
    // TripleDES
    internal static class Extensions
    {
        internal static byte[] ToByteArray(this string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }
        internal static byte[] ToByteArray(this Stream data)
        {
            using (var memoryStream = new MemoryStream())
            {
                data.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
