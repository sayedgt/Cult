using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using JetBrainsNotNullAttribute = JetBrains.Annotations.NotNullAttribute;

namespace Cult.Guard
{
	public static partial class GuardExtensions
	{

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

        private static IGuard Positive<T>([NotNull, JetBrainsNotNull] this IGuard guard, T input, [NotNull, JetBrainsNotNull] string parameterName) where T : struct, IComparable
        {
            if (input.CompareTo(default(T)) > 0)
                throw new ArgumentException($"Required input {parameterName} cannot be negative.", parameterName);

            return guard;
        }

    }
}
