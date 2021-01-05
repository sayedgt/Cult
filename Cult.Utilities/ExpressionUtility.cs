using System;
using System.Linq.Expressions;
using System.Reflection;
// ReSharper disable All 
namespace Cult.Utilities
{
    public static class ExpressionUtility<T>
    {
        public static TAttribute GetCustomAttribute<TAttribute>(
                                    Expression<Func<T, TAttribute>> selector) where TAttribute : Attribute
        {
            Expression body = selector;
            if (body is LambdaExpression)
            {
                body = ((LambdaExpression)body).Body;
            }
            switch (body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return ((PropertyInfo)((MemberExpression)body).Member).GetCustomAttribute<TAttribute>();

                default:
                    throw new InvalidOperationException();
            }
        }
        public static Func<TObject, TProperty> GetProperty<TObject, TProperty>(string propertyName)
        {
            ParameterExpression paramExpression = Expression.Parameter(typeof(TObject), "value");

            Expression propertyGetterExpression = Expression.Property(paramExpression, propertyName);

            return Expression.Lambda<Func<TObject, TProperty>>(propertyGetterExpression, paramExpression).Compile();
        }
        public static PropertyInfo GetProperty(Expression<Func<T, object>> property)
        {
            LambdaExpression lambda = property;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression expression)
            {
                memberExpression = (MemberExpression)(expression.Operand);
            }
            else
            {
                memberExpression = (MemberExpression)(lambda.Body);
            }

            return (PropertyInfo)memberExpression.Member;
        }
        public static string GetPropertyName(Expression<Func<T, object>> property)
        {
            return GetProperty(property).Name;
        }
        public static Action<TObject, TProperty> SetProperty<TObject, TProperty>(string propertyName)
        {
            ParameterExpression paramExpression = Expression.Parameter(typeof(TObject));

            ParameterExpression paramExpression2 = Expression.Parameter(typeof(TProperty), propertyName);

            MemberExpression propertyGetterExpression = Expression.Property(paramExpression, propertyName);

            return Expression.Lambda<Action<TObject, TProperty>>
                (Expression.Assign(propertyGetterExpression, paramExpression2), paramExpression, paramExpression2).Compile();
        }
    }
}
