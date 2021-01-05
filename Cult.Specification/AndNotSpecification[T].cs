namespace Cult.Specification
{

    public class AndNotSpecification<T> : CompositeSpecification<T>
    {
        readonly ISpecification<T> left;
        readonly ISpecification<T> right;

        public AndNotSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.left = left;
            this.right = right;
        }

        public override bool IsSatisfiedBy(T candidate) => left.IsSatisfiedBy(candidate) && !right.IsSatisfiedBy(candidate);
    }
}
