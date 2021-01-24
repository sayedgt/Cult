using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    public static partial class GuardExtensions
    {
        public static IGuard Default<T>([NotNull, JetBrainsNotNull] this IGuard guard, [AllowNull, NotNull, JetBrainsNotNull] T input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            if (input is null || EqualityComparer<T>.Default.Equals(input, default!))
            {
                throw new ArgumentException($"Parameter [{parameterName}] is default value for type {typeof(T).Name}",
                    parameterName);
            }

            return guard;
        }
        public static IGuard NotDefault<T>([NotNull, JetBrainsNotNull] this IGuard guard, [AllowNull] T input, [NotNull, JetBrainsNotNull] string parameterName)
        {
            if (!(input is null || EqualityComparer<T>.Default.Equals(input, default!)))
            {
                throw new ArgumentException($"Parameter [{parameterName}] is not default value for type {typeof(T).Name}",
                parameterName);
            }

            return guard;
        }
    }
}
