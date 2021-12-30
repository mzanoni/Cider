using System.Reflection;

namespace Cider;

public interface IEnumerated
{
    public string Name { get; }
}

public abstract class Enumerated<TEnum> : IEnumerated where TEnum : IEnumerated
{
    private static readonly Lazy<TEnum[]> _values = new(GetAllValues, LazyThreadSafetyMode.ExecutionAndPublication);
    private static readonly Lazy<Dictionary<string, TEnum>> _valuesByName = new(() => _values.Value.ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase));

    protected Enumerated(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

        Name = name;
    }

    public string Name { get; }
    public static TEnum[] Values => _values.Value;

    public static TEnum Parse(string enumName)
    {
        if (!_valuesByName.Value.ContainsKey(enumName))
            throw new KeyNotFoundException($"Could not find value with key '{enumName}'");

        return _valuesByName.Value[enumName];
    }

    public static bool TryParse(string enumName, out TEnum? value)
    {
        return _valuesByName.Value.TryGetValue(enumName, out value);
    }

    public static bool operator !=(Enumerated<TEnum> x, Enumerated<TEnum> y)
    {
        return !Equals(x, y);
    }

    public static bool operator ==(Enumerated<TEnum>? x, Enumerated<TEnum>? y)
    {
        return Equals(x, y);
    }

    protected bool Equals(Enumerated<TEnum>? classEnumBase)
    {
        if (classEnumBase == null) return false;
        return GetType() == classEnumBase.GetType() && Equals(Name, classEnumBase.Name);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        return Equals(obj as Enumerated<TEnum>);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    private static TEnum[] GetAllValues()
    {
        Type baseType = typeof(TEnum);
        return Assembly.GetAssembly(baseType)!
            .GetTypes()
            .Where(t => baseType.IsAssignableFrom(t))
            .SelectMany(GetFieldsOfType<TEnum>)
            .OrderBy(t => t.Name)
            .ToArray();
    }

    private static List<TFieldType> GetFieldsOfType<TFieldType>(Type type)
    {
        return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(p => type.IsAssignableFrom(p.FieldType))
            .Select(pi => (TFieldType?)pi.GetValue(null))
            .Where(x => x is not null)
            .ToList()!;
    }
}