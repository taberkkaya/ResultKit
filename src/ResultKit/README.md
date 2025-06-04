<div align="center">
  <img src="../../assets/logo-1250x1250.png" width="120" alt="ResultKit logo" />
  <h1>ResultKit</h1>
  <b>Strongly-Typed, Unified Result Pattern for .NET APIs and Applications</b>
</div>

---

## Overview

**ResultKit** brings a simple but powerful pattern for representing the outcome of any operation in .NET. By wrapping your logic with `Result` or `Result<T>`, you can handle success, errors, validation failures, or exceptions—always in a type-safe and explicit way.  
ResultKit is especially valuable in modern APIs: it encourages unified responses, making error handling and frontend integration easier than ever.

---

## Installation

```bash
dotnet add package ResultKit
```

### Core Usage

#### Service Layer Example

```csharp
using ResultKit;

// Basic success or failure
Result ok = Result.Success();
Result fail = Result.Failure(new Error(ErrorCodes.Validation, "Validation failed"));

// Validation failure
var validation = Result.ValidationFailure(new[]
{
    new ValidationError("Field", "Invalid value")
});

// Strongly-typed success
Result<string> value = Result<string>.Success("foo");

// Implicit conversion
Result<string> implicitOk = "hello"; // treated as Success
```

### Functional Helpers

- _Map:_ Transform the value if success
- _Bind:_ Chain operations that each return a `Result`
- _Match:_ Handle both success and error cases in a single expression

```csharp
var upper = value.Map(x => x.ToUpper());
var chained = value.Bind(s => Result<int>.Success(s.Length));
var resultText = value.Match(
    onSuccess: s => $"OK: {s}",
    onFailure: error => $"Failed: {error.Message}"
);
```

## ASP.NET Core API Integration

ResultKit makes returning consistent HTTP responses trivial:

```csharp
[HttpPost]
public ActionResult<Result<UserDto>> CreateUser(UserDto dto)
{
    if (string.IsNullOrWhiteSpace(dto.Name))
    {
        return Result<UserDto>.ValidationFailure(new[]
        {
            new ValidationError(nameof(dto.Name), "Name is required")
        }).ToActionResult();
    }

    // Simulate user creation
    var created = new UserDto { Name = dto.Name };
    return Result<UserDto>.Success(created).ToActionResult();
}
```

- **200 OK** for success
- **400 Bad Request** for validation errors
- **404 Not Found** for not found errors
- **401 Unauthorized**, **409 Conflict**, etc. (mapped automatically)
- The client/frontend always receives a uniform JSON shape—making client code simpler and more reliable

## Core Components

- **Result / Result<T>:** Wraps success or failure, and (on success) carries a value.
- **Error:** Standard error with code and message.
- **ValidationError:** Field-specific validation error info.
- **Functional Extensions:** `Map`, `Bind`, `Match`, and more.

## Why Standardized Responses Matter

`ResultKit` makes it easy to build APIs that always return the same response shape—regardless of success or error. This means your frontend or client apps can handle results with confidence and less boilerplate:

```json
// Success response (with value)
{
  "isSuccess": true,
  "value": {
    "id": 42,
    "name": "Jane Doe",
    "email": "jane@example.com"
  }
}

// Success response
{
  "isSuccess": true,
  "value": 123
}

// Error response (not found)
{
  "isSuccess": false,
  "error": { "code": "NOT_FOUND", "message": "User not found" }
}
```

## 📜 License

MIT © [Ataberk Kaya](https://github.com/taberkkaya)
