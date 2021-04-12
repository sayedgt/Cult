using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
// ReSharper disable All

namespace Cult.Mvc.Extensions
{
    public static class IFormFileExtensions
    {
        /*
        public static Option<string> GetMimeType(this IFormFile file)
        {
            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                if (IsJpeg(reader)) return "image/jpeg";
                if (IsPng(reader)) return "image/png";

                if (file.ContentType != null) return file.ContentType;
            }

            return None;
        }
        */
        public static byte[] ToArray(this IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        public static async Task<byte[]> ToArrayAsync(this IFormFile file)
        {
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream).ConfigureAwait(false);
            return memoryStream.ToArray();
        }
    }
}
