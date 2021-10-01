using System.Collections.Generic;
// ReSharper disable All
#if NETSTANDARD2_1
namespace Cult.Toolkit.ExtraEnumerator
{
    public static class EnumeratorExtensions
    {
        public static IEnumerator<T> GetEnumerator<T>(this IEnumerator<T> enumerator) => enumerator;
        public static IAsyncEnumerator<T> GetAsyncEnumerator<T>(this IAsyncEnumerator<T> enumerator) => enumerator;
    }
}
#endif