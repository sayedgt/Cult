using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using JetBrainsNotNullAttribute = JetBrains.Annotations.NotNullAttribute;

// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable UnusedMember.Global
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

namespace Cult.Guard
{
    public static partial class GuardExtensions
    {
        private static void Safe([NotNull, JetBrainsNotNull] this IGuard guard, [NotNull, JetBrainsNotNull] string parameterName)
        {
            if (guard == null) throw new ArgumentNullException(nameof(guard));
            if (parameterName == null) throw new ArgumentNullException(nameof(parameterName));
        }

        public static IGuard Null<T>([NotNull, JetBrainsNotNull] this IGuard guard, [NotNull, JetBrainsNotNull] T? input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            Safe(guard, parameterName);

            if (input is null)
                throw new ArgumentNullException(parameterName);

            return guard;
        }
        public static IGuard NotNull<T>([NotNull, JetBrainsNotNull] this IGuard guard, [AllowNull] T? input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            Safe(guard, parameterName);

            if (input is not null)
                throw new ArgumentException($"The required input {parameterName} is not null.", parameterName);

            return guard;
        }

        public static IGuard NullOrEmpty([NotNull, JetBrainsNotNull] this IGuard guard, [NotNull, JetBrainsNotNull] string? input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            Guard.Against.Null(input, parameterName);
            if (input.Length == 0)
                throw new ArgumentException($"Thr equired input {parameterName} is empty.", parameterName);

            return guard;
        }

        public static IGuard NotNullOrEmpty([NotNull, JetBrainsNotNull] this IGuard guard, [AllowNull] string? input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            Guard.Against.NotNull(input, parameterName);
            if (input?.Length > 0)
                throw new ArgumentException($"The required input {parameterName} is not empty.", parameterName);

            return guard;
        }

        public static IGuard NullOrEmpty([NotNull, JetBrainsNotNull] this IGuard guard, [NotNull, JetBrainsNotNull] Guid? input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            Guard.Against.Null(input, parameterName);
            if (input == Guid.Empty)
                throw new ArgumentException($"The required input {parameterName} is empty.", parameterName);

            return guard;
        }

        public static IGuard NotNullOrEmpty([NotNull, JetBrainsNotNull] this IGuard guard, [AllowNull] Guid? input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            Guard.Against.NotNull(input, parameterName);
            if (input != Guid.Empty)
                throw new ArgumentException($"Required input {parameterName} is not empty.", parameterName);

            return guard;
        }

        public static IGuard NullOrEmpty<T>([NotNull, JetBrainsNotNull] this IGuard guard, [NotNull, JetBrainsNotNull] IEnumerable<T>? input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            Guard.Against.Null(input, parameterName);
            if (!input.Any())
                throw new ArgumentException($"The required input {parameterName} is empty.", parameterName);

            return guard;
        }

        public static IGuard NotNullOrEmpty<T>([NotNull, JetBrainsNotNull] this IGuard guard, [AllowNull] IEnumerable<T>? input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            Guard.Against.NotNull(input, parameterName);
            if (input.Any())
                throw new ArgumentException($"The required input {parameterName} is not empty.", parameterName);

            return guard;
        }

        public static IGuard NullOrWhiteSpace([NotNull, JetBrainsNotNull] this IGuard guard, [NotNull, JetBrainsNotNull] string? input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            Guard.Against.NullOrEmpty(input, parameterName);
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException($"The required input {parameterName} is empty.", parameterName);

            return guard;
        }

        public static IGuard NotNullOrWhiteSpace([NotNull, JetBrainsNotNull] this IGuard guard, [AllowNull] string? input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            Guard.Against.NotNullOrEmpty(input, parameterName);
            if (!string.IsNullOrWhiteSpace(input))
                throw new ArgumentException($"The required input {parameterName} is not empty.", parameterName);

            return guard;
        }

    }
}
