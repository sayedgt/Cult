using System;
// ReSharper disable All

namespace Cult.Extensions.Guard
{
    public static class GuardExtensions
    {
        private static void Self(this IGuard guard, string parameterName)
        {
            if (guard == null) throw new ArgumentNullException(nameof(guard));
            if (parameterName == null) throw new ArgumentNullException(nameof(parameterName));
        }

        public static IGuard Null<T>(this IGuard guard, T input, string parameterName)
        {
            Self(guard, parameterName);

            if (input == null)
                throw new ArgumentNullException(parameterName);

            return guard;
        }
    }
}