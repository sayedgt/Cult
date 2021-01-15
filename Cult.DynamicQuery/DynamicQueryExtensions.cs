using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Cult.DynamicQuery
{
    public static class DynamicQueryExtensions
    {
        public static IQueryable<T> DynamicWhere<T>(this IQueryable<T> queryable, IEnumerable<DynamicFilter> dynamicFilter)
        {
            return queryable.Where(Filter<T>(dynamicFilter));
        }
        public static IQueryable<T> DynamicWhere<T>(this IEnumerable<T> enumerable, IEnumerable<DynamicFilter> dynamicFilter)
        {
            return enumerable.AsQueryable().DynamicWhere(dynamicFilter);
        }
        public static IQueryable<T> DynamicWhere<T>(this ICollection<T> collection, IEnumerable<DynamicFilter> dynamicFilter)
        {
            return collection.AsQueryable().DynamicWhere(dynamicFilter);
        }
        public static IQueryable<T> DynamicWhere<T>(this T[] array, IEnumerable<DynamicFilter> dynamicFilter)
        {
            return array.AsQueryable().DynamicWhere(dynamicFilter);
        }
        private static Expression<Func<T, bool>> Filter<T>(IEnumerable<DynamicFilter> dynamicFilter)
        {
            Expression<Func<T, bool>> result = _ => true;
            foreach (var item in dynamicFilter)
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof(T));
                MemberExpression memberExpression = Expression.Property(parameterExpression, item.PropertyName);
                ConstantExpression constantExpression = Expression.Constant(item.PropertyValue);
                BinaryExpression comparison = GetBinaryExpression(item.ComparisonMethod, memberExpression, constantExpression);
                var expression = Expression.Lambda<Func<T, bool>>(comparison, parameterExpression);
                var param = Expression.Parameter(typeof(T), "x");
                var body = Expression.AndAlso(
                            Expression.Invoke(result, param),
                            Expression.Invoke(expression, param)
                        );
                result = Expression.Lambda<Func<T, bool>>(body, param);
            }
            return result;
        }
        private static BinaryExpression GetBinaryExpression(ComparisonMethod comparisonMethod, MemberExpression memberExpression, ConstantExpression constantExpression)
        {
            switch (comparisonMethod)
            {
                case ComparisonMethod.Equal:
                    return Expression.Equal(memberExpression, constantExpression);
                case ComparisonMethod.LessThan:
                    return Expression.LessThan(memberExpression, constantExpression);
                case ComparisonMethod.GreaterThan:
                    return Expression.GreaterThan(memberExpression, constantExpression);
                case ComparisonMethod.NotEqual:
                    return Expression.NotEqual(memberExpression, constantExpression);
                case ComparisonMethod.GreaterThanEqual:
                    return Expression.GreaterThanOrEqual(memberExpression, constantExpression);
                case ComparisonMethod.LessThanEqual:
                    return Expression.LessThanOrEqual(memberExpression, constantExpression);
                case ComparisonMethod.IsNullOrEmpty:
                    MethodInfo method = typeof(DynamicQueryExtensions).GetMethod(nameof(DynamicQueryIsNullOrEmpty), BindingFlags.NonPublic | BindingFlags.Static);
                    return Expression.MakeBinary(ExpressionType.Equal, memberExpression, constantExpression, false, method);
                case ComparisonMethod.IsNotNullOrEmpty:
                    MethodInfo method0 = typeof(DynamicQueryExtensions).GetMethod(nameof(DynamicQueryIsNotNullOrEmpty), BindingFlags.NonPublic | BindingFlags.Static);
                    return Expression.MakeBinary(ExpressionType.Equal, memberExpression, constantExpression, false, method0);
                case ComparisonMethod.Contains:
                    MethodInfo method1 = typeof(DynamicQueryExtensions).GetMethod(nameof(DynamicQueryContains), BindingFlags.NonPublic | BindingFlags.Static);
                    return Expression.MakeBinary(ExpressionType.Equal, memberExpression, constantExpression, false, method1);
                case ComparisonMethod.DoesNotContain:
                    MethodInfo method2 = typeof(DynamicQueryExtensions).GetMethod(nameof(DynamicQueryDoesNotContain), BindingFlags.NonPublic | BindingFlags.Static);
                    return Expression.MakeBinary(ExpressionType.Equal, memberExpression, constantExpression, false, method2);
                case ComparisonMethod.StartsWith:
                    MethodInfo method3 = typeof(DynamicQueryExtensions).GetMethod(nameof(DynamicQueryStartsWith), BindingFlags.NonPublic | BindingFlags.Static);
                    return Expression.MakeBinary(ExpressionType.Equal, memberExpression, constantExpression, false, method3);
                case ComparisonMethod.DoesNotStartWith:
                    MethodInfo method4 = typeof(DynamicQueryExtensions).GetMethod(nameof(DynamicQueryDoesNotStartWith), BindingFlags.NonPublic | BindingFlags.Static);
                    return Expression.MakeBinary(ExpressionType.Equal, memberExpression, constantExpression, false, method4);
                case ComparisonMethod.EndsWith:
                    MethodInfo method5 = typeof(DynamicQueryExtensions).GetMethod(nameof(DynamicQueryEndsWith), BindingFlags.NonPublic | BindingFlags.Static);
                    return Expression.MakeBinary(ExpressionType.Equal, memberExpression, constantExpression, false, method5);
                case ComparisonMethod.DoesNotEndWith:
                    MethodInfo method6 = typeof(DynamicQueryExtensions).GetMethod(nameof(DynamicQueryDoesNotEndWith), BindingFlags.NonPublic | BindingFlags.Static);
                    return Expression.MakeBinary(ExpressionType.Equal, memberExpression, constantExpression, false, method6);
                case ComparisonMethod.Like:
                    MethodInfo method7 = typeof(DynamicQueryExtensions).GetMethod(nameof(DynamicQueryLike), BindingFlags.NonPublic | BindingFlags.Static);
                    return Expression.MakeBinary(ExpressionType.Equal, memberExpression, constantExpression, false, method7);
                case ComparisonMethod.NotLike:
                    MethodInfo method8 = typeof(DynamicQueryExtensions).GetMethod(nameof(DynamicQueryNotLike), BindingFlags.NonPublic | BindingFlags.Static);
                    return Expression.MakeBinary(ExpressionType.Equal, memberExpression, constantExpression, false, method8);
                default:
                    return null;
            }
        }

        private static bool DynamicQueryContains(object source, object value)
        {
            if (source is string && value is string)
            {
                return source.ToString().Contains(value.ToString());
            }

            if (source is Array)
            {
                return Array.IndexOf((Array)source, value) >= 0;
            }

            if (source is IEnumerable)
            {
                return ((IEnumerable<object>)source).Contains(value);
            }
            return false;
        }
        private static bool DynamicQueryDoesNotContain(object source, object value)
        {
            if (source is string && value is string)
            {
                return !source.ToString().Contains(value.ToString());
            }

            if (source is Array)
            {
                return !(Array.IndexOf((Array)source, value) >= 0);
            }

            if (source is IEnumerable)
            {
                return !((IEnumerable<object>)source).Contains(value);
            }
            return false;
        }
        private static bool DynamicQueryStartsWith(object source, object value)
        {
            if (source is string && value is string)
            {
                return source.ToString().StartsWith(value.ToString());
            }
            return false;
        }
        private static bool DynamicQueryDoesNotStartWith(object source, object value)
        {
            if (source is string && value is string)
            {
                return !source.ToString().StartsWith(value.ToString());
            }
            return false;
        }
        private static bool DynamicQueryEndsWith(object source, object value)
        {
            if (source is string && value is string)
            {
                return source.ToString().EndsWith(value.ToString());
            }
            return false;
        }
        private static bool DynamicQueryDoesNotEndWith(object source, object value)
        {
            if (source is string && value is string)
            {
                return !source.ToString().EndsWith(value.ToString());
            }
            return false;
        }
        private static bool DynamicQueryIsNotNullOrEmpty(object source, object value)
        {
            if (source is string)
            {
                return !string.IsNullOrEmpty(source.ToString());
            }

            if (source is Array)
            {
                return source != null && ((Array)source).Length > 0;
            }

            if (source is IEnumerable)
            {
                return source != null && ((IEnumerable<object>)source).Count() > 0;
            }
            return false;
        }
        private static bool DynamicQueryIsNullOrEmpty(object source, object value)
        {
            if (source is string)
            {
                return string.IsNullOrEmpty(source.ToString());
            }

            if (source is Array)
            {
                return source == null || ((Array)source).Length == 0;
            }

            if (source is IEnumerable)
            {
                return source == null || ((IEnumerable<object>)source).Count() == 0;
            }
            return false;
        }
        private static bool DynamicQueryLike(object source, object value)
        {
            return
                DynamicQueryStartsWith(source, value) ||
                DynamicQueryContains(source, value) ||
                DynamicQueryEndsWith(source, value);
        }
        private static bool DynamicQueryNotLike(object source, object value)
        {
            return
                !DynamicQueryStartsWith(source, value) &&
                !DynamicQueryContains(source, value) &&
                !DynamicQueryEndsWith(source, value);
        }
    }
}
