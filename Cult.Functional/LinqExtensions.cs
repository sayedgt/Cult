using Cult.Functional;

// ReSharper disable All

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static Maybe<C> SelectMany<A, B, C>(this Maybe<A> ma, Func<A, Maybe<B>> f, Func<A, B, C> select) => ma.Bind(a => f(a).Map(b => select(a, b)));
    }
}
