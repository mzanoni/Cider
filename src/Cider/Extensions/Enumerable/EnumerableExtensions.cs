using System;
using System.Collections.Generic;
using System.Linq;

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
        foreach (T item in source)
        {
            action(item);
        }
    }

    public static IEnumerable<T> If<T>(this IEnumerable<T> source, bool condition, Func<IEnumerable<T>, IEnumerable<T>> transform)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (transform == null) throw new ArgumentNullException(nameof(transform));

        return condition ? transform(source) : source;
    }

    public static TU[] SelectToArray<T, TU>(this IEnumerable<T> source, Func<T, TU> selector)
    {
        return source
            .Select(selector)
            .ToArray();
    }
}