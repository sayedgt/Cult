// ReSharper disable All 
namespace Xbehave
{
    public static class GherkinStyle
    {
        public static string And(string text, string prefix = "And", string postfix = "") => $"{prefix} {text} {postfix}";
        public static string Given(string text, string prefix = "Given", string postfix = "") => $"{prefix} {text} {postfix}";
        public static string Then(string text, string prefix = "Then", string postfix = "") => $"{prefix} {text} {postfix}";
        public static string When(string text, string prefix = "When", string postfix = "") => $"{prefix} {text} {postfix}";
    }
}
