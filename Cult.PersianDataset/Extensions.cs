using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Cult.PersianDataset
{
    internal static class Extensions
    {
        internal static string GetManifestResourceText(this Assembly assembly, string resourceName)
        {
            var result = "";
            var resourceFileName = assembly.GetManifestResourceNames().FirstOrDefault(x=>x.EndsWith(resourceName, StringComparison.InvariantCultureIgnoreCase));

            if (string.IsNullOrEmpty(resourceFileName)) return result;

            using (Stream stream = assembly.GetManifestResourceStream(resourceFileName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
