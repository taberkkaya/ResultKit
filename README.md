<p align="center">
  <img src="icon.jpg" alt="ResultKit Logo" width="200" />
  <br>
  <h1 align="center">ResultKit</h1>
</p>

> **Clean, lightweight, and extensible Result type implementation for .NET**  
> Simplifies error handling and functional programming patterns by encapsulating success and failure states consistently.

## 🚀 Features

- Provides a clear and consistent way to represent success and failure states
- Supports chaining and functional composition for better error handling
- Minimal dependencies to keep the library lightweight and performant
- Extensible design to customize behavior as needed
- Suitable for both small projects and scalable enterprise applications
- Open source and MIT licensed for maximum flexibility

## 📦 Installation

Install ResultKit via NuGet Package Manager Console:

```bash
Install-Package ResultKit
```

Or using the .NET CLI:

```bash
dotnet add package ResultKit
```

## 📂 Basic Usage

> ResultKit allows you to represent success and failure states consistently with a simple Result<T> type.

### Creating and returning results

> You can explicitly create success or failure results:

```csharp
using ResultKit;

Result<int> Divide(int numerator, int denominator)
{
    if (denominator == 0)
        return Result<int>.Failure("Cannot divide by zero.");

    return Result<int>.Succeed(numerator / denominator);
}

Console.WriteLine(Divide(10,0));
Console.WriteLine(Divide(10,2));
```

```console
{"data":0,"errorMessages":["Cannot divide by zero."],"isSuccessful":false,"statusCode":500}
{"data":5,"errorMessages":null,"isSuccessful":true,"statusCode":200}
```

### Implicit conversion from raw values

> You can also return raw values directly (like strings or other types) from methods returning Result<T>. Thanks to implicit conversion, these values are automatically wrapped into successful results:

```csharp
using ResultKit;

Result<string> GetMessage(bool success)
{
    if (success)
        return "Operation succeeded!";  // Implicitly converted to Result<string>
    else
        return Result<string>.Failure("Operation failed.");
}

var result = GetMessage(true);

if (result.IsSuccessful)
    Console.WriteLine(result.Data);
else
    Console.WriteLine(result.ErrorMessages);
```

---

### ✨ Contribution

> Feel free to fork this repository and contribute your improvements.

---

### 🪪 License

> This project is open-source and available under the MIT License.

---

### 🧠 Inspired By

> This project is inspired by the work of [Taner Saydam](https://github.com/TanerSaydam).  
> Check out his GitHub profile and repositories for more: https://github.com/TanerSaydam

---

<p align="center"> <img src="https://skillicons.dev/icons?i=dotnet,github,visualstudio" /> </p>
