using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using JetBrainsNotNullAttribute = JetBrains.Annotations.NotNullAttribute;
namespace Cult.Guard
{
	public static partial class GuardExtensions
	{
        public static IGuard InvalidFormat([NotNull, JetBrainsNotNull] this IGuard guard, [NotNull, JetBrainsNotNull] string input, [NotNull, JetBrainsNotNull] string parameterName, [NotNull, JetBrainsNotNull] string regexPattern)
        {
            if (input != Regex.Match(input, regexPattern).Value)
                throw new ArgumentException($"Input {parameterName} was not in required format.", parameterName);

            return guard;
        }

        public static IGuard InvalidFormat([NotNull, JetBrainsNotNull] this IGuard guard, [NotNull, JetBrainsNotNull] string input, [NotNull, JetBrainsNotNull] string parameterName, [NotNull, JetBrainsNotNull] Regex regex)
        {
            if (!regex.IsMatch(input))
                throw new ArgumentException($"Input {parameterName} was not in required format.", parameterName);

            return guard;
        }

        public static IGuard InvalidInput<T>([NotNull, JetBrainsNotNull] this IGuard guard, [JetBrainsNotNull] T input, [NotNull, JetBrainsNotNull] string parameterName, Func<T, bool> predicate)
        {
            if (!predicate(input))
                throw new ArgumentException($"Input {parameterName} did not satisfy the options.", parameterName);

            return guard;
        }
    }
}
