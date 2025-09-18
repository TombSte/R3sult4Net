# R3sult4Net

A lightweight Result type for .NET to model success/failure without exceptions, with support for typed results and simple functional helpers.

- Package Id: `R3sult4Net`
- Targets: .NET Standard 2.0 (works on .NET 6/7/8+)

## Install

```bash
# via dotnet CLI
dotnet add package R3sult4Net
```

## Quick Start

```csharp
using R3sult4Net;

// Non-generic result
Result op = DoSomething();
if (op.IsFailure)
{
    Console.WriteLine($"Error: {op.Error?.Code} - {op.Error?.Description}");
}

// Generic result
Result<User> userRes = GetUser("42");
if (userRes.IsSuccess)
{
    Console.WriteLine(userRes.Value.Name);
}

// Implicit conversions
Result ok = Result.Success();
Result<User> userOk = new User("John"); // implicit Success(User)
Result<User> userErr = new Error("user.not_found", "User not found"); // implicit Failure(Error)

// Map helper (only runs on success)
var userNameRes = userRes.Map(u => u.Name);
```

## API Overview

- `Result.Success()` / `Result.Failure(Error)`
- `Result<T>.Success(T value)` / `Result<T>.Failure(Error)`
- Properties: `IsSuccess`, `IsFailure`, `Error`, and on generic results `Value`
- Implicit conversions from `T` to `Result<T>` and from `Error` to `Result`/`Result<T>`
- Extension: `Map<TIn,TOut>(this Result<TIn>, Func<TIn,TOut>)`

### Error

```csharp
var err = new Error(
    code: "domain.error_code",
    description: "Something went wrong",
    domain: "DomainName",
    errors: new [] { new Error("inner.code", "Inner failure") }
);
```

## Notes

- The library uses modern C# features; ensure your project uses a recent C# language version if needed.
- Tests target .NET 6/7/8; the library targets .NET Standard 2.0 for broad compatibility.

## License

MIT
