using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
// ReSharper disable All 
namespace Cult.ParserKit
{
    public static class MagicWand
    {
        public static ICollection<T> AddRange<T>(this ICollection<T> @this, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                @this.Add(item);
            }

            return @this;
        }
        public static CharLocation GetCharLocation(this string text, int position)
        {
            var line = 1;
            var col = 0;
            for (int i = 0; i <= position; i++)
            {
                if (text[i] == '\n')
                {
                    ++line;
                    col = 0;
                }
                else
                {
                    ++col;
                }
            }
            return new CharLocation(text[position], line, col - 1);
        }
        public static string GetDescription(this Enum @enum)
        {
            return
                @enum
                    .GetType()
                    .GetMember(@enum.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description
                ?? @enum.ToString();
        }
        public static IEnumerable<TEnum> GetEnumValues<TEnum>() where TEnum : Enum
        {
            return ((TEnum[])Enum.GetValues(typeof(TEnum))).ToList();
        }
        public static IEnumerable<string> GetEnumValuesAsString<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select(e => e.ToString()).ToList();
        }
        public static bool IsDigit(this char ch)
        {
            return char.IsDigit(ch);
        }
        public static bool IsInEnum<TEnum>(this string input, bool ignoreCase = false) where TEnum : Enum
        {
            var enums = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select(v => ignoreCase ? v.ToString().ToLower() : v.ToString()).ToList();
            return enums.Contains(ignoreCase ? input.ToLower() : input);
        }
        public static bool IsLetter(this char ch)
        {
            return char.IsLetter(ch);
        }
        public static bool IsLetterOrDigit(this char ch)
        {
            return char.IsLetterOrDigit(ch);
        }
        public static bool IsMultiLine(this string text, int startPosition, int endPosition)
        {
            var start = GetCharLocation(text, startPosition);
            var end = GetCharLocation(text, endPosition);
            return start.Line != end.Line;
        }
        public static bool IsPunctuation(this char ch)
        {
            return char.IsPunctuation(ch);
        }
        public static bool IsRegexMatch(this string input, string regex, bool partialCheck = true)
        {
            regex = partialCheck ? regex : $"^{regex}$";
            return Regex.Match(input, regex, RegexOptions.Compiled).Success;
        }
        public static bool IsRegexMatch(this char input, string regex, bool partialCheck = true)
        {
            regex = partialCheck ? regex : $"^{regex}$";
            return Regex.Match(input.ToString(), regex, RegexOptions.Compiled).Success;
        }
        public static bool IsRegexMatch(this string input, Regex regex)
        {
            return regex.Match(input).Success;
        }
        public static bool IsRegexMatch(this char input, Regex regex)
        {
            return regex.Match(input.ToString()).Success;
        }
        public static bool IsWhiteSpace(this char ch)
        {
            return char.IsWhiteSpace(ch);
        }
    }
}
