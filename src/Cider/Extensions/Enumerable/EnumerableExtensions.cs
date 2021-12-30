namespace Cider.Extensions.Enumerable;

public static class EnumerableExtensions
{
    public static IEnumerable<T> SkipNulls<T>(this IEnumerable<T?> source)
    {
        return source.Where(x => x is not null)!;
    }

    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source)
    {
        return source ?? Array.Empty<T>();
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (T item in source.EmptyIfNull())
        {
            action(item);
        }
    }
}