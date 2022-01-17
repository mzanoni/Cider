using System;

namespace Cider.Patterns;

public class PredicateSpecification<T> : Specification<T>
{
    private readonly Predicate<T> _predicate;

    protected PredicateSpecification(Predicate<T> predicate)
    {
        _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
    }

    public override bool IsSatisfiedBy(T item)
    {
        return _predicate(item);
    }
}