namespace R3sult4Net;

/// <summary>
/// 
/// </summary>
public class Result : BaseResult
{
    protected Result(bool isSuccess, Error? error = null) : base(isSuccess, error) {}
    public static Result Success() => new (true);
    
    public static Result Failure(Error error) => new (false, error);

    public static Result<T> Success<T>(T value) where T : class => Result<T>.Success(value);
    public static Result<T> Failure<T>(Error error) where T : class => Result<T>.Failure(error);

    public static implicit operator Result(Error error) => Failure(error);
}

public class Result<T> : BaseResult where T : class
{
    public T Value { get; private set; }
    protected Result(bool isSuccess, T value, Error? error = null) : base(isSuccess, error)
    {
        this.Value = value;
    }

    public static Result<T> Success(T value) => new (true, value);
    public static Result<T> Failure(Error error) => new (false, null!, error);
    public static Result<TResult> Map<TResult>(Result<T> result, Func<T, TResult> mapFunc) where TResult : class
    {
        if (result.IsSuccess)
            return Result<TResult>.Success(mapFunc(result.Value));
        else
            return Result<TResult>.Failure(result.Error!);
    }


    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
}