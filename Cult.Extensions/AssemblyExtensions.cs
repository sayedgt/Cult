using System.Collections.Generic;
using System.Linq;
using System.Reflection;
// ReSharper disable UnusedMember.Global

namespace Cult.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Assembly> WithReferencedAssemblies(this Assembly assembly)
        {
            var listOfAssemblies = new List<Assembly>
            {
                assembly
            };
            listOfAssemblies.AddRange(assembly.GetReferencedAssemblies().Select(Assembly.Load));
            return listOfAssemblies;
        }

        public static IEnumerable<Assembly> GetReferencedAssemblies(this Assembly assembly)
        {
            var listOfAssemblies = new List<Assembly>();
            listOfAssemblies.AddRange(assembly.GetReferencedAssemblies().Select(Assembly.Load));
            return listOfAssemblies;
        }
    }
}

