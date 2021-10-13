using System.Collections.Generic;

namespace Cult.Toolkit.ExtraEnumerator
{
    public static class EnumeratorExtensions
    {
        public static IAsyncEnumerator<T> GetAsyncEnumerator<T>(this IAsyncEnumerator<T> enumerator) => enumerator;

        public static IEnumerator<T> GetEnumerator<T>(this IEnumerator<T> enumerator) => enumerator;
    }
}
