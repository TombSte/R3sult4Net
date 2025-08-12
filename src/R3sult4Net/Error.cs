namespace R3sult4Net;


/// <summary>
/// Represents an error in the R3sult4Net library.
/// </summary>
/// <param name="Code"></param>
/// <param name="Message"></param>
/// <param name="Type"></param>
/// <param name="Meta"></param>
public sealed record Error(
    string Code,
    string Message,
    ErrorType Type = ErrorType.Failure,
    IReadOnlyDictionary<string, object?>? Meta = null)
{
    /// <summary>
    /// Represents a generic error with no specific details.
    /// </summary>
    public static Error None => new("None", "No error", ErrorType.Failure);
    /// <summary>
    /// Represents a not found error, typically when a requested resource does not exist.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"{Code} ({Type}): {Message}";
}