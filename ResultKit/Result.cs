using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ResultKit;

/// <summary>
/// Represents the result of an operation, encapsulating data, error messages, status and success information.
/// </summary>
/// <typeparam name="T">The type of data to be returned in case of success.</typeparam>
public sealed class Result<T>
{
    /// <summary>
    /// The data returned by the operation, if successful.
    /// </summary>
    [JsonPropertyName("data")]
    public T? Data { get; set; }

    /// <summary>
    /// List of error messages returned by the operation, if failed.
    /// </summary>
    [JsonPropertyName("errorMessages")]
    public List<string>? ErrorMessages { get; set; }

    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    [JsonPropertyName("isSuccessful")]
    public bool IsSuccessful { get; set; } = true;

    /// <summary>
    /// The HTTP status code associated with the result.
    /// </summary>
    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; } = (int)HttpStatusCode.OK;

    /// <summary>
    /// Parameterless constructor for deserialization or manual object creation.
    /// </summary>
    [JsonConstructor]
    public Result() { }

    /// <summary>
    /// Creates a successful result with the provided data.
    /// </summary>
    /// <param name="data">The payload data.</param>
    public Result(T data)
    {
        Data = data;
    }

    /// <summary>
    /// Creates a failed result with multiple error messages.
    /// </summary>
    public Result(int statusCode, List<string> errorMessages)
    {
        IsSuccessful = false;
        StatusCode = statusCode;
        ErrorMessages = errorMessages;
    }

    /// <summary>
    /// Creates a failed result with a single error message.
    /// </summary>
    public Result(int statusCode, string errorMessage)
    {
        IsSuccessful = false;
        StatusCode = statusCode;
        ErrorMessages = new() { errorMessage };
    }

    /// <summary>
    /// Implicit conversion from data to success result.
    /// </summary>
    public static implicit operator Result<T>(T data) => new(data);

    /// <summary>
    /// Implicit conversion from tuple to failure result (multiple errors).
    /// </summary>
    public static implicit operator Result<T>((int statusCode, List<string> errorMessages) parameters)
        => new(parameters.statusCode, parameters.errorMessages);

    /// <summary>
    /// Implicit conversion from tuple to failure result (single error).
    /// </summary>
    public static implicit operator Result<T>((int statusCode, string errorMessage) parameters)
        => new(parameters.statusCode, parameters.errorMessage);

    /// <summary>
    /// Creates a success result with data.
    /// </summary>
    public static Result<T> Succeed(T data) => new(data);

    /// <summary>
    /// Creates a failure result with multiple errors and custom status code.
    /// </summary>
    public static Result<T> Failure(int statusCode, List<string> errorMessages) => new(statusCode, errorMessages);

    /// <summary>
    /// Creates a failure result with a single error and custom status code.
    /// </summary>
    public static Result<T> Failure(int statusCode, string errorMessage) => new(statusCode, errorMessage);

    /// <summary>
    /// Creates a failure result with a single error and default 500 status code.
    /// </summary>
    public static Result<T> Failure(string errorMessage) => new(500, errorMessage);

    /// <summary>
    /// Creates a failure result with multiple errors and default 500 status code.
    /// </summary>
    public static Result<T> Failure(List<string> errorMessages) => new(500, errorMessages);

    /// <summary>
    /// Returns the JSON representation of the result.
    /// </summary>
    public override string ToString() => JsonSerializer.Serialize(this);
}
