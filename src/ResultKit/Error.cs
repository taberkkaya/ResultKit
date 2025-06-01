namespace ResultKit;

/// <summary>
/// Represents a standard error for failed operations.
/// </summary>
public class Error
{
    /// <summary>
    /// Application-specific error code.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Human-readable error message.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Creates a new error instance.
    /// </summary>
    /// <param name="code">Application-specific code</param>
    /// <param name="message">Error message</param>
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    /// <summary>
    /// Returns a string representation of the error.
    /// </summary>
    public override string ToString() => $"[{Code}] {Message}";
}
