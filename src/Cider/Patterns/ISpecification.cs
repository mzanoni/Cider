namespace Cider.Patterns;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T item);
    ISpecification<T> And(ISpecification<T> other);
    ISpecification<T> Or(ISpecification<T> other);
}