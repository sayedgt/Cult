// ReSharper disable All

using System;

namespace Cult.Functional
{
    public static class MonadExtensions
    {
        public static bool HasValue<T>(this Maybe<T> maybe) => maybe != Maybe<T>.None;

        public static Maybe<C> SelectMany<A, B, C>(this Maybe<A> ma, Func<A, Maybe<B>> f, Func<A, B, C> select) => ma.Bind(a => f(a).Map(b => select(a, b)));

    }
}