using System;
using System.IO;
// ReSharper disable All

namespace Cult.Utilities
{
    public static class DirectoryUtility
    {
        public static string CreateTempDirectory()
        {
            var tempDirectory = GetTempDirectory();
            if (!Directory.Exists(tempDirectory))
            {
                Directory.CreateDirectory(tempDirectory);
            }

            return tempDirectory;
        }

        public static void CreateTempDirectory(Action<string> action, bool autoDelete = true)
        {
            var tempDirectory = GetTempDirectory();
            if (!Directory.Exists(tempDirectory))
            {
                Directory.CreateDirectory(tempDirectory);
            }
            action(tempDirectory);
            if (autoDelete)
            {
                Directory.Delete(tempDirectory, true);
            }
        }

        public static string GetTempDirectory()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        }
    }
}
