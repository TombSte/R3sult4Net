namespace R3sult4Net;

public abstract class BaseResult
{
    public bool IsSuccess { get; protected set; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; protected set; }

    protected BaseResult(bool isSuccess, Error? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
}