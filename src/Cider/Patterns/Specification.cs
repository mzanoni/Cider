namespace Cider.Patterns;

public abstract class Specification<T> : ISpecification<T>
{
    public abstract bool IsSatisfiedBy(T item);

    public ISpecification<T> And(ISpecification<T> other)
    {
        return new AndSpecification(this, other);
    }

    public ISpecification<T> Or(ISpecification<T> other)
    {
        return new OrSpecification(this, other);
    }

    private class AndSpecification : Specification<T>
    {
        private readonly ISpecification<T> _first;
        private readonly ISpecification<T> _second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            _first = first;
            _second = second;
        }

        public override bool IsSatisfiedBy(T item)
        {
            return _first.IsSatisfiedBy(item) && _second.IsSatisfiedBy(item);
        }
    }

    private class OrSpecification : Specification<T>
    {
        private readonly ISpecification<T> _first;
        private readonly ISpecification<T> _second;

        public OrSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            _first = first;
            _second = second;
        }

        public override bool IsSatisfiedBy(T item)
        {
            return _first.IsSatisfiedBy(item) || _second.IsSatisfiedBy(item);
        }
    }
}