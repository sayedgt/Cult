using System;
using System.Collections.Generic;
using System.Linq;

namespace Cult.Extensions.ExtraException
{
    public static class ExceptionExtensions
    {
        public static string GetFullMessage(this Exception exception)
        {
            return exception.InnerException == null
                 ? exception.Message
                 : exception.Message + " --> " + exception.InnerException.GetFullMessage();
        }

        public static string ToFormattedString(this Exception exception)
        {
            IEnumerable<string> messages = exception
                .GetAllExceptions()
                .Where(e => !string.IsNullOrWhiteSpace(e.Message))
                .Select(e => e.Message.Trim());
            return string.Join(Environment.NewLine, messages);
        }

        private static IEnumerable<Exception> GetAllExceptions(this Exception exception)
        {
            yield return exception;

            if (exception is AggregateException aggrEx)
            {
                foreach (Exception innerEx in aggrEx.InnerExceptions.SelectMany(e => e.GetAllExceptions()))
                {
                    yield return innerEx;
                }
            }
            else if (exception.InnerException != null)
            {
                foreach (Exception innerEx in exception.InnerException.GetAllExceptions())
                {
                    yield return innerEx;
                }
            }
        }

        public static bool ThrowIfTrue(this bool value)
        {
            if (value)
                throw new ArgumentNullException();

            return value;
        }

        public static object ThrowIfFalse(this bool value)
        {
            return (!value).ThrowIfTrue();
        }

        public static object ThrowIfNull(this object o)
        {
            (o is null).ThrowIfTrue();

            return o;
        }

        public static object ThrowIfNotNull(this object o)
        {
            (o is null).ThrowIfFalse();

            return o;
        }
    }
}
