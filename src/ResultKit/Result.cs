namespace ResultKit;

/// <summary>
/// Represents the result of an operation, containing success status and error/validation details if failed.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Error detail for failure (nullable).
    /// </summary>
    public Error? Error { get; }

    /// <summary>
    /// Validation errors (nullable, not empty only for validation errors).
    /// </summary>
    public IReadOnlyCollection<ValidationError>? ValidationErrors { get; }

    /// <summary>
    /// Protected ctor. Use static factory methods.
    /// </summary>
    protected Result(bool isSuccess, Error? error = null, IReadOnlyCollection<ValidationError>? validationErrors = null)
    {
        IsSuccess = isSuccess;
        Error = error;
        ValidationErrors = validationErrors;
    }

    /// <summary>
    /// Creates a successful Result instance.
    /// </summary>
    public static Result Success() => new Result(true);

    /// <summary>
    /// Creates a failed Result instance with error.
    /// </summary>
    /// <param name="error">Error detail</param>
    public static Result Failure(Error error) => new Result(false, error);

    /// <summary>
    /// Creates a failed Result instance with validation errors.
    /// </summary>
    /// <param name="validationErrors">Validation error collection</param>
    public static Result ValidationFailure(IEnumerable<ValidationError> validationErrors)
        => new Result(false, null, validationErrors is IReadOnlyCollection<ValidationError> c ? c : new List<ValidationError>(validationErrors));

    /// <summary>
    /// Creates a failed Result instance from an exception.
    /// </summary>
    /// <param name="ex">Exception to wrap</param>
    public static Result FromException(Exception ex)
    {
        return Failure(new Error(
            ex.GetType().Name,
            ex.Message + (ex.InnerException != null ? $" | Inner: {ex.InnerException.Message}" : "")
        ));
    }
}

/// <summary>
/// Represents the result of an operation, with a value if successful, or error/validation details if failed.
/// </summary>
/// <typeparam name="T">Type of value on success</typeparam>
public class Result<T> : Result
{
    /// <summary>
    /// The returned value if successful, otherwise null.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// Protected ctor for success.
    /// </summary>
    /// <param name="value">Value to return</param>
    private Result(T? value) : base(true)
    {
        Value = value;
    }

    /// <summary>
    /// Protected ctor for error or validation.
    /// </summary>
    private Result(Error error) : base(false, error) { }
    private Result(IReadOnlyCollection<ValidationError> validationErrors) : base(false, null, validationErrors) { }

    /// <summary>
    /// Creates a successful Result instance with value.
    /// </summary>
    /// <param name="value">Returned value</param>
    public static Result<T> Success(T value) => new Result<T>(value);

    /// <summary>
    /// Creates a failed Result instance with error.
    /// </summary>
    /// <param name="error">Error detail</param>
    public static Result<T> Failure(Error error) => new Result<T>(error);

    /// <summary>
    /// Creates a failed Result instance with validation errors.
    /// </summary>
    /// <param name="validationErrors">Validation error collection</param>
    public static Result<T> ValidationFailure(IEnumerable<ValidationError> validationErrors)
        => new Result<T>(validationErrors is IReadOnlyCollection<ValidationError> c ? c : new List<ValidationError>(validationErrors));

    /// <summary>
    /// Creates a failed Result<T> instance from an exception.
    /// </summary>
    /// <param name="ex">Exception to wrap</param>
    public static Result<T> FromException(Exception ex)
    {
        return Failure(new Error(
            ex.GetType().Name,
            ex.Message + (ex.InnerException != null ? $" | Inner: {ex.InnerException.Message}" : "")
        ));
    }

    /// <summary>
    /// Allows implicit conversion from T to Result<T> as a success result.
    /// </summary>
    /// <param name="value">Value to wrap</param>
    public static implicit operator Result<T>(T value) => Success(value);
}
