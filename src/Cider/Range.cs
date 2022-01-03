namespace Cider;

public class Range<T> : IEquatable<Range<T>> where T : IComparable<T>
{
    public Range(T lowerBound, T upperBound)
    {
        LowerBound = lowerBound;
        UpperBound = upperBound;
    }

    public T LowerBound { get; }
    public T UpperBound { get; }

    public bool Equals(Range<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return EqualityComparer<T>.Default.Equals(LowerBound, other.LowerBound) && 
               EqualityComparer<T>.Default.Equals(UpperBound, other.UpperBound);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        return Equals((Range<T>)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(LowerBound, UpperBound);
    }

    public static Range<T> Empty() => new(default!, default!);
}