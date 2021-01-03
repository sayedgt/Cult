using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Cult.MustacheSharp.Mustache;
using Cult.MustacheSharp.Tags;
using Microsoft.Extensions.FileProviders;

// ReSharper disable UnusedMember.Global

namespace Cult.MustacheSharp
{
    public static class MustacheSharpExtensions
    {
        private const string TemplateRegex = @"{{#template\s+.+\s*}}";
        private const string OpenBraceReplacement = @"$@$@$***___@$%$";
        private const string TemplateReplacement = @"$@$@$***___@$%$#template";
        private const string TemplateTag = @"{{#template";

        public static Generator CompileInMemoryNestedTemplates(this FormatCompiler compiler, string format, Dictionary<string, string> templates)
        {
            var text = compiler.ResolveInMemoryNestedTemplates(format, templates);
            Generator generator = compiler.Compile(text);
            return generator;
        }

        public static Generator CompileInMemoryNestedTemplates(this HtmlFormatCompiler compiler, string format, Dictionary<string, string> templates)
        {
            var text = compiler.ResolveInMemoryNestedTemplates(format, templates);
            Generator generator = compiler.Compile(text);
            return generator;
        }

        public static Generator CompileNestedTemplates(this FormatCompiler compiler, string format)
        {
            var text = compiler.ResolveNestedTemplates(format, true);
            Generator generator = compiler.Compile(text);
            return generator;
        }

        public static Generator CompileNestedTemplates(this HtmlFormatCompiler compiler, string format)
        {
            var text = compiler.ResolveNestedTemplates(format, true);
            Generator generator = compiler.Compile(text);
            return generator;
        }

        // ReSharper disable once UnusedMember.Global
        public static void RegistersCustomTags(this HtmlFormatCompiler compiler)
        {
            compiler.RegisterTag(new TemplateDefinition(), true);
            compiler.RegisterTag(new IsNullOrEmptyTagDefinition(), true);
            compiler.RegisterTag(new AnyTagDefinition(), true);
            compiler.RegisterTag(new CamelizeTagDefinition(), true);
            compiler.RegisterTag(new LowerTagDefinition(), true);
            compiler.RegisterTag(new UpperTagDefinition(), true);
            compiler.RegisterTag(new TabTagDefinition(), true);
            compiler.RegisterTag(new CommentTagDefinition(), true);
        }

        // ReSharper disable once UnusedMember.Global
        public static void RegistersCustomTags(this FormatCompiler compiler)
        {
            compiler.RegisterTag(new TemplateDefinition(), true);
            compiler.RegisterTag(new IsNullOrEmptyTagDefinition(), true);
            compiler.RegisterTag(new AnyTagDefinition(), true);
            compiler.RegisterTag(new CamelizeTagDefinition(), true);
            compiler.RegisterTag(new LowerTagDefinition(), true);
            compiler.RegisterTag(new UpperTagDefinition(), true);
            compiler.RegisterTag(new TabTagDefinition(), true);
            compiler.RegisterTag(new CommentTagDefinition(), true);

        }

        private static string ResolveInMemoryNestedTemplates(this FormatCompiler compiler, string format, Dictionary<string, string> templates)
        {
            if (templates != null && templates.Count > 0)
            {
                Dictionary<string, object> obj = new Dictionary<string, object>();
                foreach (var tuple in templates)
                    obj.Add(tuple.Key, tuple.Value);
                var anonymous = obj.ToDynamicObject();
                while (true)
                {

                    if (Regex.IsMatch(format, TemplateRegex))
                    {
                        format = format.Replace("{{", OpenBraceReplacement);
                        format = format.Replace(TemplateReplacement, TemplateTag);
                        Generator generator = compiler.Compile(format);
                        format = generator.Render(anonymous);
                    }
                    else
                    {
                        format = format.Replace(OpenBraceReplacement, "{{");
                        return format;
                    }
                }
            }
            return format;

        }

        private static string ResolveInMemoryNestedTemplates(this HtmlFormatCompiler compiler, string format, Dictionary<string, string> templates)
        {
            if (templates != null && templates.Count > 0)
            {
                Dictionary<string, object> obj = new Dictionary<string, object>();
                foreach (var tuple in templates)
                    obj.Add(tuple.Key, tuple.Value);
                var anonymous = obj.ToDynamicObject();
                while (true)
                {

                    if (Regex.IsMatch(format, TemplateRegex))
                    {
                        format = format.Replace("{{", OpenBraceReplacement);
                        format = format.Replace(TemplateReplacement, TemplateTag);
                        Generator generator = compiler.Compile(format);
                        format = generator.Render(anonymous);
                    }
                    else
                    {
                        format = format.Replace(OpenBraceReplacement, "{{");
                        return format;
                    }
                }
            }
            return format;
        }

        private static string ResolveNestedTemplates(this FormatCompiler compiler, string format, bool searchDirectory = false)
        {
            var assembly = Assembly.GetEntryAssembly();
            Dictionary<string, object> resources = new Dictionary<string, object>();
            var files = assembly.GetResources(new Regex(@".*\.mustache", RegexOptions.IgnoreCase));
            foreach (var file in files)
            {
                var fName = Path.GetFileNameWithoutExtension(file.Name).ToLowerInvariant();
                if (fName.Contains("/"))
                {
                    var lastIndex = fName.LastIndexOf('/');
                    fName = fName.Substring(lastIndex);
                }
                if (!resources.ContainsKey(fName))
                    resources.Add(fName, file.CreateReadStream().ToString());
            }

            if (searchDirectory)
            {
                if (!(assembly is null))
                {
                    var dirFiles = Directory.EnumerateFiles(assembly.Location, "*.mustache", SearchOption.AllDirectories);
                    foreach (var file in dirFiles)
                    {
                        var name = Path.GetFileNameWithoutExtension(file).ToLowerInvariant();
                        if (!resources.ContainsKey(name))
                            resources.Add(name, File.ReadAllText(file));
                    }
                }
            }
            if (resources.Count > 0)
            {
                var anonymous = resources.ToDynamicObject();
                while (true)
                {

                    if (Regex.IsMatch(format, TemplateRegex))
                    {
                        format = format.Replace("{{", OpenBraceReplacement);
                        format = format.Replace(TemplateReplacement, TemplateTag);
                        Generator generator = compiler.Compile(format);
                        format = generator.Render(anonymous);
                    }
                    else
                    {
                        format = format.Replace(OpenBraceReplacement, "{{");
                        return format;
                    }
                }
            }
            return format;

        }

        private static string ResolveNestedTemplates(this HtmlFormatCompiler compiler, string format, bool searchDirectory = false)
        {
            var assembly = Assembly.GetEntryAssembly();
            Dictionary<string, object> resources = new Dictionary<string, object>();
            var files = assembly.GetResources(new Regex(@".*\.mustache", RegexOptions.IgnoreCase));
            foreach (var file in files)
            {
                var fName = Path.GetFileNameWithoutExtension(file.Name).ToLowerInvariant();
                if (fName.Contains("/"))
                {
                    var lastIndex = fName.LastIndexOf('/');
                    fName = fName.Substring(lastIndex);
                }
                if (!resources.ContainsKey(fName))
                    resources.Add(fName, file.CreateReadStream().ToString());
            }

            if (searchDirectory)
            {
                if (!(assembly is null))
                {
                    var dirFiles = Directory.EnumerateFiles(assembly.Location, "*.mustache", SearchOption.AllDirectories);
                    foreach (var file in dirFiles)
                    {
                        var name = Path.GetFileNameWithoutExtension(file).ToLowerInvariant();
                        if (!resources.ContainsKey(name))
                            resources.Add(name, File.ReadAllText(file));
                    }
                }
            }
            if (resources.Count > 0)
            {
                var anonymous = resources.ToDynamicObject();
                while (true)
                {

                    if (Regex.IsMatch(format, TemplateRegex))
                    {
                        format = format.Replace("{{", OpenBraceReplacement);
                        format = format.Replace(TemplateReplacement, TemplateTag);
                        Generator generator = compiler.Compile(format);
                        format = generator.Render(anonymous);
                    }
                    else
                    {
                        format = format.Replace(OpenBraceReplacement, "{{");
                        return format;
                    }
                }
            }
            return format;
        }

        private static dynamic ToDynamicObject(this IDictionary<string, object> source)
        {
            ICollection<KeyValuePair<string, object>> someObject = new ExpandoObject();
            foreach (var item in source)
            {
                someObject.Add(item);
            }
            return someObject;
        }

        private static IEnumerable<IFileInfo> GetResources(this Assembly assembly, Regex regex = null)
        {
            List<IFileInfo> files = new List<IFileInfo>();
            var embedded = new EmbeddedFileProvider(assembly);
            var dirContents = embedded.GetDirectoryContents(Path.DirectorySeparatorChar.ToString());
            var resources = regex != null ? dirContents.Where(x => regex.IsMatch(x.Name)) : dirContents;
            foreach (var resource in resources)
                files.Add(resource);
            return files;
        }
    }
}
