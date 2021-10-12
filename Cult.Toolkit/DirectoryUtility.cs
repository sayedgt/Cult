﻿using Cult.Toolkit.ExtraIEnumerable;
using System;
using System.IO;
// ReSharper disable All 
namespace Cult.Toolkit
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

        public static bool RenameDirectory(string directoryPath, string newName)
        {
            var path = Path.GetDirectoryName(directoryPath);
            if (path == null)
                return false;
            try
            {
                Directory.Move(directoryPath, Path.Combine(path, newName));
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void SafeDeleteDirectory(string path, bool recursive = false)
        {
            if (Directory.Exists(path))
            {
                var searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                Directory
                    .EnumerateFileSystemEntries(path, "*", searchOption)
                    .ForEach(x => File.SetAttributes(x, FileAttributes.Normal));

                Directory.Delete(path, recursive);
            }
        }
    }
}
