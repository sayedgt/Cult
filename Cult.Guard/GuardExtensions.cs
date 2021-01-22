using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrainsNotNullAttribute = JetBrains.Annotations.NotNullAttribute;

// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable UnusedMember.Global
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

namespace Cult.Guard
{
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
    public static class GuardClauseExtensions
    {
        private static void IsSafe([NotNull, JetBrainsNotNull] this IGuard guard, [NotNull, JetBrainsNotNull] string parameterName)
        {
            if (guard == null) throw new ArgumentNullException(nameof(guard));
            if (parameterName == null) throw new ArgumentNullException(nameof(parameterName));
        }

        public static IGuard IsNull<T>([NotNull, JetBrainsNotNull] this IGuard guard, [NotNull, JetBrainsNotNull] T? input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            IsSafe(guard, parameterName);

            if (input is null)
                throw new ArgumentNullException(parameterName);

            return guard;
        }

        public static IGuard NullOrEmpty([NotNull, JetBrainsNotNull] this IGuard guard, [NotNull, JetBrainsNotNull] string? input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            Guard.That.IsNull(input, parameterName);
            if (input.Length == 0)
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);

            return guard;
        }

        public static IGuard NullOrEmpty([NotNull, JetBrainsNotNull] this IGuard guard, [NotNull, JetBrainsNotNull] Guid? input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            Guard.That.IsNull(input, parameterName);
            if (input == Guid.Empty)
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);

            return guard;
        }

        public static IGuard NullOrEmpty<T>([NotNull, JetBrainsNotNull] this IGuard guard, [NotNull, JetBrainsNotNull] IEnumerable<T>? input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            Guard.That.IsNull(input, parameterName);
            if (!input.Any())
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);

            return guard;
        }

        public static IGuard NullOrWhiteSpace([NotNull, JetBrainsNotNull] this IGuard guard, [NotNull, JetBrainsNotNull] string? input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            Guard.That.NullOrEmpty(input, parameterName);
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);

            return guard;
        }

        public static IGuard OutOfRange([NotNull, JetBrainsNotNull] this IGuard guard, int input, [NotNull, JetBrainsNotNull] string parameterName, int rangeFrom, int rangeTo)
        {
            return OutOfRange<int>(guard, input, parameterName, rangeFrom, rangeTo);
        }

        public static IGuard OutOfRange([NotNull, JetBrainsNotNull] this IGuard guard, DateTime input, [NotNull, JetBrainsNotNull] string parameterName, DateTime rangeFrom, DateTime rangeTo)
        {
            return OutOfRange<DateTime>(guard, input, parameterName, rangeFrom, rangeTo);
        }

        public static IGuard OutOfSqlDateRange([NotNull, JetBrainsNotNull] this IGuard guard, DateTime input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            // System.Data is unavailable in .NET Standard so we can't use SqlDateTime.
            const long sqlMinDateTicks = 552877920000000000;
            const long sqlMaxDateTicks = 3155378975999970000;

            return OutOfRange<DateTime>(guard, input, parameterName, new DateTime(sqlMinDateTicks), new DateTime(sqlMaxDateTicks));
        }

        public static IGuard OutOfRange(this IGuard guard, decimal input, string parameterName, decimal rangeFrom, decimal rangeTo)
        {
            return OutOfRange<decimal>(guard, input, parameterName, rangeFrom, rangeTo);
        }

        public static IGuard OutOfRange(this IGuard guard, short input, string parameterName, short rangeFrom, short rangeTo)
        {
            return OutOfRange<short>(guard, input, parameterName, rangeFrom, rangeTo);
        }

        public static IGuard OutOfRange(this IGuard guard, double input, string parameterName, double rangeFrom, double rangeTo)
        {
            return OutOfRange<double>(guard, input, parameterName, rangeFrom, rangeTo);
        }

        public static IGuard OutOfRange(this IGuard guard, float input, string parameterName, float rangeFrom, float rangeTo)
        {
            return OutOfRange<float>(guard, input, parameterName, rangeFrom, rangeTo);
        }

        private static IGuard OutOfRange<T>(this IGuard guard, T input, string parameterName, T rangeFrom, T rangeTo)
        {
            Comparer<T> comparer = Comparer<T>.Default;

            if (comparer.Compare(rangeFrom, rangeTo) > 0)
                throw new ArgumentException($"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}");

            if (comparer.Compare(input, rangeFrom) < 0 || comparer.Compare(input, rangeTo) > 0)
                throw new ArgumentOutOfRangeException(parameterName, $"Input {parameterName} was out of range");

            return guard;
        }

        public static IGuard Zero([NotNull, JetBrainsNotNull] this IGuard guard, int input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return Zero<int>(guard, input, parameterName);
        }

        public static IGuard Zero([NotNull, JetBrainsNotNull] this IGuard guard, long input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return Zero<long>(guard, input, parameterName);
        }

        public static IGuard Zero([NotNull, JetBrainsNotNull] this IGuard guard, decimal input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return Zero<decimal>(guard, input, parameterName);
        }

        public static IGuard Zero([NotNull, JetBrainsNotNull] this IGuard guard, float input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return Zero<float>(guard, input, parameterName);
        }

        public static IGuard Zero([NotNull, JetBrainsNotNull] this IGuard guard, double input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return Zero<double>(guard, input, parameterName);
        }

        private static IGuard Zero<T>([NotNull, JetBrainsNotNull] this IGuard guard, T input, [NotNull, JetBrainsNotNull] string parameterName) where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(input, default))
                throw new ArgumentException($"Required input {parameterName} cannot be zero.", parameterName);

            return guard;
        }

        public static IGuard Negative([NotNull, JetBrainsNotNull] this IGuard guard, int input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return Negative<int>(guard, input, parameterName);
        }

        public static IGuard Negative([NotNull, JetBrainsNotNull] this IGuard guard, long input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return Negative<long>(guard, input, parameterName);
        }

        public static IGuard Negative([NotNull, JetBrainsNotNull] this IGuard guard, decimal input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return Negative<decimal>(guard, input, parameterName);
        }

        public static IGuard Negative([NotNull, JetBrainsNotNull] this IGuard guard, float input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return Negative<float>(guard, input, parameterName);
        }

        public static IGuard Negative([NotNull, JetBrainsNotNull] this IGuard guard, double input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return Negative<double>(guard, input, parameterName);
        }

        private static IGuard Negative<T>([NotNull, JetBrainsNotNull] this IGuard guard, T input, [NotNull, JetBrainsNotNull] string parameterName) where T : struct, IComparable
        {
            if (input.CompareTo(default(T)) < 0)
                throw new ArgumentException($"Required input {parameterName} cannot be negative.", parameterName);

            return guard;
        }

        public static IGuard NegativeOrZero([NotNull, JetBrainsNotNull] this IGuard guard, int input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return NegativeOrZero<int>(guard, input, parameterName);
        }

        public static IGuard NegativeOrZero([NotNull, JetBrainsNotNull] this IGuard guard, long input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return NegativeOrZero<long>(guard, input, parameterName);
        }

        public static IGuard NegativeOrZero([NotNull, JetBrainsNotNull] this IGuard guard, decimal input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return NegativeOrZero<decimal>(guard, input, parameterName);
        }

        public static IGuard NegativeOrZero([NotNull, JetBrainsNotNull] this IGuard guard, float input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return NegativeOrZero<float>(guard, input, parameterName);
        }

        public static IGuard NegativeOrZero([NotNull, JetBrainsNotNull] this IGuard guard, double input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            return NegativeOrZero<double>(guard, input, parameterName);
        }

        private static IGuard NegativeOrZero<T>([NotNull, JetBrainsNotNull] this IGuard guard, T input, [NotNull, JetBrainsNotNull] string parameterName) where T : struct, IComparable
        {
            if (input.CompareTo(default(T)) <= 0)
            {
                throw new ArgumentException($"Required input {parameterName} cannot be zero or negative.",
                    parameterName);
            }

            return guard;
        }

        public static IGuard OutOfRange<T>([NotNull, JetBrainsNotNull] this IGuard guard, int input, [NotNull, JetBrainsNotNull] string parameterName) where T : struct, Enum
        {
            if (!Enum.IsDefined(typeof(T), input))
            {
                throw new InvalidEnumArgumentException(parameterName, Convert.ToInt32(input), typeof(T));
            }

            return guard;
        }

        public static IGuard OutOfRange<T>([NotNull, JetBrainsNotNull] this IGuard guard, T input, [NotNull, JetBrainsNotNull] string parameterName) where T : struct, Enum
        {
            if (!Enum.IsDefined(typeof(T), input))
                throw new InvalidEnumArgumentException(parameterName, Convert.ToInt32(input), typeof(T));

            return guard;
        }

        public static IGuard Default<T>([NotNull, JetBrainsNotNull] this IGuard guard, [AllowNull, NotNull, JetBrainsNotNull] T input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            if (input is null || EqualityComparer<T>.Default.Equals(input, default!))
            {
                throw new ArgumentException($"Parameter [{parameterName}] is default value for type {typeof(T).Name}",
                    parameterName);
            }

            return guard;
        }

        public static IGuard InvalidFormat([NotNull, JetBrainsNotNull] this IGuard guard, [NotNull, JetBrainsNotNull] string input, [NotNull, JetBrainsNotNull] string parameterName, [NotNull, JetBrainsNotNull] string regexPattern)
        {
            if (input != Regex.Match(input, regexPattern).Value)
                throw new ArgumentException($"Input {parameterName} was not in required format.", parameterName);

            return guard;
        }

        public static IGuard InvalidInput<T>([NotNull, JetBrainsNotNull] this IGuard guard, [JetBrainsNotNull] T input, [NotNull, JetBrainsNotNull] string parameterName, Func<T, bool> predicate)
        {
            if (!predicate(input))
                throw new ArgumentException($"Input {parameterName} did not satisfy the options.", parameterName);

            return guard;
        }


        private static IGuard OutOfRange<T>(this IGuard guard, IEnumerable<T> input, string parameterName, T rangeFrom, T rangeTo) where T : IComparable
        {
            Comparer<T> comparer = Comparer<T>.Default;

            if (comparer.Compare(rangeFrom, rangeTo) > 0)
            {
                throw new ArgumentException($"{nameof(rangeFrom)} should be less or equal than {nameof(rangeTo)}.");
            }

            if (input.Any(x => comparer.Compare(x, rangeFrom) < 0 || comparer.Compare(x, rangeTo) > 0))
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Input {parameterName} had out of range item(s).");
            }

            return guard;
        }

        public static IGuard OutOfRange([NotNull, JetBrainsNotNull] this IGuard guard, IEnumerable<int> input, [NotNull, JetBrainsNotNull] string parameterName, int rangeFrom, int rangeTo)
        {
            return OutOfRange<int>(guard, input, parameterName, rangeFrom, rangeTo);
        }

        public static IGuard OutOfRange([NotNull, JetBrainsNotNull] this IGuard guard, IEnumerable<long> input, [NotNull, JetBrainsNotNull] string parameterName, long rangeFrom, long rangeTo)
        {
            return OutOfRange<long>(guard, input, parameterName, rangeFrom, rangeTo);
        }

        public static IGuard OutOfRange([NotNull, JetBrainsNotNull] this IGuard guard, IEnumerable<decimal> input, [NotNull, JetBrainsNotNull] string parameterName, decimal rangeFrom, decimal rangeTo)
        {
            return OutOfRange<decimal>(guard, input, parameterName, rangeFrom, rangeTo);
        }

        public static IGuard OutOfRange([NotNull, JetBrainsNotNull] this IGuard guard, IEnumerable<float> input, [NotNull, JetBrainsNotNull] string parameterName, float rangeFrom, float rangeTo)
        {
            return OutOfRange<float>(guard, input, parameterName, rangeFrom, rangeTo);
        }

        public static IGuard OutOfRange([NotNull, JetBrainsNotNull] this IGuard guard, IEnumerable<double> input, [NotNull, JetBrainsNotNull] string parameterName, double rangeFrom, double rangeTo)
        {
            return OutOfRange<double>(guard, input, parameterName, rangeFrom, rangeTo);
        }

        public static IGuard OutOfRange([NotNull, JetBrainsNotNull] this IGuard guard, IEnumerable<short> input, [NotNull, JetBrainsNotNull] string parameterName, short rangeFrom, short rangeTo)
        {
            return OutOfRange<short>(guard, input, parameterName, rangeFrom, rangeTo);
        }
    }
}
