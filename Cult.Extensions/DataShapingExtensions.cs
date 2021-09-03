using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
// ReSharper disable UnusedMember.Global
// ReSharper disable once CheckNamespace
namespace Cult.Extensions.DataShaping
{
    public static class DataShapingExtensions
    {
        public static IDictionary<string, object> ShapeData<T>(this T dataToShape, string fields, bool ignoreCase = true, Func<T, string, object, object> converter = null)
        {
            var result = new Dictionary<string, object>();
            if (dataToShape == null)
            {
                return result;
            }

            var propertyInfoList = GetPropertyInfos<T>(fields, ignoreCase);

            result.FillDictionary(dataToShape, propertyInfoList, converter);

            return result;
        }

        public static IDictionary<string, object> ShapeData<T>(this IEnumerable<T> dataToShape, string fields, bool ignoreCase = true, Func<T, string, object, object> converter = null, string jsonListName = "values")
        {
            var result = new Dictionary<string, object>();
            if (dataToShape == null)
            {
                return result;
            }

            var propertyInfoList = GetPropertyInfos<T>(fields, ignoreCase);

            var list = dataToShape.Select(line =>
            {
                var data = new Dictionary<string, object>();
                data.FillDictionary(line, propertyInfoList, converter);
                return data;
            }).Where(d => d.Keys.Any()).ToList();

            result[jsonListName] = list;

            return result;
        }

        public static string ToShapedData<T>(this T dataToShape, string fields, bool ignoreCase = true, Func<T, string, object, object> converter = null)
        {
            return JsonSerializer.Serialize(ShapeData(dataToShape, fields, ignoreCase, converter));
        }

        public static string ToShapedData<T>(this IEnumerable<T> dataToShape, string fields, bool ignoreCase = true, Func<T, string, object, object> converter = null, string jsonListName = "values")
        {
            return JsonSerializer.Serialize(ShapeData(dataToShape, fields, ignoreCase, converter, jsonListName));
        }

        private static void FillDictionary<T>(this IDictionary<string, object> dictionary, T source, IEnumerable<PropertyInfo> fields, Func<T, string, object, object> converter = null)
        {
            foreach (var propertyInfo in fields)
            {
                var propertyValue = propertyInfo.GetValue(source);

                var value = converter != null ? converter(source, propertyInfo.Name, propertyValue) : propertyValue;


                dictionary.Add(propertyInfo.Name, value);
            }
        }

        private static IEnumerable<PropertyInfo> GetPropertyInfos<T>(string fields, bool ignoreCase)
        {
            var propertyInfoList = new List<PropertyInfo>();

            if (!string.IsNullOrWhiteSpace(fields))
            {
                return ExtractSelectedPropertiesInfo<T>(fields, propertyInfoList, ignoreCase);
            }

            var propertyInfos = typeof(T).GetRuntimeProperties();
            propertyInfoList.AddRange(propertyInfos);
            return propertyInfoList;
        }

        private static IEnumerable<PropertyInfo> ExtractSelectedPropertiesInfo<T>(string fields, List<PropertyInfo> propertyInfoList, bool ignoreCase)
        {
            var fieldsAfterSplit = fields.Split(',');

            foreach (var propertyName in fieldsAfterSplit.Select(f => f.Trim()))
            {
                var propName = ignoreCase ? propertyName.ToLower() : propertyName;
                var propertyInfo = typeof(T).GetRuntimeProperties().FirstOrDefault(x => (ignoreCase ? x.Name.ToLower() : x.Name) == propName);

                if (propertyInfo == null)
                {
                    continue;
                }

                propertyInfoList.Add(propertyInfo);
            }

            return propertyInfoList;
        }
    }
}
