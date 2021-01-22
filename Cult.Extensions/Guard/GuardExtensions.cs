using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Cult.Extensions.ExtraObject;

// ReSharper disable All

namespace Cult.Extensions.Guard
{
    // https://github.com/ardalis/GuardClauses
    // https://github.com/safakgur/guard
    // https://github.com/danielwertheim/Ensure.That
    // https://github.com/3komma14/Guard
    // https://github.com/adamralph/liteguard
    // https://github.com/feO2x/Light.GuardClauses
    // https://github.com/haacked/NullGuard
    // https://github.com/dustindavis/FluentGuard
    // https://github.com/george-pancescu/Guard
    // https://github.com/pmcilreavy/GuardThat
    // https://github.com/BoasE/FluentGuard
    public static class GuardExtensions
    {
        private static void Self(this IGuard guard, string parameterName)
        {
            if (guard == null) throw new ArgumentNullException(nameof(guard));
            if (parameterName == null) throw new ArgumentNullException(nameof(parameterName));
        }
        private static void OutOfRange<T>(this IGuard guard, T input, string parameterName, T from, T to)
        {
            Comparer<T> comparer = Comparer<T>.Default;

            if (comparer.Compare(from, to) > 0)
                throw new ArgumentException($"{nameof(from)} should be less or equal than {nameof(to)}");

            if (comparer.Compare(input, from) < 0 || comparer.Compare(input, to) > 0)
                throw new ArgumentOutOfRangeException(parameterName, $"The input {parameterName} is out of range");
        }

        public static IGuard IsNull<T>(this IGuard guard, T input, string parameterName)
        {
            Self(guard, parameterName);

            if (input == null)
                throw new ArgumentNullException(parameterName);

            return guard;
        }
        public static IGuard IsNotNull<T>(this IGuard guard, T input, string parameterName)
        {
            Self(guard, parameterName);

            if (input != null)
                throw new ArgumentException($"The required input {parameterName} is not null.", parameterName);

            return guard;
        }

        public static IGuard IsNotNullOrEmpty(this IGuard guard, string input, string parameterName)
        {
            Guard.That.IsNotNull(input, parameterName);
            if (input != string.Empty)
                throw new ArgumentException($"The required input {parameterName} is not empty.", parameterName);

            return guard;
        }
        public static IGuard IsNullOrEmpty(this IGuard guard, string input, string parameterName)
        {
            Guard.That.IsNull(input, parameterName);
            if (input == string.Empty)
                throw new ArgumentException($"The required input {parameterName} is empty.", parameterName);

            return guard;
        }
        public static IGuard IsNullOrEmpty(this IGuard guard, Guid? input, string parameterName)
        {
            Guard.That.IsNull(input, parameterName);
            if (input == Guid.Empty)
                throw new ArgumentException($"The required input {parameterName} is empty.", parameterName);

            return guard;
        }
        public static IGuard IsNullOrEmpty<T>(this IGuard guard, IEnumerable<T> input, string parameterName)
        {
            Guard.That.IsNull(input, parameterName);
            if (!input.Any())
                throw new ArgumentException($"The required input {parameterName} is empty.", parameterName);

            return guard;
        }
        public static IGuard IsNullOrEmpty<T>(this IGuard guard, IList<T> input, string parameterName)
        {
            Guard.That.IsNull(input, parameterName);
            if (!input.Any())
                throw new ArgumentException($"The required input {parameterName} is empty.", parameterName);

            return guard;
        }
        public static IGuard IsNullOrEmpty<T>(this IGuard guard, ICollection<T> input, string parameterName)
        {
            Guard.That.IsNull(input, parameterName);
            if (!input.Any())
                throw new ArgumentException($"The required input {parameterName} is empty.", parameterName);

            return guard;
        }
        public static IGuard IsNullOrEmpty<T>(this IGuard guard, IReadOnlyCollection<T> input, string parameterName)
        {
            Guard.That.IsNull(input, parameterName);
            if (!input.Any())
                throw new ArgumentException($"The required input {parameterName} is empty.", parameterName);

            return guard;
        }
        public static IGuard IsNullOrEmpty<T>(this IGuard guard, T[] input, string parameterName)
        {
            Guard.That.IsNull(input, parameterName);
            if (!input.Any())
                throw new ArgumentException($"The required input {parameterName} is empty.", parameterName);

            return guard;
        }
        public static IGuard IsNullOrWhiteSpace(this IGuard guard, string input, string parameterName)
        {
            Guard.That.IsNullOrEmpty(input, parameterName);
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException($"The required input {parameterName} has whitespace.", parameterName);

            return guard;
        }
        public static IGuard IsDefault<T>(this IGuard guard, T input, string parameterName)
        {
            if (input.IsDefault())
                throw new ArgumentException($"The parameter [{parameterName}] is default value for type {typeof(T).Name}.", parameterName);

            return guard;
        }
        public static IGuard IsNotDefault<T>(this IGuard guard, T input, string parameterName)
        {
            if (!input.IsDefault())
                throw new ArgumentException($"The parameter [{parameterName}] is not default value for type {typeof(T).Name}.", parameterName);

            return guard;
        }
        public static IGuard IsInvalidFormat(this IGuard guard, string input, string parameterName, string regexPattern)
        {
            if (input != Regex.Match(input, regexPattern).Value)
                throw new ArgumentException($"The input {parameterName} was not in required format.", parameterName);

            return guard;
        }
        public static IGuard IsValidFormat(this IGuard guard, string input, string parameterName, string regexPattern)
        {
            if (input == Regex.Match(input, regexPattern).Value)
                throw new ArgumentException($"The input {parameterName} was in required format.", parameterName);

            return guard;
        }
        public static IGuard IsOutOfRange(this IGuard guard, int input, string parameterName, int from, int to, RangeStatus rangeStatus)
        {


            return guard;
        }
        public static IGuard IsOutOfRange(this IGuard guard, float input, string parameterName, float from, float to, RangeStatus rangeStatus)
        {


            return guard;
        }
        public static IGuard IsOutOfRange(this IGuard guard, double input, string parameterName, double from, double to, RangeStatus rangeStatus)
        {


            return guard;
        }
        public static IGuard IsOutOfRange(this IGuard guard, decimal input, string parameterName, decimal from, decimal to, RangeStatus rangeStatus)
        {


            return guard;
        }
        public static IGuard IsOutOfRange(this IGuard guard, long input, string parameterName, long from, long to, RangeStatus rangeStatus)
        {


            return guard;
        }
        public static IGuard IsOutOfRange(this IGuard guard, DateTime input, string parameterName, DateTime from, DateTime to, RangeStatus rangeStatus)
        {


            return guard;
        }
        public static IGuard IsInRange(this IGuard guard, int input, string parameterName, int from, int to, RangeStatus rangeStatus)
        {


            return guard;
        }
        public static IGuard IsInRange(this IGuard guard, float input, string parameterName, float from, float to, RangeStatus rangeStatus)
        {


            return guard;
        }
        public static IGuard IsInRange(this IGuard guard, double input, string parameterName, double from, double to, RangeStatus rangeStatus)
        {


            return guard;
        }
        public static IGuard IsInRange(this IGuard guard, decimal input, string parameterName, decimal from, decimal to, RangeStatus rangeStatus)
        {


            return guard;
        }
        public static IGuard IsInRange(this IGuard guard, long input, string parameterName, long from, long to, RangeStatus rangeStatus)
        {


            return guard;
        }
        public static IGuard IsInRange(this IGuard guard, DateTime input, string parameterName, DateTime from, DateTime to, RangeStatus rangeStatus)
        {


            return guard;
        }
    }
}