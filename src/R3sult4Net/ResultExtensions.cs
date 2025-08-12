namespace R3sult4Net;

/// <summary>
/// Provides extension methods for working with Result and Result&lt;T&gt; types.
/// </summary>
/// <remarks>
/// This class contains methods to transform, bind, and combine results, as well as to handle
/// asynchronous operations with Task&lt;Result&gt;.
/// </remarks>
/// <example>
/// <code>
/// var result = Result.Ok(42);
/// var mappedResult = result.Map(x => x * 2);
/// </code>
/// </example>
public static class ResultExtensions
{
    /// <summary>
    /// Maps a Result&lt;T&gt; to Result&lt;U&gt; by applying a transformation function.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="r"></param>
    /// <param name="map"></param>
    /// <returns></returns>
    public static Result<U> Map<T, U>(this Result<T> r, Func<T, U> map)
        => r.IsSuccess ? Result<U>.Success(map(r.Value!)) : Result<U>.Failure(r.Error);

    /// <summary>
    /// Binds a Result&lt;T&gt; to Result&lt;U&gt; by applying a function that returns a Result&lt;U&gt;.
    /// If the Result&lt;T&gt; is a failure, it returns the same failure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="r"></param>
    /// <param name="bind"></param>
    /// <returns></returns>
    public static Result<U> Bind<T, U>(this Result<T> r, Func<T, Result<U>> bind)
        => r.IsSuccess ? bind(r.Value!) : Result<U>.Failure(r.Error);

    /// <summary>
    /// Executes an action if the Result&lt;T&gt; is successful.
    /// Returns the original Result&lt;T&gt; regardless of success or failure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="r"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static Result<T> Tap<T>(this Result<T> r, Action<T> action)
    { if (r.IsSuccess) action(r.Value!); return r; }

    /// <summary>
    /// Ensures that the Result&lt;T&gt; meets a specified condition.
    /// If the condition is not met, it returns a failure Result&lt;T&gt;
    /// with the provided error.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="r"></param>
    /// <param name="predicate"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Result<T> Ensure<T>(this Result<T> r, Func<T, bool> predicate, Error error)
        => r.IsSuccess && !predicate(r.Value!) ? Result<T>.Failure(error) : r;

    /// <summary>
    /// Matches the Result&lt;T&gt; to either a success or failure case,
    /// returning a value of type U.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="r"></param>
    /// <param name="onOk"></param>
    /// <param name="onErr"></param>
    /// <returns></returns>
    /// <remarks>
    /// This method allows you to handle both success and failure cases in a functional style.
    /// </remarks>
    /// <example>
    /// <code>
    /// var result = Result.Ok(42);
    /// var message = result.Match(
    ///     onOk: value => $"Success: {value}",
    ///     onErr: error => $"Error: {error.Message}"
    /// );
    /// </code>
    /// </example>
    public static U Match<T, U>(this Result<T> r, Func<T, U> onOk, Func<Error, U> onErr)
        => r.IsSuccess ? onOk(r.Value!) : onErr(r.Error);

}

/// <summary>
/// Provides extension methods for working with Task&lt;Result&gt; and Task&lt;Result&lt;T&gt;&gt; types.
/// </summary>
public static class TaskResultExtensions
{
    /// <summary>
    /// Maps a Task&lt;Result&gt; to Task&lt;Result&lt;U&gt;&gt; by applying a transformation function.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="t"></param>
    /// <param name="map"></param>
    /// <returns></returns>
    /// <remarks>
    /// This method allows you to transform the result of an asynchronous operation.
    /// </remarks>
    /// <example>
    /// <code>
    /// var taskResult = Task.FromResult(Result.Ok(42));
    /// var mappedTaskResult = taskResult.Map(x => x * 2);
    /// </code>
    /// </example>
    public static async Task<Result<U>> Map<T, U>(this Task<Result<T>> t, Func<T, U> map)
        => (await t.ConfigureAwait(false)).Map(map);

    /// <summary>
    /// Binds a Task&lt;Result&gt; to Task&lt;Result&lt;U&gt;&gt; by applying a function that returns a Task&lt;Result&lt;U&gt;&gt;&gt;.
    /// If the Task&lt;Result&gt; is a failure, it returns the same failure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="t"></param>
    /// <param name="bind"></param>
    /// <returns></returns>
    /// <remarks>
    /// This method allows you to chain asynchronous operations that return results.
    /// </remarks>
    /// <example>
    /// <code>
    /// var taskResult = Task.FromResult(Result.Ok(42));
    /// var boundTaskResult = await taskResult.Bind(x => Task.FromResult(Result.Ok(x * 2)));
    /// </code>
    /// </example>
    public static async Task<Result<U>> Bind<T, U>(this Task<Result<T>> t, Func<T, Task<Result<U>>> bind)
    {
        var r = await t.ConfigureAwait(false);
        return r.IsSuccess ? await bind(r.Value!).ConfigureAwait(false) : Result<U>.Failure(r.Error);
    }

    /// <summary>
    /// Executes an action if the Task&lt;Result&gt; is successful.
    /// Returns the original Task&lt;Result&gt; regardless of success or failure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <remarks>
    /// This method allows you to perform side effects based on the success of an asynchronous operation.
    /// </remarks>
    /// <example>
    /// <code>
    /// var taskResult = Task.FromResult(Result.Ok(42));
    /// var tappedTaskResult = await taskResult.Tap(x => Console.WriteLine($"Value: {x}"));
    /// </code>
    /// </example>
    public static async Task<Result<T>> Tap<T>(this Task<Result<T>> t, Func<T, Task> action)
    {
        var r = await t.ConfigureAwait(false);
        if (r.IsSuccess) await action(r.Value!).ConfigureAwait(false);
        return r;
    }
}

/// <summary>
/// Provides static methods for creating Result and Result&lt;T&gt; instances.
/// </summary>
public static class Results
{
    /// <summary>
    /// Creates a successful Result with no value.
    /// </summary>
    /// <returns></returns>
    public static Result Ok() => Result.Success();
    /// <summary>
    /// Creates a successful Result with the specified value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Result<T> Ok<T>(T value) => Result<T>.Success(value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Result Fail(string code, string message, ErrorType type = ErrorType.Failure)
        => Result.Failure(new Error(code, message, type));

    /// <summary>
    /// Creates a failed Result&lt;T&gt; with the specified error.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Result<T> Fail<T>(string code, string message, ErrorType type = ErrorType.Failure)
        => Result<T>.Failure(new Error(code, message, type));
}