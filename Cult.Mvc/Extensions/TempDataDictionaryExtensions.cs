using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;
// ReSharper disable All 
namespace Cult.Mvc.Extensions
{
    public static class TempDataDictionaryExtensions
    {
        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            tempData.TryGetValue(key, out var o);
            return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
        }
        public static T Peek<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            var o = tempData.Peek(key);
            return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
        }
        public static void Set<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonSerializer.Serialize(value);
        }
    }
}
