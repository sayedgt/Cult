using System.Collections.Generic;
using System.Reflection;
// ReSharper disable All

namespace Cult.Utilities
{
    public static class AssemblyUtility
    {
        private static IEnumerable<Assembly> GetEntryAssemblyWithReferences()
        {
            var listOfAssemblies = new List<Assembly>();
            var mainAsm = Assembly.GetEntryAssembly();

            if (mainAsm == null) return null;

            listOfAssemblies.Add(mainAsm);

            foreach (var refAsmName in mainAsm.GetReferencedAssemblies())
            {
                listOfAssemblies.Add(Assembly.Load(refAsmName));
            }
            return listOfAssemblies;
        }
    }
}