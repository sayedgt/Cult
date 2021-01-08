using System.Collections.Generic;
using System.IO;
// ReSharper disable All 

namespace Cult.Extensions
{
    public static class FileSignatureExtensions
    {
        private static readonly Dictionary<string, List<byte[]>> _fileSignature = new Dictionary<string, List<byte[]>>
        {
            {
                ".jpeg", new List<byte[]>
                {
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE0},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE2},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE3},
                }
            },
            {
                ".jpg", new List<byte[]>
                {
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE0},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE1},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE8},
                }
            },
            {
                ".png", new List<byte[]>
                {
                    new byte[] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A},
                    // new byte[] {0x0D, 0x0A, 0x1A, 0x0A, 0x00, 0x00, 0x00, 0x0D}, // PNG screenshot files on mac
                }
            },
        };

        public static bool IsJpeg(this byte[] byteArray)
        {
            return false;
        }
        public static bool IsJpeg(this Stream stream)
        {
            return false;
        }
        public static bool IsJpeg(this FileStream fileStream)
        {
            return false;
        }
    }
}
