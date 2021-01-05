using System;
using System.Linq.Expressions;
using System.Reflection;
// ReSharper disable All 
namespace Cult.Utilities
{
    public static class ExpressionUtility<T>
    {
        /*
            var getterNameProperty = ExpressionUtils.CreateGetter<Employee, string>("Name");
            var getterBirthDateProperty = ExpressionUtils.CreateGetter<Employee, DateTime>("BirthDate");
            var name = getterNameProperty(emp1);
            var birthDate = getterBirthDateProperty(emp1);
            
            var setterNameProperty = ExpressionUtils.CreateSetter<Employee, string>("Name");
            var setterBirthDateProperty = ExpressionUtils.CreateSetter<Employee, DateTime>("BirthDate");
            setterNameProperty(emp, "John");
            setterBirthDateProperty(emp, new DateTime(1990, 6, 5));
        */
        public static Func<TEntity, TProperty> CreateGetter<TEntity, TProperty>(string name) where TEntity : class
        {
            ParameterExpression instance = Expression.Parameter(typeof(TEntity), "instance");

            var body = Expression.Property(instance, name);

            return Expression.Lambda<Func<TEntity, TProperty>>(body, instance).Compile();
        }
        public static Action<TEntity, TProperty> CreateSetter<TEntity, TProperty>(string name) where TEntity : class
        {
            ParameterExpression instance = Expression.Parameter(typeof(TEntity), "instance");
            ParameterExpression propertyValue = Expression.Parameter(typeof(TProperty), "propertyValue");

            var body = Expression.Assign(Expression.Property(instance, name), propertyValue);

            return Expression.Lambda<Action<TEntity, TProperty>>(body, instance, propertyValue).Compile();
        }
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
