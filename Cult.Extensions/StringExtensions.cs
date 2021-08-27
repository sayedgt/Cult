using Cult.Extensions.Enums;
using Cult.Extensions.ExtraIEnumerable;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Xml;
using System.Xml.Linq;
// ReSharper disable All

namespace Cult.Extensions.ExtraString
{
    public static class StringExtensions
    {
        public static bool TryParseEnum<T>(this string name, out T result, bool ignoreCase = false)
            where T : struct, Enum
        {
            return Enum.TryParse<T>(name, ignoreCase, out result);
        }

        public static T ParseEnum<T>(this string name, bool ignoreCase = false)
            where T : struct, Enum
        {
            return (T)Enum.Parse(typeof(T), name, ignoreCase);
        }
        public static DateTime ParseAsUtc(this string str)
        {
            return DateTimeOffset.Parse(str).UtcDateTime;
        }
        public static string BreakLineToNewLine(this string @this)
        {
            return @this.Replace("<br />", "\r\n").Replace("<br>", "\r\n").Replace("<br/>", "\r\n");
        }
        public static int CompareOrdinal(this string strA, string strB)
        {
            return string.CompareOrdinal(strA, strB);
        }
        public static int CompareOrdinal(this string strA, int indexA, string strB, int indexB, int length)
        {
            return string.CompareOrdinal(strA, indexA, strB, indexB, length);
        }
        public static string Concat(this string str0, string str1)
        {
            return string.Concat(str0, str1);
        }
        public static string Concat(this string str0, string str1, string str2)
        {
            return string.Concat(str0, str1, str2);
        }
        public static string Concat(this string str0, string str1, string str2, string str3)
        {
            return string.Concat(str0, str1, str2, str3);
        }
        public static string Concat(this IEnumerable<string> @this)
        {
            var sb = new StringBuilder();

            foreach (var s in @this)
            {
                sb.Append(s);
            }

            return sb.ToString();
        }
        public static string Concat<T>(this IEnumerable<T> source, Func<T, string> func)
        {
            var sb = new StringBuilder();
            foreach (var item in source)
            {
                sb.Append(func(item));
            }

            return sb.ToString();
        }
        public static string ConcatWith(this string @this, params string[] values)
        {
            return string.Concat(@this, string.Concat(values));
        }
        
        public static bool Contains(this string @this, string value, StringComparison comparisonType)
        {
            return @this?.IndexOf(value, comparisonType) != -1;
        }
        public static bool ContainsAll(this string @this, params string[] values)
        {
            foreach (var value in values)
            {
                if (@this.IndexOf(value, StringComparison.Ordinal) == -1)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool ContainsAll(this string @this, StringComparison comparisonType, params string[] values)
        {
            foreach (var value in values)
            {
                if (@this.IndexOf(value, comparisonType) == -1)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool ContainsAny(this string @this, params string[] values)
        {
            foreach (var value in values)
            {
                if (@this.IndexOf(value, StringComparison.Ordinal) != -1)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool ContainsAny(this string @this, StringComparison comparisonType, params string[] values)
        {
            foreach (var value in values)
            {
                if (@this.IndexOf(value, comparisonType) != -1)
                {
                    return true;
                }
            }
            return false;
        }
        public static int ConvertToUtf32(this string s, int index)
        {
            return char.ConvertToUtf32(s, index);
        }
        public static string Copy(this string str)
        {
            return string.Copy(str);
        }
        public static string DecodeBase64(this string encodedValue)
        {
            return encodedValue.DecodeBase64(null);
        }
        public static string DecodeBase64(this string encodedValue, Encoding encoding)
        {
            encoding = encoding ?? Encoding.UTF8;
            var bytes = Convert.FromBase64String(encodedValue);
            return encoding.GetString(bytes);
        }
        public static string EncodeBase64(this string value)
        {
            return value.EncodeBase64(null);
        }
        public static string EncodeBase64(this string value, Encoding encoding)
        {
            encoding = encoding ?? Encoding.UTF8;
            var bytes = encoding.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }
        public static bool EqualsIgnoreCase(this string @this, string comparedString)
        {
            return @this.Equals(comparedString, StringComparison.OrdinalIgnoreCase);
        }
        public static string EscapeXml(this string @this)
        {
            return @this
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;")
                ;
        }
        public static string Extract(this string @this, Func<char, bool> predicate)
        {
            return new string(@this.ToCharArray().Where(predicate).ToArray());
        }
        public static string ExtractLetter(this string @this)
        {
            return new string(@this.ToCharArray().Where(char.IsLetter).ToArray());
        }
        public static IEnumerable<int> FindAllIndexOf(this string str, string substr, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException(nameof(str));
            }
            if (string.IsNullOrEmpty(substr))
            {
                throw new ArgumentException(nameof(substr));
            }
            var indexes = new List<int>();
            var index = 0;
            while (
                (index =
                    str.IndexOf(substr, index,
                        ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)) != -1)
            {
                indexes.Add(index++);
            }
            return indexes;
        }
        public static IEnumerable<int> FindAllIndexOf(this string text, string pattern)
        {
            var indices = new List<int>();
            foreach (Match match in Regex.Matches(text, pattern))
                indices.Add(match.Index);

            return indices;
        }
        public static IEnumerable<int> FindAllIndexOf(this string text, Regex pattern)
        {
            var indices = new List<int>();
            foreach (Match match in pattern.Matches(text))
                indices.Add(match.Index);

            return indices;
        }
        public static IEnumerable<int> FindAllIndexOf<T>(this T[] @this, Predicate<T> predicate) where T : class
        {
            var subArray = Array.FindAll(@this, predicate);
            return (from T item in subArray select Array.IndexOf(@this, item));
        }
        public static string Format(this string format, object[] args)
        {
            return string.Format(format, args);
        }
        public static long FromBase(this string input, string baseChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")
        {
            var srcBase = baseChars.Length;
            long id = 0;
            var text = new string(input.Reverse().ToArray());
            for (var i = 0; i < text.Length; i++)
            {
                var charIndex = baseChars.IndexOf(text[i]);
                id += charIndex * (long)Math.Pow(srcBase, i);
            }
            return id;
        }
        public static string GetAfter(this string @this, string value)
        {
            if (@this.IndexOf(value, StringComparison.Ordinal) == -1)
            {
                return "";
            }
            return @this.Substring(@this.IndexOf(value, StringComparison.Ordinal) + value.Length);
        }
        public static string GetBefore(this string @this, string value)
        {
            if (@this.IndexOf(value, StringComparison.Ordinal) == -1)
            {
                return "";
            }
            return @this.Substring(0, @this.IndexOf(value, StringComparison.Ordinal));
        }
        public static string GetBetween(this string @this, string before, string after)
        {
            var beforeStartIndex = @this.IndexOf(before, StringComparison.Ordinal);
            var startIndex = beforeStartIndex + before.Length;
            var afterStartIndex = @this.IndexOf(after, startIndex, StringComparison.Ordinal);

            if (beforeStartIndex == -1 || afterStartIndex == -1)
            {
                return "";
            }

            return @this.Substring(startIndex, afterStartIndex - startIndex);
        }
        public static MatchCollection GetMatches(this string value, string regexPattern)
        {
            return GetMatches(value, regexPattern, RegexOptions.None);
        }
        public static MatchCollection GetMatches(this string value, string regexPattern, RegexOptions options)
        {
            return Regex.Matches(value, regexPattern, options);
        }
        public static IEnumerable<string> GetMatchingValues(this string value, string regexPattern)
        {
            return GetMatchingValues(value, regexPattern, RegexOptions.None);
        }
        public static IEnumerable<string> GetMatchingValues(this string value, string regexPattern, RegexOptions options)
        {
            foreach (Match match in GetMatches(value, regexPattern, options))
            {
                if (match.Success) yield return match.Value;
            }
        }
        public static byte[] HexStringToByteArray(this string hexString)
        {
            int stringLength = hexString.Length;
            byte[] bytes = new byte[stringLength / 2];
            for (int i = 0; i < stringLength; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return bytes;
        }
        public static string HtmlDecode(this string s)
        {
            return HttpUtility.HtmlDecode(s);
        }
        public static void HtmlDecode(this string s, TextWriter output)
        {
            HttpUtility.HtmlDecode(s, output);
        }
        public static string HtmlEncode(this string s)
        {
            return HttpUtility.HtmlEncode(s);
        }
        public static void HtmlEncode(this string s, TextWriter output)
        {
            HttpUtility.HtmlEncode(s, output);
        }
        public static string IfEmpty(this string value, string defaultValue)
        {
            return (value.IsNotEmpty() ? value : defaultValue);
        }
        public static string IfNullOrWhiteSpaceElse(this string input, string nullAlternateValue)
        {
            return (!string.IsNullOrWhiteSpace(input)) ? input : nullAlternateValue;
        }
        public static string IfNullOrWhiteSpaceElse(this string input, Func<string> nullAlternateAction)
        {
            return (!string.IsNullOrWhiteSpace(input)) ? input : nullAlternateAction();
        }
        public static bool In(this string @this, params string[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }
        public static bool IsAlphabetic(this string @this)
        {
            return !Regex.IsMatch(@this, "[^a-zA-Z]");
        }
        public static bool IsAlphabeticNumeric(this string @this)
        {
            return !Regex.IsMatch(@this, "[^a-zA-Z0-9]");
        }
        public static bool IsControl(this string s, int index)
        {
            return char.IsControl(s, index);
        }
        public static bool IsDateTime(this string date)
        {
            var isDate = true;
            try
            {
                // ReSharper disable once UnusedVariable
                var dt = DateTime.Parse(date);
            }
            catch
            {
                isDate = false;
            }
            return isDate;
        }
        public static bool IsDigit(this string str)
        {
            return !string.IsNullOrEmpty(str) && str.All(char.IsDigit);
        }
        public static bool IsDigit(this string s, int index)
        {
            return char.IsDigit(s, index);
        }
        public static bool IsEmpty(this string @this)
        {
            return @this == "";
        }
        public static bool IsHighSurrogate(this string s, int index)
        {
            return char.IsHighSurrogate(s, index);
        }
        public static string IsInterned(this string str)
        {
            return string.IsInterned(str);
        }
        public static bool IsLetter(this string s, int index)
        {
            return char.IsLetter(s, index);
        }
        public static bool IsLetterOrDigit(this string s, int index)
        {
            return char.IsLetterOrDigit(s, index);
        }
        public static bool IsLike(this string @this, string pattern)
        {
            // Turn the pattern into regex pattern, and match the whole string with ^$
            var regexPattern = "^" + Regex.Escape(pattern) + "$";

            // Escape special character ?, #, *, [], and [!]
            regexPattern = regexPattern.Replace(@"\[!", "[^")
                .Replace(@"\[", "[")
                .Replace(@"\]", "]")
                .Replace(@"\?", ".")
                .Replace(@"\*", ".*")
                .Replace(@"\#", @"\d");

            return Regex.IsMatch(@this, regexPattern);
        }
        public static bool IsLower(this string s, int index)
        {
            return char.IsLower(s, index);
        }
        public static bool IsLowSurrogate(this string s, int index)
        {
            return char.IsLowSurrogate(s, index);
        }
        public static bool IsMatch(this string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }
        public static bool IsMatch(this string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }
        public static bool IsMatchingTo(this string value, string regexPattern)
        {
            return IsMatchingTo(value, regexPattern, RegexOptions.None);
        }
        public static bool IsMatchingTo(this string value, string regexPattern, RegexOptions options)
        {
            return Regex.IsMatch(value, regexPattern, options);
        }
        public static bool IsNotEmpty(this string @this)
        {
            return @this != "";
        }
        public static bool IsNotNull(this string @this)
        {
            return @this != null;
        }
        public static bool IsNotNullOrEmpty(this string @this)
        {
            return !string.IsNullOrEmpty(@this);
        }
        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
        public static bool IsNull(this string @this)
        {
            return @this == null;
        }
        public static bool IsNullOrEmpty(this string @this)
        {
            return string.IsNullOrEmpty(@this);
        }
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        public static bool IsNumber(this string s, int index)
        {
            return char.IsNumber(s, index);
        }
        public static bool IsNumeric(this string @this)
        {
            return !Regex.IsMatch(@this, "[^0-9]");
        }
        public static bool IsPunctuation(this string s, int index)
        {
            return char.IsPunctuation(s, index);
        }
        public static bool IsSeparator(this string s, int index)
        {
            return char.IsSeparator(s, index);
        }
        public static bool IsSurrogate(this string s, int index)
        {
            return char.IsSurrogate(s, index);
        }
        public static bool IsSurrogatePair(this string s, int index)
        {
            return char.IsSurrogatePair(s, index);
        }
        public static bool IsSymbol(this string s, int index)
        {
            return char.IsSymbol(s, index);
        }
        public static bool IsUpper(this string s, int index)
        {
            return char.IsUpper(s, index);
        }
        public static bool IsValidEmail(this string obj)
        {
            return Regex.IsMatch(obj, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z0-9]{1,30})(\]?)$");
        }
        public static bool IsValidIp(this string obj)
        {
            return Regex.IsMatch(obj, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
        }
        public static bool IsWhiteSpace(this string s, int index)
        {
            return char.IsWhiteSpace(s, index);
        }
        public static string JavaScriptStringEncode(this string value)
        {
            return HttpUtility.JavaScriptStringEncode(value);
        }
        public static string JavaScriptStringEncode(this string value, bool addDoubleQuotes)
        {
            return HttpUtility.JavaScriptStringEncode(value, addDoubleQuotes);
        }
        public static string Join(this string separator, string[] value)
        {
            return string.Join(separator, value);
        }
        public static string Join(this string separator, object[] values)
        {
            return string.Join(separator, values);
        }
        public static string Join<T>(this string separator, IEnumerable<T> values)
        {
            return string.Join(separator, values);
        }
        public static string Join(this string separator, IEnumerable<string> values)
        {
            return string.Join(separator, values);
        }
        public static string Join(this string separator, string[] value, int startIndex, int count)
        {
            return string.Join(separator, value, startIndex, count);
        }
        public static string Left(this string @this, int length)
        {
            return @this.Substring(0, length);
        }
        public static string LeftSafe(this string @this, int length)
        {
            return @this.Substring(0, Math.Min(length, @this.Length));
        }
        public static bool Like(this string value, string search)
        {
            return value.Contains(search) || value.StartsWith(search) || value.EndsWith(search);
        }
        public static Match Match(this string input, string pattern)
        {
            return Regex.Match(input, pattern);
        }
        public static Match Match(this string input, string pattern, RegexOptions options)
        {
            return Regex.Match(input, pattern, options);
        }
        public static MatchCollection Matches(this string input, string pattern)
        {
            return Regex.Matches(input, pattern);
        }
        public static MatchCollection Matches(this string input, string pattern, RegexOptions options)
        {
            return Regex.Matches(input, pattern, options);
        }
        public static string NewLineToBreakLine(this string @this)
        {
            return @this.Replace("\r\n", "<br />").Replace("\n", "<br />");
        }
        public static bool NotIn(this string @this, params string[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
        public static string NullIfEmpty(this string @this)
        {
            return @this == "" ? null : @this;
        }
        public static string PadBoth(this string value, int width, char padChar, bool truncate = false)
        {
            var diff = width - value.Length;
            if (diff == 0 || diff < 0 && !truncate)
            {
                return value;
            }
            if (diff < 0)
            {
                return value.Substring(0, width);
            }
            return value.PadLeft(width - diff / 2, padChar).PadRight(width, padChar);
        }
        public static NameValueCollection ParseQueryString(this string query)
        {
            return HttpUtility.ParseQueryString(query);
        }
        public static NameValueCollection ParseQueryString(this string query, Encoding encoding)
        {
            return HttpUtility.ParseQueryString(query, encoding);
        }
        public static string PathCombine(this string @this, params string[] paths)
        {
            var list = paths.ToList();
            list.Insert(0, @this);
            return Path.Combine(list.ToArray());
        }
        public static string RemoveAll(this string source, params string[] removeStrings)
        {
            var v = source;
            foreach (var s in removeStrings)
            {
                v = v.Replace(s, string.Empty);
            }
            return v;
        }
        public static string RemoveAllSpecialCharacters(this string value)
        {
            var sb = new StringBuilder(value.Length);
            foreach (var c in value.Where(char.IsLetterOrDigit))
                sb.Append(c);
            return sb.ToString();
        }
        public static string RemoveBefore(this string @this, char c, bool removeChar = true)
        {
            return @this.Substring(removeChar ? (@this.IndexOf(c) + 1) : @this.IndexOf(c));
        }
        public static string RemoveBeforeLastIndex(this string @this, char c, bool removeChar = true)
        {
            return @this.Substring(removeChar ? (@this.LastIndexOf(c) + 1) : @this.LastIndexOf(c));
        }
        public static string RemoveControlCharacters(this string input)
        {
            return
                input.Where(character => !char.IsControl(character))
                    .Aggregate(new StringBuilder(), (builder, character) => builder.Append(character))
                    .ToString();
        }
        public static string RemoveDiacritics(this string @this)
        {
            var normalizedString = @this.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var t in normalizedString)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(t);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(t);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
        public static string RemoveEmptyLines(this string text)
        {
            return Regex.Replace(text, @"^\s*$\n|\r", string.Empty, RegexOptions.Multiline).TrimEnd();
        }
        public static string RemoveFirst(this string instr, int number)
        {
            return instr.Substring(number);
        }
        public static string RemoveFirstCharacter(this string instr)
        {
            return instr.Substring(1);
        }
        public static string RemoveHtmlTags(this string htmlString)
        {
            return Regex.Replace(htmlString, @"<[^>]*(>|$)|&nbsp;|&zwnj;|&raquo;|&laquo;", string.Empty).Trim();
        }
        public static string RemoveLast(this string instr, int number)
        {
            return instr.Substring(0, instr.Length - number);
        }
        public static string RemoveLastCharacter(this string instr)
        {
            return instr.Substring(0, instr.Length - 1);
        }
        public static string RemoveLetter(this string @this)
        {
            return new string(@this.ToCharArray().Where(x => !char.IsLetter(x)).ToArray());
        }
        public static string RemoveNumber(this string @this)
        {
            return new string(@this.ToCharArray().Where(x => !char.IsNumber(x)).ToArray());
        }
        public static string RemoveWhere(this string @this, Func<char, bool> predicate)
        {
            return new string(@this.ToCharArray().Where(x => !predicate(x)).ToArray());
        }
        public static string RemoveWhiteSpaces(this string input)
        {
            return Regex.Replace(input, @"\s+", "");
        }
        public static string Repeat(this string @this, int repeatCount)
        {
            if (@this.Length == 1)
            {
                return new string(@this[0], repeatCount);
            }

            var sb = new StringBuilder(repeatCount * @this.Length);
            while (repeatCount-- > 0)
            {
                sb.Append(@this);
            }

            return sb.ToString();
        }
        public static string Replace(this string @this, int startIndex, int length, string value)
        {
            @this = @this.Remove(startIndex, length).Insert(startIndex, value);

            return @this;
        }
        public static string ReplaceAll(this string value, IEnumerable<string> oldValues,
                            Func<string, string> replacePredicate)
        {
            var sbStr = new StringBuilder(value);
            foreach (var oldValue in oldValues)
            {
                var newValue = replacePredicate(oldValue);
                sbStr.Replace(oldValue, newValue);
            }

            return sbStr.ToString();
        }
        public static string ReplaceAll(this string value, IEnumerable<string> oldValues, string newValue)
        {
            var sbStr = new StringBuilder(value);
            foreach (var oldValue in oldValues)
                sbStr.Replace(oldValue, newValue);

            return sbStr.ToString();
        }
        public static string ReplaceAll(this string value, IEnumerable<string> oldValues, IEnumerable<string> newValues)
        {
            var sbStr = new StringBuilder(value);
            var newValueEnum = newValues.GetEnumerator();
            foreach (var old in oldValues)
            {
                if (!newValueEnum.MoveNext())
                    throw new ArgumentOutOfRangeException(nameof(newValues),
                        "newValues sequence is shorter than oldValues sequence");
                sbStr.Replace(old, newValueEnum.Current ?? throw new InvalidOperationException());
            }
            if (newValueEnum.MoveNext())
            {
                throw new ArgumentOutOfRangeException(nameof(newValues),
                    "newValues sequence is longer than oldValues sequence");
            }
            newValueEnum.Dispose();
            return sbStr.ToString();
        }
        public static string ReplaceAt(this string str, int index, string replace)
        {
            var length = replace.Length;
            return str.Remove(index, Math.Min(length, str.Length - index))
                .Insert(index, replace);
        }
        public static string ReplaceAt(this string str, int index, int length, string replace)
        {
            return str.Remove(index, Math.Min(length, str.Length - index))
                .Insert(index, replace);
        }
        public static string ReplaceAt(this string str, int startIndex, long endIndex, string replace)
        {
            var length = endIndex - startIndex;
            return str.Remove(startIndex, Math.Min(Convert.ToInt32(length), str.Length - startIndex))
                .Insert(startIndex, replace);
        }
        public static string ReplaceAt(this string text, char target, string replace, ReplaceMode replaceMode = ReplaceMode.On)
        {
            var i = text.IndexOf(target);
            if (i > -1)
            {
                var part1 = text.Substring(0, i);
                var part2 = text.Substring(i + 1);
                switch (replaceMode)
                {
                    case ReplaceMode.On:
                        return part1 + replace + part2;
                    case ReplaceMode.Before:
                        return part1 + replace + target + part2;
                    case ReplaceMode.After:
                        return part1 + target + replace + part2;
                }
                return text;
            }
            else
            {
                return text;
            }

        }
        public static string ReplaceByEmpty(this string @this, params string[] values)
        {
            foreach (var value in values)
            {
                @this = @this.Replace(value, "");
            }

            return @this;
        }
        public static string ReplaceFirst(this string @this, string oldValue, string newValue)
        {
            var startIndex = @this.IndexOf(oldValue, StringComparison.Ordinal);
            return startIndex == -1 ? @this : @this.Remove(startIndex, oldValue.Length).Insert(startIndex, newValue);
        }
        public static string ReplaceFirst(this string @this, int number, string oldValue, string newValue)
        {
            var list = @this.Split(oldValue).ToList();
            var old = number + 1;
            var listStart = list.Take(old);
            var listEnd = list.Skip(old).ToList();

            return string.Join(newValue, listStart) +
                   (listEnd.Any() ? oldValue : "") +
                   string.Join(oldValue, listEnd);
        }
        public static string ReplaceLast(this string @this, string oldValue, string newValue)
        {
            var startIndex = @this.LastIndexOf(oldValue, StringComparison.Ordinal);
            return startIndex == -1 ? @this : @this.Remove(startIndex, oldValue.Length).Insert(startIndex, newValue);
        }
        public static string ReplaceLast(this string @this, int number, string oldValue, string newValue)
        {
            var list = @this.Split(oldValue).ToList();
            var old = Math.Max(0, list.Count - number - 1);
            var listStart = list.Take(old);
            var listEnd = list.Skip(old);

            return string.Join(oldValue, listStart) +
                   (old > 0 ? oldValue : "") +
                   string.Join(newValue, listEnd);
        }
        public static string ReplaceWhenEquals(this string @this, string oldValue, string newValue)
        {
            return @this == oldValue ? newValue : @this;
        }
        public static string ReplaceWhiteSpacesWithOne(this string input)
        {
            return Regex.Replace(input, @"\s+", " ");
        }
        public static string ReplaceWith(this string value, string regexPattern, string replaceValue)
        {
            return ReplaceWith(value, regexPattern, replaceValue, RegexOptions.None);
        }
        public static string ReplaceWith(this string value, string regexPattern, string replaceValue,
                            RegexOptions options)
        {
            return Regex.Replace(value, regexPattern, replaceValue, options);
        }
        public static string ReplaceWith(this string value, string regexPattern, MatchEvaluator evaluator)
        {
            return ReplaceWith(value, regexPattern, RegexOptions.None, evaluator);
        }
        public static string ReplaceWith(this string value, string regexPattern, RegexOptions options,
                            MatchEvaluator evaluator)
        {
            return Regex.Replace(value, regexPattern, evaluator, options);
        }
        public static string Reverse(this string @this)
        {
            if (@this.Length <= 1)
            {
                return @this;
            }

            var chars = @this.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
        public static string Right(this string @this, int length)
        {
            return @this.Substring(@this.Length - length);
        }
        public static string RightSafe(this string @this, int length)
        {
            return @this.Substring(Math.Max(0, @this.Length - length));
        }
        public static void SaveAs(this string @this, string fileName, bool append = false)
        {
            using (TextWriter tw = new StreamWriter(fileName, append))
            {
                tw.Write(@this);
            }
        }
        public static void SaveAs(this string @this, FileInfo file, bool append = false)
        {
            using (TextWriter tw = new StreamWriter(file.FullName, append))
            {
                tw.Write(@this);
            }
        }


        public static string SubstringTillEnd(this string source, int n)
        {
            if (n >= source.Length)
            {
                return source;
            }
            return source.Substring(source.Length - n);
        }

        public static string SubstringByIndex(this string source, int startIndex, int endIndex)
        {
            return source.Substring(startIndex, endIndex - startIndex);
        }

        public static string Slice(this string source, int start, int end)
        {
            if (end < 0) // Keep this for negative end support
            {
                end = source.Length + end;
            }
            var len = end - start;               // Calculate length
            return source.Substring(start, len); // Return Substring of length
        }
        public static string[] Split(this string @this, string separator, StringSplitOptions option = StringSplitOptions.None)
        {
            return @this.Split(new[] { separator }, option);
        }
        public static string[] Split(this string str, int chunkSize)
        {
            return
                Enumerable.Range(0, str.Length / chunkSize).Select(i => str.Substring(i * chunkSize, chunkSize)).ToArray();
        }
        public static string[] Split(this string value, string regexPattern)
        {
            return value.Split(regexPattern, RegexOptions.None);
        }
        public static string[] Split(this string value, string regexPattern, RegexOptions options)
        {
            return Regex.Split(value, regexPattern, options);
        }
        public static SqlDbType SqlTypeNameToSqlDbType(this string @this)
        {
            switch (@this.ToLower())
            {
                case "image": // 34 | "image" | SqlDbType.Image
                    return SqlDbType.Image;

                case "text": // 35 | "text" | SqlDbType.Text
                    return SqlDbType.Text;

                case "uniqueidentifier": // 36 | "uniqueidentifier" | SqlDbType.UniqueIdentifier
                    return SqlDbType.UniqueIdentifier;

                case "date": // 40 | "date" | SqlDbType.Date
                    return SqlDbType.Date;

                case "time": // 41 | "time" | SqlDbType.Time
                    return SqlDbType.Time;

                case "datetime2": // 42 | "datetime2" | SqlDbType.DateTime2
                    return SqlDbType.DateTime2;

                case "datetimeoffset": // 43 | "datetimeoffset" | SqlDbType.DateTimeOffset
                    return SqlDbType.DateTimeOffset;

                case "tinyint": // 48 | "tinyint" | SqlDbType.TinyInt
                    return SqlDbType.TinyInt;

                case "smallint": // 52 | "smallint" | SqlDbType.SmallInt
                    return SqlDbType.SmallInt;

                case "int": // 56 | "int" | SqlDbType.Int
                    return SqlDbType.Int;

                case "smalldatetime": // 58 | "smalldatetime" | SqlDbType.SmallDateTime
                    return SqlDbType.SmallDateTime;

                case "real": // 59 | "real" | SqlDbType.Real
                    return SqlDbType.Real;

                case "money": // 60 | "money" | SqlDbType.Money
                    return SqlDbType.Money;

                case "datetime": // 61 | "datetime" | SqlDbType.DateTime
                    return SqlDbType.DateTime;

                case "float": // 62 | "float" | SqlDbType.Float
                    return SqlDbType.Float;

                case "sql_variant": // 98 | "sql_variant" | SqlDbType.Variant
                    return SqlDbType.Variant;

                case "ntext": // 99 | "ntext" | SqlDbType.NText
                    return SqlDbType.NText;

                case "bit": // 104 | "bit" | SqlDbType.Bit
                    return SqlDbType.Bit;

                case "decimal": // 106 | "decimal" | SqlDbType.Decimal
                    return SqlDbType.Decimal;

                case "numeric": // 108 | "numeric" | SqlDbType.Decimal
                    return SqlDbType.Decimal;

                case "smallmoney": // 122 | "smallmoney" | SqlDbType.SmallMoney
                    return SqlDbType.SmallMoney;

                case "bigint": // 127 | "bigint" | SqlDbType.BigInt
                    return SqlDbType.BigInt;

                case "varbinary": // 165 | "varbinary" | SqlDbType.VarBinary
                    return SqlDbType.VarBinary;

                case "varchar": // 167 | "varchar" | SqlDbType.VarChar
                    return SqlDbType.VarChar;

                case "binary": // 173 | "binary" | SqlDbType.Binary
                    return SqlDbType.Binary;

                case "char": // 175 | "char" | SqlDbType.Char
                    return SqlDbType.Char;

                case "timestamp": // 189 | "timestamp" | SqlDbType.Timestamp
                    return SqlDbType.Timestamp;

                case "nvarchar": // 231 | "nvarchar", "sysname" | SqlDbType.NVarChar
                    return SqlDbType.NVarChar;
                case "sysname": // 231 | "nvarchar", "sysname" | SqlDbType.NVarChar
                    return SqlDbType.NVarChar;

                case "nchar": // 239 | "nchar" | SqlDbType.NChar
                    return SqlDbType.NChar;

                case "hierarchyid": // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                    return SqlDbType.Udt;
                case "geometry": // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                    return SqlDbType.Udt;
                case "geography": // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                    return SqlDbType.Udt;

                case "xml": // 241 | "xml" | SqlDbType.Xml
                    return SqlDbType.Xml;

                default:
                    throw new Exception(
                        $"Unsupported Type: {@this}. Please let us know about this type and we will support it: sales@zzzprojects.com");
            }
        }
        public static byte[] ToAsciiByteArray(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
        public static byte[] ToByteArray(this string @this)
        {
            Encoding encoding = Activator.CreateInstance<ASCIIEncoding>();
            return encoding.GetBytes(@this);
        }
        public static byte[] ToByteArray<TEncoding>(this string str) where TEncoding : Encoding
        {
            Encoding enc = Activator.CreateInstance<TEncoding>();
            return enc.GetBytes(str);
        }
        public static IEnumerable<char> ToChars(this string str)
        {
            return str.Select(x => x);
        }
        public static DirectoryInfo ToDirectoryInfo(this string @this)
        {
            return new DirectoryInfo(@this);
        }
        public static T ToEnum<T>(this string @this)
        {
            var enumType = typeof(T);
            return (T)Enum.Parse(enumType, @this);
        }
        public static FileInfo ToFileInfo(this string @this)
        {
            return new FileInfo(@this);
        }
        public static IEnumerable<string> ToLines(this string str, StringSplitOptions stringSplitOptions = StringSplitOptions.RemoveEmptyEntries)
        {
            return str.Split(new[] { Environment.NewLine }, stringSplitOptions);
        }
        public static MemoryStream ToMemoryStream(this string text)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(text ?? ""));
        }
        public static SecureString ToSecureString(this string @this)
        {
            var secureString = new SecureString();
            foreach (var c in @this)
                secureString.AppendChar(c);

            return secureString;
        }
        public static Stream ToStream(this string str)
        {
            return str.ToStream<ASCIIEncoding>();
        }
        public static Stream ToStream<TEncoding>(this string str) where TEncoding : Encoding
        {
            return new MemoryStream(str.ToByteArray<TEncoding>());
        }
        public static string ToTitleCase(this string @this, CultureInfo culture)
        {
            return culture.TextInfo.ToTitleCase(@this);
        }
        public static string ToTitleCase(this string @this)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(@this);
        }
        public static string ToUnicodeString(this string text, bool ignoreWhiteSpaces = false)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char ch in text)
            {
                if (ignoreWhiteSpaces && char.IsWhiteSpace(ch))
                {
                    builder.Append(" ");
                }
                else
                {
                    builder.Append(@"\u");
                    builder.AppendFormat("{0:x4}", (int)ch);
                }
            }
            return builder.ToString();
        }
        public static byte[] ToUtf8ByteArray(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
        public static DateTime? ToValidDateTimeOrNull(this string @this)
        {
            if (DateTime.TryParse(@this, out var date))
            {
                return date;
            }

            return null;
        }
        public static IEnumerable<string> ToWords(this string str, string[] wordSeparators = null)
        {
            var ws = wordSeparators.IsNullOrEmpty() ? new[] { " ", "\r\n", "\n", Environment.NewLine } : wordSeparators;
            return str.Split(ws, StringSplitOptions.RemoveEmptyEntries);
        }
        public static XDocument ToXDocument(this string xml)
        {
            Encoding encoding = Activator.CreateInstance<UTF8Encoding>();
            using (var ms = new MemoryStream(encoding.GetBytes(xml)))
            {
                return XDocument.Load(ms);
            }
        }
        public static XDocument ToXDocument<TEncoding>(this string xml) where TEncoding : Encoding
        {
            Encoding encoding = Activator.CreateInstance<TEncoding>();
            using (var ms = new MemoryStream(encoding.GetBytes(xml)))
            {
                return XDocument.Load(ms);
            }
        }
        public static XElement ToXElement(this string xml)
        {
            return XElement.Parse(xml);
        }
        public static XmlDocument ToXmlDocument(this string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }
        public static XmlElement ToXmlElement(this string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc.DocumentElement;
        }
        public static string TrimToMaxLength(this string value, int maxLength)
        {
            return value == null || value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
        public static string TrimToMaxLength(this string value, int maxLength, string suffix)
        {
            return value == null || value.Length <= maxLength
                ? value
                : string.Concat(value.Substring(0, maxLength), suffix);
        }
        public static string Truncate(this string @this, int maxLength)
        {
            const string suffix = "...";

            if (@this == null || @this.Length <= maxLength)
            {
                return @this;
            }

            var strLength = maxLength - suffix.Length;
            return @this.Substring(0, strLength) + suffix;
        }
        public static string Truncate(this string @this, int maxLength, string suffix)
        {
            if (@this == null || @this.Length <= maxLength)
            {
                return @this;
            }

            var strLength = maxLength - suffix.Length;
            return @this.Substring(0, strLength) + suffix;
        }
        public static string UrlDecode(this string str)
        {
            return HttpUtility.UrlDecode(str);
        }
        public static string UrlDecode(this string str, Encoding e)
        {
            return HttpUtility.UrlDecode(str, e);
        }
        public static byte[] UrlDecodeToBytes(this string str)
        {
            return HttpUtility.UrlDecodeToBytes(str);
        }
        public static byte[] UrlDecodeToBytes(this string str, Encoding e)
        {
            return HttpUtility.UrlDecodeToBytes(str, e);
        }
        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }
        public static string UrlEncode(this string str, Encoding e)
        {
            return HttpUtility.UrlEncode(str, e);
        }
        public static byte[] UrlEncodeToBytes(this string str)
        {
            return HttpUtility.UrlEncodeToBytes(str);
        }
        public static byte[] UrlEncodeToBytes(this string str, Encoding e)
        {
            return HttpUtility.UrlEncodeToBytes(str, e);
        }
        public static string UrlPathEncode(this string str)
        {
            return HttpUtility.UrlPathEncode(str);
        }
    }
}
