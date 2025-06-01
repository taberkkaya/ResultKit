using Microsoft.AspNetCore.Mvc;

namespace ResultKit;

/// <summary>
/// Provides extension methods to convert <see cref="Result{T}"/> to <see cref="ActionResult{T}"/> with HTTP status code mapping for ASP.NET Core Web API.
/// </summary>
public static class ActionResultHelper
{
    /// <summary>
    /// Converts a <see cref="Result{T}"/> to an <see cref="ActionResult{Result{T}}"/> with the appropriate HTTP status code based on the result.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result.</typeparam>
    /// <param name="result">The result to convert.</param>
    /// <returns>
    /// Returns 200 OK for success, 400 Bad Request for validation errors,
    /// 404 Not Found for not found errors, 401 Unauthorized for unauthorized errors,
    /// 409 Conflict for conflict errors, and 400 Bad Request by default for any other failure.
    /// </returns>
    public static ActionResult<Result<T>> ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result);

        if (result.ValidationErrors is not null && result.ValidationErrors.Count > 0)
            return new BadRequestObjectResult(result);

        if (result.Error is not null)
        {
            if (result.Error.Code == ErrorCodes.NotFound)
                return new NotFoundObjectResult(result);
            if (result.Error.Code == ErrorCodes.Unauthorized)
                return new ObjectResult(result) { StatusCode = 401 };
            if (result.Error.Code == ErrorCodes.Conflict)
                return new ConflictObjectResult(result);

        }

        // Default: 400 Bad Request
        return new BadRequestObjectResult(result);
    }
}
