<div align="center">
  <img src="assets/logo-1250x1250.png" width="120" alt="ResultKit logo" />

# ResultKit

**A Modular & Extensible Result Pattern Library for .NET**

</div>

---

## ✨ Overview

**ResultKit** is a lightweight, strongly-typed, and extensible result abstraction for .NET projects. It enables safe, explicit handling of operation outcomes—success, failure, validation errors, and exceptions—while improving API and service design.

---

## 📦 Packages

| Package     | Description                                 |
| ----------- | ------------------------------------------- |
| `ResultKit` | Core result, error, validation abstractions |

---

## 🛠️ Installation

```bash
Install-Package ResultKit
```

---

## 🚀 Getting Started

```csharp
using ResultKit;

// Simple success/failure
var ok = Result.Success();
var fail = Result.Failure(new Error(ErrorCodes.Validation, "Validation failed"));

// With validation errors
var validation = Result.ValidationFailure(new[] { new ValidationError("Field", "Message") });

// Strongly-typed generic result
var value = Result<string>.Success("foo");
Result<string> fromDto = "implicit"; // Implicit conversion

// Extension methods
var mapped = value.Map(s => s.ToUpper());
```

---

## 🧩 ASP.NET Core Integration

`Result<T>` can be turned into an API response with auto status code mapping:

```csharp
[HttpGet("{id}")]
public ActionResult<Result<UserDto>> GetUser(int id)
{
    var dto = new UserDto { Id = id, Name = "Demo" };
    return Result<UserDto>.Success(dto).ToActionResult();
}
```

- `Ok` for success
- `BadRequest` for validation errors
- `NotFound` for not found errors
- `Conflict`, `Unauthorized`, etc.

---

## 🧪 Unit Testing

Easy to verify operation results and error handling:

```csharp
[Fact]
public void ImplicitOperator_Wraps_Success()
{
    Result<string> result = "hi";
    Assert.True(result.IsSuccess);
    Assert.Equal("hi", result.Value);
}
```

To run all tests:

```bash
dotnet test tests/ResultKit.Tests
```

---

## 📁 Folder Structure

```
src/
├── ResultKit                  # Core result implementation
samples/
├── ResultKit.SampleApi        # Example WebAPI usage
├── ...
tests/
└── ResultKit.Tests            # Unit tests
```

---

## 🧱 Architecture

- `Result`/`Result<T>`: success, error, validation error, exception
- `Error`, `ValidationError`: standardized error contracts
- Extension methods: Map, Bind, Match, ToActionResult
- **Immutable, functional, testable design**

---

## 💡 Motivation

Tired of ambiguous error returns or magic strings? ResultKit brings:

- 🔒 Type-safety for all result flows
- 🔁 Cleaner APIs & predictable error handling
- 🧪 Seamless testing
- 🧩 Integration-ready for service & API layers

---

## 📜 License

MIT © [Ataberk Kaya](https://github.com/taberkkaya)

---

> 📎 For detailed usage and API samples, see `/samples` and `/tests` folders

---

<div align="center">
Made with ❤️ by [@taberkkaya](https://github.com/taberkkaya)
</div>
