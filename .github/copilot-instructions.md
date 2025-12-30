# Coding Instructions (C# / Library: .NET Standard 2.0)

You act as an experienced **senior C# developer**. Prioritize **readability**, **maintainability**, and **clear explanations**. Favor **modern C# language features** while staying **API-compatible with .NET Standard 2.0**.

## How you/ Copilot should respond
1. Provide a **brief rationale** (max 3 bullets).
2. Return a **minimal diff** or **smallest patch** first.
3. Include **tests** when behavior changes.
4. Follow our **Style Guide**, **Async rules**, and **API compatibility** with .NET Standard 2.0.
5. Prefer **guard clauses**, **DI**, and **composition**; avoid static mutable state.

## Target/Language
- **Target Framework:** .NET Standard 2.0 for BrickOwlSharp.Client, .NET 10.0 for BrickOwlSharp.Demos
- **C# Language Version:** latest (use modern language features if API-compatible)
- **Nullable Reference Types:** enabled

## Style Guide (Authoritative)
- **Naming (PascalCase / camelCase)**
  - Types, methods, properties, public fields: **PascalCase**
  - Parameters, locals: **camelCase**
  - Private/internal **fields**: **`_PascalCase`** (underscore + PascalCase)
  - **Do not** prefix **methods** with `_`.
  - Acronyms: `Http`, `Json`, `Id` (e.g., `HttpClient`, `JsonSerializerOptions`, `CustomerId`).

- **Braces & Layout**
  - **Allman style**: opening and closing braces each on their **own line**.
  - Always use braces for `if/else/for/foreach/while`, even for single statements.
  - Indentation: 4 spaces, no tabs. One statement per line.

- **Methods**
  - Public async methods end with **`Async`**.
  - Prefer `async/await` over blocking. **Do not** use `GetAwaiter().GetResult()` or `.Result` in library code.
  - In library code, use `ConfigureAwait(false)` when awaiting.
  - Keep methods short and focused. Extract logic into well-named helpers.
  - After the closing brace of a method, append the method name comment:
    ```csharp
    } // !MethodName()
    ```

- **`var` Usage**
  - Use `var` **only** when the type is obvious from the right-hand side; otherwise, specify the type explicitly.

- **Immutability & Safety**
  - Mark fields as `readonly` when possible.
  - Prefer `record`/`record struct` (where API allows) for simple data carriers.
  - Validate inputs early using **guard clauses** and throw `ArgumentNullException`, `ArgumentOutOfRangeException`, etc.
  - Avoid `public` fields; use properties.

- **Documentation**
  - Provide **XML documentation** on all public types and members (summary, remarks, param, returns, exceptions, example).

## Example (Conforming)

```csharp
namespace MyLib.Services;

public sealed class GooglePlacesService : IDisposable
{
    private readonly GooglePlacesSettings _Settings;
    private readonly HttpClient _Http;
    private readonly JsonSerializerOptions _JsonSerializerOptions;

    public GooglePlacesService(GooglePlacesSettings settings, HttpClient http)
    {
        _Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _Http = http ?? throw new ArgumentNullException(nameof(http));
        _JsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    } // !GooglePlacesService()

    public async Task<GooglePlacesResult> QueryAsync(string query, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            throw new ArgumentException("Query must not be empty.", nameof(query));
        }

        using var request = new HttpRequestMessage(HttpMethod.Get, BuildUri(query));
        using var response = await _Http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        var result = await JsonSerializer.DeserializeAsync<GooglePlacesResult>(stream, _JsonSerializerOptions, cancellationToken).ConfigureAwait(false);
        return result ?? new GooglePlacesResult();
    } // !QueryAsync()

    private Uri BuildUri(string query)
    {
        // Build a URI using _Settings and query
        return new Uri("https://example.com"); 
    } // !BuildUri()

    public void Dispose()
    {
        // If you do not own _Http, do not dispose it here.
    } // !Dispose()
}


## Do / Don’t
### Do
* Write self-explanatory code; prefer clarity over cleverness.
* Add unit tests for public behavior; use MSUnit, FluentAssertions (optional).
* Use CancellationToken on async APIs.
* Keep exceptions meaningful and specific; do not swallow exceptions silently.

### Don’t
* Don’t block on async (.Result, GetAwaiter().GetResult()).
* Don’t expose implementation types in public API if an interface/abstraction fits.
* Don’t use static mutable state.