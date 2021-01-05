using System;
using System.Linq.Expressions;
// ReSharper disable All

namespace Cult.Specification
{

    public abstract class LinqSpecification<T> : CompositeSpecification<T>
    {
        public abstract Expression<Func<T, bool>> AsExpression();
        public override bool IsSatisfiedBy(T candidate) => AsExpression().Compile()(candidate);
    }
}
