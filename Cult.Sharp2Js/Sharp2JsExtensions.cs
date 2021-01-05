using Castle.Sharp2Js;
using System;
using System.Collections.Generic;
using System.Text;
namespace Cult.Sharp2Js
{
    public static class Sharp2JsExtensions
    {
        private static string AddJavaScriptArrayModel(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            var sb = new StringBuilder();
            sb.Append("models = [];");
            sb.AppendLine();
            sb.AppendLine(source);
            return sb.ToString();
        }
        public static string GetJavaScriptPoco(this Type type, bool addModelArray = false)
        {
            var poco = JsGenerator.Generate(new[] { type }, new JsGeneratorOptions
            {
                CamelCase = true,
                IncludeMergeFunction = false,
                IncludeEqualsFunction = false
            });
            return addModelArray ? AddJavaScriptArrayModel(poco) : poco;
        }
        public static string GetJavaScriptPoco(this Type type, JsGeneratorOptions options, bool addModelArray = false)
        {
            var poco = JsGenerator.Generate(new[] { type }, options);
            return addModelArray ? AddJavaScriptArrayModel(poco) : poco;
        }
        public static string GetJavaScriptPocos(this IEnumerable<Type> types, bool addModelArray = false)
        {
            var poco = JsGenerator.Generate(types, new JsGeneratorOptions
            {
                CamelCase = true,
                IncludeMergeFunction = false,
                IncludeEqualsFunction = false
            });
            return addModelArray ? AddJavaScriptArrayModel(poco) : poco;
        }
        public static string GetTypeScriptPocos(this IEnumerable<Type> types, JsGeneratorOptions options, bool addModelArray = false)
        {
            var poco = JsGenerator.Generate(types, options);
            return addModelArray ? AddJavaScriptArrayModel(poco) : poco;
        }
    }
}
