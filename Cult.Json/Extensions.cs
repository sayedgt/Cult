using System.Text.Json;

namespace Cult.Json
{
    public static class Extensions
    {
        public static string ToJson<T>(this T obj, bool indented = false)
        {
            return JsonSerializer.Serialize<T>(obj, new JsonSerializerOptions()
            {
                WriteIndented = indented
            });
        }

        public static T FromJson<T>(this string jsonText)
        {
            return JsonSerializer.Deserialize<T>(jsonText);
        }
    }
}
