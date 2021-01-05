namespace Cult.Specification
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        readonly ISpecification<T> left;
        readonly ISpecification<T> right;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.left = left;
            this.right = right;
        }

        public override bool IsSatisfiedBy(T candidate) => left.IsSatisfiedBy(candidate) || right.IsSatisfiedBy(candidate);
    }
}
