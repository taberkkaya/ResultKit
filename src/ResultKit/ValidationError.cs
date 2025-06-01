namespace ResultKit;

/// <summary>
/// Represents a validation error for a specific field.
/// </summary>
public class ValidationError
{
    /// <summary>
    /// The property or field name causing the error.
    /// </summary>
    public string Field { get; }

    /// <summary>
    /// The validation error message.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Creates a new ValidationError instance.
    /// </summary>
    public ValidationError(string field, string message)
    {
        Field = field;
        Message = message;
    }
}
