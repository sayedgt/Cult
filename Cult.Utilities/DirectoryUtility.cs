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
        public static void DeleteReadOnlyDirectory(string directoryPath)
        {
            foreach (var subdirectory in Directory.EnumerateDirectories(directoryPath))
            {
                DeleteReadOnlyDirectory(subdirectory);
            }
            foreach (var fileName in Directory.EnumerateFiles(directoryPath))
            {
                var fileInfo = new FileInfo(fileName);
                fileInfo.Attributes = FileAttributes.Normal;
                fileInfo.Delete();
            }
            Directory.Delete(directoryPath);
        }
        public static string GetTempDirectory()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        }
    }
}
