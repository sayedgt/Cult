namespace Cult.Specification
{

    public class NotSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> _other;
        public NotSpecification(ISpecification<T> other) => _other = other;
        public override bool IsSatisfiedBy(T candidate) => !_other.IsSatisfiedBy(candidate);
    }
}
