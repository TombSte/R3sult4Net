namespace R3sult4Net;

/// <summary>
/// Represents a value that may or may not be present.
/// This is similar to the Maybe type in functional programming, allowing for the representation of optional values
/// </summary>
/// <typeparam name="T"></typeparam>
public readonly struct Maybe<T>
{
    /// <summary>
    /// Indicates whether a value is present.
    /// </summary>
    public bool HasValue { get; }
    /// <summary>
    /// The value if present; otherwise, it is null.
    /// </summary>
    public T? Value { get; }
    private Maybe(T value) { HasValue = true; Value = value; }
    /// <summary>
    /// Creates a Maybe with a value.
    /// </summary>
    public static Maybe<T> None => default;
    /// <summary>
    /// Creates a Maybe with the specified value.
    /// </summary>
    public static Maybe<T> From(T? value) => value is null ? None : new(value);
}