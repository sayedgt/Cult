using System;
// ReSharper disable All 
namespace Cult.Toolkit.Piping
{
    public static class PipeExtensions
    {
        /// <summary>
        /// Take an object, pipe it into a function, and return the result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="obj"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static U Pipe<T, U>(this T obj, Func<T, U> f)
        {
            return f(obj);
        }

        /// <summary>
        ///  Pipes object it into a function, and returns object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="f"></param>
        /// <remarks>useful for encapsulating procedural void method calls</remarks>
        public static T Pipe<T>(this T obj, Action<T> f)
        {
            f(obj);
            return obj;
        }

        /// <summary>
        /// Pipes object into a void function (action)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="f"></param>
        /// <remarks>for cases where the output is to be ignored</remarks>
        public static void PipeVoid<T>(this T obj, Action<T> f)
        {
            f(obj);
        }
    }
}
