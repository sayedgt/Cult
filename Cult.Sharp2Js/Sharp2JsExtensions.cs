﻿using Castle.Sharp2Js;
using System;
using System.Collections.Generic;
using System.Text;
// ReSharper disable All 
namespace Cult.Sharp2Js
{
    public static class Sharp2JsExtensions
    {
        private static string AddModelsVariable(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            var sb = new StringBuilder();
            sb.Append("var models = [];");
            sb.AppendLine();
            sb.AppendLine(source);
            return sb.ToString();
        }
        public static string ToJavaScript(this Type type, bool defineModelsVariable = false)
        {
            var result = JsGenerator.Generate(new[] { type }, new JsGeneratorOptions
            {
                CamelCase = true,
                IncludeMergeFunction = false,
                IncludeEqualsFunction = false
            });
            return defineModelsVariable ? AddModelsVariable(result) : result;
        }
        public static string ToJavaScript(this Type type, JsGeneratorOptions options, bool addModelsVariable = false)
        {
            var result = JsGenerator.Generate(new[] { type }, options);
            return addModelsVariable ? AddModelsVariable(result) : result;
        }
        public static string ToJavaScript(this IEnumerable<Type> types, bool addModelsVariable = false)
        {
            var result = JsGenerator.Generate(types, new JsGeneratorOptions
            {
                CamelCase = true,
                IncludeMergeFunction = false,
                IncludeEqualsFunction = false
            });
            return addModelsVariable ? AddModelsVariable(result) : result;
        }
        public static string ToJavaScript(this IEnumerable<Type> types, JsGeneratorOptions options, bool addModelsVariable = false)
        {
            var result = JsGenerator.Generate(types, options);
            return addModelsVariable ? AddModelsVariable(result) : result;
        }
        public static string ToJavaScript(this Type[] types, bool addModelsVariable = false)
        {
            var result = JsGenerator.Generate(types, new JsGeneratorOptions
            {
                CamelCase = true,
                IncludeMergeFunction = false,
                IncludeEqualsFunction = false
            });
            return addModelsVariable ? AddModelsVariable(result) : result;
        }
        public static string ToJavaScript(this Type[] types, JsGeneratorOptions options, bool addModelsVariable = false)
        {
            var result = JsGenerator.Generate(types, options);
            return addModelsVariable ? AddModelsVariable(result) : result;
        }
    }
}
