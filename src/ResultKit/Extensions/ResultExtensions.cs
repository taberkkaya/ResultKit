namespace ResultKit;

public static class ResultExtensions
{
    /// <summary>
    /// Maps the value of a successful Result<T> to a new type, otherwise propagates the failure.
    /// </summary>
    public static Result<TResult> Map<T, TResult>(this Result<T> result, Func<T, TResult> selector)
    {
        if (result.IsSuccess && result.Value is not null)
            return Result<TResult>.Success(selector(result.Value));
        if (result.Error != null)
            return Result<TResult>.Failure(result.Error);
        if (result.ValidationErrors != null)
            return Result<TResult>.ValidationFailure(result.ValidationErrors);
        return Result<TResult>.Failure(new Error("UNKNOWN_ERROR", "Unknown error during Map."));
    }

    /// <summary>
    /// Binds (flat-maps) the result, chaining operations that return Result<TResult>.
    /// </summary>
    public static Result<TResult> Bind<T, TResult>(this Result<T> result, Func<T, Result<TResult>> func)
    {
        if (result.IsSuccess && result.Value is not null)
            return func(result.Value);
        if (result.Error != null)
            return Result<TResult>.Failure(result.Error);
        if (result.ValidationErrors != null)
            return Result<TResult>.ValidationFailure(result.ValidationErrors);
        return Result<TResult>.Failure(new Error("UNKNOWN_ERROR", "Unknown error during Bind."));
    }

    /// <summary>
    /// Pattern matches on the result, calling onSuccess or onFailure as appropriate.
    /// </summary>
    public static TResult Match<T, TResult>(this Result<T> result, Func<T, TResult> onSuccess, Func<Error?, IReadOnlyCollection<ValidationError>?, TResult> onFailure)
    {
        if (result.IsSuccess && result.Value is not null)
            return onSuccess(result.Value);
        return onFailure(result.Error, result.ValidationErrors);
    }
}
