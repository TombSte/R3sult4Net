namespace R3sult4Net;

public static class Extensions
{
    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> map) 
        where TIn : class 
        where TOut : class
    {
        if (result.IsFailure) return result.Error!;
        return map(result.Value);
    }
}