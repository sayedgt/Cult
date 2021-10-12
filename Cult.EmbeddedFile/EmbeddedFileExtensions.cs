using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Cult.EmbeddedFile
{
    public static class EmbeddedFileExtensions
    {
        private static bool Contains(this string source, string str, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(source))
                return true;
            return source.IndexOf(str, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0;
        }

        public static string GetResourceAsString(this Assembly assembly, string resourceName)
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        public static IEnumerable<IFileInfo> GetResources(this Assembly assembly)
        {
            List<IFileInfo> files = new List<IFileInfo>();
            var embedded = new EmbeddedFileProvider(assembly);
            var resources = embedded.GetDirectoryContents("/");
            foreach (var resource in resources)
                files.Add(resource);
            return files;
        }

        public static IEnumerable<IFileInfo> GetResources(this IEnumerable<Assembly> assemblies)
        {
            List<IFileInfo> files = new List<IFileInfo>();
            foreach (var assembly in assemblies)
            {
                var embedded = new EmbeddedFileProvider(assembly);
                var resources = embedded.GetDirectoryContents("/");
                foreach (var resource in resources)
                    files.Add(resource);
            }
            return files;
        }

        public static IEnumerable<IFileInfo> GetResources(this Assembly assembly, string name, bool ignoreCase = false)
        {
            List<IFileInfo> files = new List<IFileInfo>();
            var embedded = new EmbeddedFileProvider(assembly);
            var resources = embedded.GetDirectoryContents("/").Where(x => x.Name.Contains(name, ignoreCase));
            foreach (var resource in resources)
                files.Add(resource);
            return files;
        }

        public static IEnumerable<IFileInfo> GetResources(this IEnumerable<Assembly> assemblies, string name, bool ignoreCase = false)
        {
            List<IFileInfo> files = new List<IFileInfo>();
            foreach (var assembly in assemblies)
            {
                var embedded = new EmbeddedFileProvider(assembly);
                var resources = embedded.GetDirectoryContents("/").Where(x => x.Name.Contains(name, ignoreCase));
                foreach (var resource in resources)
                    files.Add(resource);
            }
            return files;
        }

        public static IEnumerable<IFileInfo> GetResources(this Assembly assembly, string[] names, bool ignoreCase = false)
        {
            var rs = new List<IFileInfo>();
            List<IFileInfo> files = new List<IFileInfo>();
            var embedded = new EmbeddedFileProvider(assembly);
            foreach (var name in names)
            {
                var resources = embedded.GetDirectoryContents("/").Where(x => x.Name.Contains(name, ignoreCase));
                rs.AddRange(resources);
            }
            foreach (var resource in rs)
                files.Add(resource);
            return files;
        }

        public static IEnumerable<IFileInfo> GetResources(this IEnumerable<Assembly> assemblies, string[] names, bool ignoreCase = false)
        {
            var rs = new List<IFileInfo>();
            List<IFileInfo> files = new List<IFileInfo>();
            foreach (var assembly in assemblies)
            {
                var embedded = new EmbeddedFileProvider(assembly);
                foreach (var name in names)
                {
                    var resources = embedded.GetDirectoryContents("/").Where(x => x.Name.Contains(name, ignoreCase));
                    rs.AddRange(resources);
                }
            }
            foreach (var resource in rs)
                files.Add(resource);
            return files;
        }

        public static IEnumerable<IFileInfo> GetResources(this Assembly assembly, Regex regex)
        {
            List<IFileInfo> files = new List<IFileInfo>();
            var embedded = new EmbeddedFileProvider(assembly);
            var resources = embedded.GetDirectoryContents("/").Where(x => regex.IsMatch(x.Name));
            foreach (var resource in resources)
                files.Add(resource);
            return files;
        }

        public static IEnumerable<IFileInfo> GetResources(this IEnumerable<Assembly> assemblies, Regex regex)
        {
            List<IFileInfo> files = new List<IFileInfo>();
            foreach (var assembly in assemblies)
            {
                var embedded = new EmbeddedFileProvider(assembly);
                var resources = embedded.GetDirectoryContents("/").Where(x => regex.IsMatch(x.Name));
                foreach (var resource in resources)
                    files.Add(resource);
            }
            return files;
        }

        public static IEnumerable<Stream> GetResourcesAsStream(this Assembly assembly)
        {
            List<Stream> files = new List<Stream>();
            var embedded = new EmbeddedFileProvider(assembly);
            var resources = embedded.GetDirectoryContents("/");
            foreach (var resource in resources)
                files.Add(resource.CreateReadStream());
            return files;
        }

        public static IEnumerable<Stream> GetResourcesAsStream(this IEnumerable<Assembly> assemblies)
        {
            List<Stream> files = new List<Stream>();
            foreach (var assembly in assemblies)
            {
                var embedded = new EmbeddedFileProvider(assembly);
                var resources = embedded.GetDirectoryContents("/");
                foreach (var resource in resources)
                    files.Add(resource.CreateReadStream());
            }
            return files;
        }

        public static IEnumerable<Stream> GetResourcesAsStream(this Assembly assembly, string name, bool ignoreCase = false)
        {
            List<Stream> files = new List<Stream>();
            var embedded = new EmbeddedFileProvider(assembly);
            var resources = embedded.GetDirectoryContents("/").Where(x => x.Name.Contains(name, ignoreCase));
            foreach (var resource in resources)
                files.Add(resource.CreateReadStream());
            return files;
        }

        public static IEnumerable<Stream> GetResourcesAsStream(this IEnumerable<Assembly> assemblies, string name, bool ignoreCase = false)
        {
            List<Stream> files = new List<Stream>();
            foreach (var assembly in assemblies)
            {
                var embedded = new EmbeddedFileProvider(assembly);
                var resources = embedded.GetDirectoryContents("/").Where(x => x.Name.Contains(name, ignoreCase));
                foreach (var resource in resources)
                    files.Add(resource.CreateReadStream());
            }
            return files;
        }

        public static IEnumerable<Stream> GetResourcesAsStream(this Assembly assembly, string[] names, bool ignoreCase = false)
        {
            var rs = new List<IFileInfo>();
            List<Stream> files = new List<Stream>();
            var embedded = new EmbeddedFileProvider(assembly);
            foreach (var name in names)
            {
                var resources = embedded.GetDirectoryContents("/").Where(x => x.Name.Contains(name, ignoreCase));
                rs.AddRange(resources);
            }
            foreach (var resource in rs)
                files.Add(resource.CreateReadStream());
            return files;
        }

        public static IEnumerable<Stream> GetResourcesAsStream(this IEnumerable<Assembly> assemblies, string[] names, bool ignoreCase = false)
        {
            var rs = new List<IFileInfo>();
            List<Stream> files = new List<Stream>();
            foreach (var assembly in assemblies)
            {
                var embedded = new EmbeddedFileProvider(assembly);
                foreach (var name in names)
                {
                    var resources = embedded.GetDirectoryContents("/").Where(x => x.Name.Contains(name, ignoreCase));
                    rs.AddRange(resources);
                }
            }
            foreach (var resource in rs)
                files.Add(resource.CreateReadStream());
            return files;
        }

        public static IEnumerable<Stream> GetResourcesAsStream(this Assembly assembly, Regex regex)
        {
            List<Stream> files = new List<Stream>();
            var embedded = new EmbeddedFileProvider(assembly);
            var resources = embedded.GetDirectoryContents("/").Where(x => regex.IsMatch(x.Name));
            foreach (var resource in resources)
                files.Add(resource.CreateReadStream());
            return files;
        }

        public static IEnumerable<Stream> GetResourcesAsStream(this IEnumerable<Assembly> assemblies, Regex regex)
        {
            List<Stream> files = new List<Stream>();
            foreach (var assembly in assemblies)
            {
                var embedded = new EmbeddedFileProvider(assembly);
                var resources = embedded.GetDirectoryContents("/").Where(x => regex.IsMatch(x.Name));
                foreach (var resource in resources)
                    files.Add(resource.CreateReadStream());
            }
            return files;
        }

        public static IEnumerable<string> GetResourcesAsString(this Assembly assembly)
        {
            List<string> files = new List<string>();
            var embedded = new EmbeddedFileProvider(assembly);
            var resources = embedded.GetDirectoryContents("/");
            foreach (var resource in resources)
            {
                var resourceName = $"{Assembly.GetExecutingAssembly().GetName().Name}.{resource.Name}";
                files.Add(assembly.GetResourceAsString(resourceName));
            }
            return files;
        }
    }
}
