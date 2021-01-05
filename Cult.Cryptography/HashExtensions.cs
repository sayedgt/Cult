using System.IO;
using System.Security.Cryptography;
using System.Text;
// ReSharper disable All

namespace Cult.Cryptography
{
    public static class HashExtensions
    {
        public static string ComputeHash(this byte[] data, HashAlgorithm hashAlgorithm)
        {
            if (hashAlgorithm == HashAlgorithm.MD5)
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    byte[] bytes = md5Hash.ComputeHash(data);
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
            if (hashAlgorithm == HashAlgorithm.SHA1)
            {
                using (SHA1 sha1Hash = SHA1.Create())
                {
                    byte[] bytes = sha1Hash.ComputeHash(data);
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
            if (hashAlgorithm == HashAlgorithm.SHA256)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(data);
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
            if (hashAlgorithm == HashAlgorithm.SHA384)
            {
                using (SHA384 sha384Hash = SHA384.Create())
                {
                    byte[] bytes = sha384Hash.ComputeHash(data);
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
            if (hashAlgorithm == HashAlgorithm.SHA512)
            {
                using (SHA512 sha512Hash = SHA512.Create())
                {
                    byte[] bytes = sha512Hash.ComputeHash(data);
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
            return string.Empty;
        }

        public static string ComputeHash(this string data, HashAlgorithm hashAlgorithm)
        {
            return ComputeHash(data.ToByteArray(), hashAlgorithm);
        }

        public static string ComputeHash(this Stream data, HashAlgorithm hashAlgorithm)
        {
            return ComputeHash(data.ToByteArray(), hashAlgorithm);
        }

        private static byte[] ToByteArray(this string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }
        private static byte[] ToByteArray(this Stream data)
        {
            using (var memoryStream = new MemoryStream())
            {
                data.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
