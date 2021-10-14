using System.Collections.Generic;

namespace Cult.Toolkit.ExtraEnumerator
{
    public static class EnumeratorExtensions
    {
        public static IAsyncEnumerator<T> GetAsyncEnumerator<T>(this IAsyncEnumerator<T> enumerator) => enumerator;

        public static IEnumerator<T> GetEnumerator<T>(this IEnumerator<T> enumerator) => enumerator;

        public static IEnumerator<int> GetEnumerator(this int input)
        {
            if (input >= 0)
            {
                for (int i = 0; i < input; i++)
                {
                    yield return i;
                }
            }
            else
            {
                for (int i = input - 1; i >= 0; i--)
                {
                    yield return i;
                }
            }
        }
    }
}
