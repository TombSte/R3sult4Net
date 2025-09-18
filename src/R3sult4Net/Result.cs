namespace R3sult4Net;

/// <summary>
/// 
/// </summary>
public class Result : BaseResult
{
    protected Result(bool isSuccess, Error? error = null) : base(isSuccess, error) {}
    public static Result Success() => new (true);
    
    public static Result Failure(Error error) => new (false, error);
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
    
    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
}