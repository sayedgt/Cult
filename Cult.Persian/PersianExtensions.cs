using System.Text.RegularExpressions;

namespace Cult.Persian
{
    public static class PersianExtensions
    {
        private const char ArabicKeChar = (char)1603;
        private const char ArabicYeChar1 = (char)1609;
        private const char ArabicYeChar2 = (char)1610;
        private const char ArabicYeWithOneDotBelow = (char)1568;
        private const char ArabicYeWithInvertedV = (char)1597;
        private const char ArabicYeWithTwoDotsAbove = (char)1598;
        private const char ArabicYeWithThreeDotsAbove = (char)1599;
        private const char ArabicYeWithHighHamzeYeh = (char)1656;
        private const char ArabicYeWithFinalForm = (char)1744;
        private const char ArabicYeWithThreeDotsBelow = (char)1745;
        private const char ArabicYeWithTail = (char)1741;
        private const char ArabicYeSmallV = (char)1742;
        private const char PersianKeChar = (char)1705;
        private const char PersianYeChar = (char)1740;
        private static readonly Regex _matchArabicHebrew = new Regex(@"[\u0600-\u06FF,\u0590-\u05FF,«,»]", options: RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex _matchOnlyPersianNumbersRange = new Regex(@"^[\u06F0-\u06F9]+$", options: RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex _matchOnlyPersianLetters = new Regex(@"^[\\s,\u06A9\u06AF\u06C0\u06CC\u060C,\u062A\u062B\u062C\u062D\u062E\u062F,\u063A\u064A\u064B\u064C\u064D\u064E,\u064F\u067E\u0670\u0686\u0698\u200C,\u0621-\u0629\u0630-\u0639\u0641-\u0654]+$", options: RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex _hasHalfSpaces = new Regex(@"\u200B|\u200C|\u200E|\u200F", options: RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static string ApplyCorrectYeKe(this string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return string.Empty;

            var dataChars = data.ToCharArray();
            for (var i = 0; i < dataChars.Length; i++)
            {
                switch (dataChars[i])
                {
                    case ArabicYeChar1:
                    case ArabicYeChar2:
                    case ArabicYeWithOneDotBelow:
                    case ArabicYeWithInvertedV:
                    case ArabicYeWithTwoDotsAbove:
                    case ArabicYeWithThreeDotsAbove:
                    case ArabicYeWithHighHamzeYeh:
                    case ArabicYeWithFinalForm:
                    case ArabicYeWithThreeDotsBelow:
                    case ArabicYeWithTail:
                    case ArabicYeSmallV:
                        dataChars[i] = PersianYeChar;
                        break;

                    case ArabicKeChar:
                        dataChars[i] = PersianKeChar;
                        break;

                    default:
                        dataChars[i] = dataChars[i];
                        break;
                }
            }

            return new string(dataChars);
        }
        public static string CorrectArabicLetters(this string text)
        {
            text
                .Replace("ي", "ی")
                .Replace("ك", "ک")
                .Replace("دِ", "د")
                .Replace("بِ", "ب")
                .Replace("زِ", "ز")
                .Replace("ذِ", "ذ")
                .Replace("ِشِ", "ش")
                .Replace("ِسِ", "س")
                .Replace("ى", "ی")
                .Replace("ة", "ه")
                .Replace("‍", "")
                .ApplyCorrectYeKe();
            return text;
        }
        public static bool ContainsFarsi(this string txt)
        {
            return !string.IsNullOrEmpty(txt) && _matchArabicHebrew.IsMatch(txt);
        }
        public static bool ContainsOnlyFarsiLetters(this string txt)
        {
            return !string.IsNullOrEmpty(txt) && _matchOnlyPersianLetters.IsMatch(txt);
        }
        public static bool ContainsOnlyPersianNumbers(this string text, bool ignoreCommaSeparator = false)
        {
            return !string.IsNullOrEmpty(text) && _matchOnlyPersianNumbersRange.IsMatch(ignoreCommaSeparator ? text.Replace(",", "") : text);
        }
        public static bool ContainsHalfSpace(this string text) => _hasHalfSpaces.IsMatch(text);
    }
}
