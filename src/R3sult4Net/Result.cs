namespace R3sult4Net;

/// <summary>
/// Represents a result of an operation, which can either be successful or contain an error.
/// </summary>
public readonly struct Result
{
    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }
    /// <summary>
    /// Indicates whether the operation failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;
    /// <summary>
    /// Contains the error details if the operation failed; otherwise, it is an empty error.
    /// </summary>
    public Error Error { get; }

    private Result(bool isSuccess, Error error) { IsSuccess = isSuccess; Error = error; }

    /// <summary>
    /// Creates a successful result with no error.
    /// </summary>
    /// <returns></returns>
    public static Result Success() => new(true, Error.None);
    /// <summary>
    /// Creates a failed result with the specified error.
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Result Failure(Error error) => new(false, error ?? throw new ArgumentNullException(nameof(error)));
    /// <summary>
    /// Implicitly converts an error to a Result, treating it as a failure.
    /// </summary>
    /// <param name="error"></param>
    public static implicit operator Result(Error error) => Failure(error);
}

/// <summary>
/// Represents a result of an operation that returns a value of type T, which can either be successful or contain an error.
/// </summary>
/// <typeparam name="T"></typeparam>
public readonly struct Result<T>
{
    /// <summary>
    /// Indicates whether the operation was successful and a value is present.
    /// </summary>
    public bool IsSuccess { get; }
    /// <summary>
    /// Indicates whether the operation failed and no value is present.
    /// </summary>
    public bool IsFailure => !IsSuccess;
    /// <summary>
    /// Contains the value if the operation was successful; otherwise, it is null.
    /// </summary>
    public T? Value { get; }
    /// <summary>
    /// Contains the error details if the operation failed; otherwise, it is an empty error.
    /// </summary>
    public Error Error { get; }

    private Result(T value) { IsSuccess = true; Value = value; Error = Error.None; }
    private Result(Error error) { IsSuccess = false; Value = default; Error = error ?? throw new ArgumentNullException(nameof(error)); }

    /// <summary>
    /// Creates a successful result with the specified value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Result<T> Success(T value) => new(value);
    /// <summary>
    /// Creates a failed result with the specified error.
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Result<T> Failure(Error error) => new(error);

    /// <summary>
    /// Deconstructs the Result into its components.
    /// </summary>
    /// <param name="ok"></param>
    /// <param name="value"></param>
    /// <param name="error"></param>
    public void Deconstruct(out bool ok, out T? value, out Error error) { ok = IsSuccess; value = Value; error = Error; }

    /// <summary>
    /// Implicitly converts a value of type T to a Result, treating it as a success.
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator Result<T>(T value) => Success(value);
    /// <summary>
    /// Implicitly converts an error to a Result, treating it as a failure.
    /// </summary>
    /// <param name="error"></param>
    public static implicit operator Result<T>(Error error) => Failure(error);
}