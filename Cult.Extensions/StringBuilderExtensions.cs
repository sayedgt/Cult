using static System.String;
using System;
using System.Collections.Generic;
using System.Text;
// ReSharper disable All 
namespace Cult.Extensions
{
    public static class StringBuilderExtensions
    {
        public static void Append(this StringBuilder builder, string value, params object[] parameters)
        {
            builder.Append(Format(value, parameters));
        }
        public static StringBuilder AppendFormat(this StringBuilder @this, string format, params object[] args)
        {
            @this.Append(Format(format, args));

            return @this;
        }
        public static StringBuilder AppendFormat(this StringBuilder @this, string format, List<IEnumerable<object>> args)
        {
            @this.Append(Format(format, args));

            return @this;
        }
        public static StringBuilder AppendIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (var value in values)
            {
                if (predicate(value))
                {
                    @this.Append(value);
                }
            }

            return @this;
        }
        public static StringBuilder AppendJoin<T>(this StringBuilder @this, string separator, IEnumerable<T> values)
        {
            @this.Append(Join(separator, values));

            return @this;
        }
        public static StringBuilder AppendJoin<T>(this StringBuilder @this, string separator, params T[] values)
        {
            @this.Append(Join(separator, values));

            return @this;
        }
        public static void AppendLine(this StringBuilder builder, string value, params object[] parameters)
        {
            builder.AppendLine(Format(value, parameters));
        }
        public static StringBuilder AppendLineFormat(this StringBuilder @this, string format, params object[] args)
        {
            @this.AppendLine(Format(format, args));

            return @this;
        }
        public static StringBuilder AppendLineFormat(this StringBuilder @this, string format, List<IEnumerable<object>> args)
        {
            @this.AppendLine(Format(format, args));

            return @this;
        }
        public static StringBuilder AppendLineIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (var value in values)
            {
                if (predicate(value))
                {
                    @this.AppendLine(value.ToString());
                }
            }

            return @this;
        }
        public static StringBuilder AppendLineJoin<T>(this StringBuilder @this, string separator, IEnumerable<T> values)
        {
            @this.AppendLine(Join(separator, values));

            return @this;
        }
        public static StringBuilder AppendLineJoin(this StringBuilder @this, string separator, params object[] values)
        {
            @this.AppendLine(Join(separator, values));

            return @this;
        }
        public static string Substring(this StringBuilder @this, int startIndex)
        {
            return @this.ToString(startIndex, @this.Length - startIndex);
        }
        public static string Substring(this StringBuilder @this, int startIndex, int length)
        {
            return @this.ToString(startIndex, length);
        }
    }
}
