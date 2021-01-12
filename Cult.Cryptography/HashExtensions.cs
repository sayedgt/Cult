using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace Cult.Cryptography
{
    public static class HashExtensions
    {
        public static string ComputeMD5Hash(this string data)
        {
            return ComputeMD5Hash(data.ToByteArray());
        }
        public static string ComputeMD5Hash(this Stream data)
        {
            return ComputeMD5Hash(data.ToByteArray());
        }
        public static string ComputeMD5Hash(this byte[] data)
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
        public static string ComputeSHA1Hash(this string data)
        {
            return ComputeSHA1Hash(data.ToByteArray());
        }
        public static string ComputeSHA1Hash(this Stream data)
        {
            return ComputeSHA1Hash(data.ToByteArray());
        }
        public static string ComputeSHA1Hash(this byte[] data)
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
        public static string ComputeSHA256Hash(this string data)
        {
            return ComputeSHA256Hash(data.ToByteArray());
        }
        public static string ComputeSHA256Hash(this Stream data)
        {
            return ComputeSHA256Hash(data.ToByteArray());
        }
        public static string ComputeSHA256Hash(this byte[] data)
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
        public static string ComputeSHA384Hash(this string data)
        {
            return ComputeMD5Hash(data.ToByteArray());
        }
        public static string ComputeSHA384Hash(this Stream data)
        {
            return ComputeMD5Hash(data.ToByteArray());
        }
        public static string ComputeSHA384Hash(this byte[] data)
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
        public static string ComputeSHA512Hash(this string data)
        {
            return ComputeMD5Hash(data.ToByteArray());
        }
        public static string ComputeSHA512Hash(this Stream data)
        {
            return ComputeMD5Hash(data.ToByteArray());
        }
        public static string ComputeSHA512Hash(this byte[] data)
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

    }
}
