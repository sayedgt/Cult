using System;

namespace Cult.Extensions.Guard
{
    public static class GuardExtensions
    {
        private static void Self(this IGuard guard, string parameterName)
        {
            if (guard is null)
                throw new ArgumentNullException(nameof(guard));

            if (parameterName is null)
                throw new ArgumentNullException(nameof(parameterName));
        }

        public static T Null<T>(this IGuard guard, T input, string parameterName)
        {
            Self(guard, parameterName);

            if (input == null)
                throw new ArgumentNullException(parameterName);

            return input;
        }
    }
}