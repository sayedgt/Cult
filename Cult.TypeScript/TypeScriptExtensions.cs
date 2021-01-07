using CsToTs;
using System;
using System.Collections.Generic;

namespace Cult.TypeScript
{
    public static class TypeScriptExtensions
    {
        public static string ToTypeScript(this Type type)
        {
            return Generator.GenerateTypeScript(type, new TypeScriptOptions());
        }
        public static string ToTypeScript(this Type type, TypeScriptOptions options)
        {
            return Generator.GenerateTypeScript(type, options);
        }
        public static string ToTypeScript(this IEnumerable<Type> types)
        {
            return Generator.GenerateTypeScript(types);
        }
        public static string ToTypeScript(this IEnumerable<Type> types, TypeScriptOptions options)
        {
            return Generator.GenerateTypeScript(options, types);
        }
        public static string ToTypeScript(this Type[] types, TypeScriptOptions options)
        {
            return Generator.GenerateTypeScript(options, types);
        }
        public static string ToTypeScript(this Type[] types)
        {
            return Generator.GenerateTypeScript(types);
        }
    }
}
