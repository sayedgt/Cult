using System;
using System.Linq.Expressions;
// ReSharper disable UnusedMember.Global

namespace Cult.Extensions
{
    public static class ExpressionExtensions
    {
        public static string ToReadableString(this Expression expression, bool trimLongArgumentList = false)
        {
            return ExpressionStringBuilder.ToString(expression, trimLongArgumentList);
        }
        public static string GetMemberName<TSource, TProperty>(this Expression<Func<TSource, TProperty>> property)
        {
            if (Equals(property, null))
            {
                throw new NullReferenceException("Property is required");
            }

            MemberExpression expr;

            if (property.Body is MemberExpression)
            {
                expr = (MemberExpression)property.Body;
            }
            else if (property.Body is UnaryExpression)
            {
                expr = (MemberExpression)((UnaryExpression)property.Body).Operand;
            }
            else
            {
                const string format = "Expression '{0}' not supported.";
                string message = string.Format(format, property);

                throw new ArgumentException(message, "Property");
            }

            return expr.Member.Name;
        }
    }
}
