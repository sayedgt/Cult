using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cult.Toolkit.EasyPredicateBuilder
{
    public class EasyPredicate<TExpressionFuncType>
    {
        public class ParameterRebinder : ExpressionVisitor
        {
            readonly Dictionary<ParameterExpression, ParameterExpression> map;

            ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;

                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }

                return base.VisitParameter(p);
            }
        }

        private Expression<Func<TExpressionFuncType, bool>> internalExpression;
        /// <summary>
        /// Creates a predicate that evaluates to true.
        /// </summary>
        public static EasyPredicate<TExpressionFuncType> True = EasyPredicate<TExpressionFuncType>.Create(param => true);
        /// <summary>
        /// Creates a predicate that evaluates to false.
        /// </summary>
        public static EasyPredicate<TExpressionFuncType> False = EasyPredicate<TExpressionFuncType>.Create(param => false);

        public EasyPredicate(Expression<Func<TExpressionFuncType, bool>> expression)
        {
            internalExpression = expression;
        }

        /// <summary>
        /// Combines the first predicate with the second using the logical "and".
        /// </summary>
        public EasyPredicate<TExpressionFuncType> And(Expression<Func<TExpressionFuncType, bool>> expression)
        {
            return Create(Compose(expression, Expression.AndAlso));
        }

        public EasyPredicate<TExpressionFuncType> And(EasyPredicate<TExpressionFuncType> predicate)
        {
            return And(predicate.internalExpression);
        }

        /// <summary>
        /// Combines the first expression with the second using the specified merge function.
        /// </summary>
        Expression<T> Compose<T>(Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // zip parameters (map from parameters of second to parameters of first)
            var map = internalExpression.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with the parameters in the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // create a merged lambda expression with parameters from the first expression
            return Expression.Lambda<T>(merge(internalExpression.Body, secondBody), internalExpression.Parameters);
        }

        /// <summary>
        /// Creates a predicate expression from the specified lambda expression.
        /// </summary>
        public static EasyPredicate<TExpressionFuncType> Create(Expression<Func<TExpressionFuncType, bool>> expression) { return new EasyPredicate<TExpressionFuncType>(expression); }

        public static implicit operator EasyPredicate<TExpressionFuncType>(Expression<Func<TExpressionFuncType, bool>> expression)
        {
            return EasyPredicate<TExpressionFuncType>.Create(expression);
        }

        public static implicit operator Expression<Func<TExpressionFuncType, bool>>(EasyPredicate<TExpressionFuncType> expression)
        {
            return expression.internalExpression;
        }

        /// <summary>
        /// Negates the predicate.
        /// </summary>
        public EasyPredicate<TExpressionFuncType> Not()
        {
            var negated = Expression.Not(internalExpression.Body);
            return Create(Expression.Lambda<Func<TExpressionFuncType, bool>>(negated, internalExpression.Parameters));
        }

        public static EasyPredicate<TExpressionFuncType> operator !(EasyPredicate<TExpressionFuncType> predicate)
        {
            return predicate.Not();
        }

        public static EasyPredicate<TExpressionFuncType> operator &(EasyPredicate<TExpressionFuncType> first, EasyPredicate<TExpressionFuncType> second)
        {
            return first.And(second);
        }

        public static EasyPredicate<TExpressionFuncType> operator |(EasyPredicate<TExpressionFuncType> first, EasyPredicate<TExpressionFuncType> second)
        {
            return first.Or(second);
        }

        public static bool operator false(EasyPredicate<TExpressionFuncType> first) { return false; }

        public static bool operator true(EasyPredicate<TExpressionFuncType> first) { return false; }

        /// <summary>
        /// Combines the first predicate with the second using the logical "or".
        /// </summary>
        public EasyPredicate<TExpressionFuncType> Or(Expression<Func<TExpressionFuncType, bool>> expression)
        {
            return Create(Compose(expression, Expression.OrElse));
        }

        public EasyPredicate<TExpressionFuncType> Or(EasyPredicate<TExpressionFuncType> predicate)
        {
            return Or(predicate.internalExpression);
        }

        public Expression<Func<TExpressionFuncType, bool>> ToExpression()
        {
            return internalExpression;
        }

        public Func<TExpressionFuncType, bool> ToFunc()
        {
            return internalExpression.Compile();
        }
    }

    /// <summary>
    /// Enables the efficient, dynamic composition of query predicates.
    /// </summary>
    public static class EasyPredicate
    {
        /// <summary>
        /// Combines the first predicate with the second using the logical "and".
        /// </summary>
        public static EasyPredicate<T> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.ToEasyPredicate().And(second);
        }

        /// <summary>
        /// Negates the predicate.
        /// </summary>
        public static EasyPredicate<T> Not<T>(this Expression<Func<T, bool>> expression)
        {
            return expression.ToEasyPredicate().Not();
        }

        /// <summary>
        /// Combines the first predicate with the second using the logical "or".
        /// </summary>
        public static EasyPredicate<T> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.ToEasyPredicate().Or(second);
        }

        public static EasyPredicate<T> ToEasyPredicate<T>(this Expression<Func<T, bool>> expression)
        {
            return EasyPredicate<T>.Create(expression);
        }
    }
}
