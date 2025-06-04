<div align="center">
  <img src="assets/logo-1250x1250.png" width="120" alt="ResultKit logo" />
  <h1>ResultKit</h1>
  <b>A Modular & Extensible Result Pattern Library for .NET</b>
</div>

---

## ✨ Overview

**ResultKit** is a lightweight, strongly-typed result abstraction for .NET projects. It enables safe and explicit handling of operation outcomes—**success**, **failure**, **validation errors**, and **exceptions**—while improving your API and service design. ResultKit provides a unified way to represent operation results, eliminating ambiguous return codes and ad-hoc error handling.

---

## 📦 Installation

Install via NuGet:

```bash
dotnet add package ResultKit
```

## 📁 Folder Structure

```graphql
src/
└── ResultKit             # Core result implementation (library source)
samples/
└── ResultKit.SampleApi   # Example ASP.NET Core Web API usage
```

## 💡 Motivation

ResultKit was created to enforce clear, consistent outcomes across your application:

- 🔒 Type-Safety for All Result Flows: No more sentinel values or exception-based control flow—return structured Result objects for both success and failure.
- 🔁 Cleaner APIs, Predictable Errors: Service and API methods return a single, explicit result type, making intent and error handling obvious.
- 🧩 Frontend-Friendly: Easily create unified, predictable HTTP responses for API clients and frontend apps.
- 🧪 Seamless Integration: Supports ASP.NET Core with helpers for direct conversion to HTTP responses.

## 📎 Documentation:

See [`src/ResultKit/README.md`](https://github.com/taberkkaya/ResultKit/blob/master/src/ResultKit/README.md) for usage, API details, and integration examples.

## 🤝 Contributing

Contributions and suggestions are welcome!

## 📜 License

MIT © [Ataberk Kaya](https://github.com/taberkkaya)
