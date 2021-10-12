using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
// ReSharper disable All 
namespace Cult.Toolkit.ExtraTask
{
    public static class TaskExtensions
    {
        public static async void SafeFireAndForget(this Task @this, bool continueOnCapturedContext = true, Action<Exception> onException = null)
        {
            try
            {
                await @this.ConfigureAwait(continueOnCapturedContext);
            }
            catch (Exception e) when (onException != null)
            {
                onException(e);
            }
        }

        public static Task<V> GroupJoin<T, U, K, V>(
                            this Task<T> source, Task<U> inner,
                            Func<T, K> outerKeySelector, Func<U, K> innerKeySelector,
                            Func<T, Task<U>, V> resultSelector)
        {
            return source.TaskBind(t =>
            {
                return resultSelector(
                    t,
                    inner.Where(u =>
                        EqualityComparer<K>.Default.Equals(
                            outerKeySelector(t),
                            innerKeySelector(u)
                            )
                        )
                    ).TaskUnit();
            }
                );
        }

        public static Task<V> Join<T, U, K, V>(
                            this Task<T> source, Task<U> inner,
                            Func<T, K> outerKeySelector, Func<U, K> innerKeySelector,
                            Func<T, U, V> resultSelector)
        {
            Task.WaitAll(source, inner);

            return source.TaskBind(t =>
            {
                return inner.TaskBind(u =>
                {
                    if (!EqualityComparer<K>.Default.Equals(outerKeySelector(t), innerKeySelector(u)))
                        throw new OperationCanceledException();

                    return resultSelector(t, u).TaskUnit();
                }
                    );
            }
                );
        }

        public static Task<U> Select<T, U>(
                            this Task<T> source, Func<T, U> selector)
        {
            return source.TaskBind(t => selector(t).TaskUnit());
        }

        public static Task<C> SelectMany<A, B, C>(
                            this Task<A> monad,
                            Func<A, Task<B>> function,
                            Func<A, B, C> projection)
        {

            return monad.TaskBind(
                outer => function(outer).TaskBind(
                    inner => projection(outer, inner).TaskUnit()));
        }

        public static Task<V> TaskBind<U, V>(
                            this Task<U> m, Func<U, Task<V>> k)
        {
            return m.ContinueWith(m_ => k(m_.Result)).Unwrap();
        }

        public static Task<T> TaskUnit<T>(this T value)
        {
            return Task.Factory.StartNew(() => value);
        }

        public static bool WaitCancellationRequested(
                            this CancellationToken token,
                            TimeSpan timeout)
        {
            return token.WaitHandle.WaitOne(timeout);
        }

        public static Task<T> Where<T>(
                            this Task<T> source, Func<T, bool> predicate)
        {
            return source.TaskBind(t =>
            {
                if (!predicate(t))
                    throw new OperationCanceledException();

                return t.TaskUnit();
            }
                );
        }
    }
}
