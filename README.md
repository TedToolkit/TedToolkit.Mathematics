# TedToolkit.Disposables

[![NuGet](https://img.shields.io/nuget/v/TedToolkit.Disposables)](https://www.nuget.org/packages/TedToolkit.Disposables)
[![Build](https://github.com/TedToolkit/TedToolkit.Disposables/actions/workflows/build.yml/badge.svg)](https://github.com/TedToolkit/TedToolkit.Disposables/actions/workflows/build.yml)
[![License: LGPL-3.0](https://img.shields.io/badge/License-LGPL--3.0-blue.svg)](https://www.gnu.org/licenses/lgpl-3.0)

Zero-allocation disposable types using `readonly ref struct` for lightweight, stack-allocated resource cleanup in C#.

## Installation

```shell
dotnet add package TedToolkit.Disposables
```

## Types

### `ValueDisposable`

A `readonly ref struct` that executes an action on disposal. Zero heap allocation — lives entirely on the stack.

```csharp
using TedToolkit.Disposables;

using (new ValueDisposable(() => Console.WriteLine("Disposed!")))
{
    // Do work...
} // prints "Disposed!"
```

Useful for scoped operations like temporary state changes:

```csharp
public ValueDisposable BeginBatchUpdate()
{
    _isBatching = true;
    return new ValueDisposable(() =>
    {
        _isBatching = false;
        Flush();
    });
}

// Usage:
using (BeginBatchUpdate())
{
    Add(item1);
    Add(item2);
} // automatically ends batch and flushes
```

### `ConditionalValueDisposable`

A `readonly ref struct` that executes an action on disposal **only when a condition is met**.

```csharp
using TedToolkit.Disposables;

bool shouldCleanUp = SomeCondition();

using (new ConditionalValueDisposable(() => CleanUp(), shouldCleanUp))
{
    // Do work...
} // CleanUp() runs only if shouldCleanUp was true
```

Useful for conditional resource management:

```csharp
public ConditionalValueDisposable AcquireLockIfNeeded(bool needsLock)
{
    if (needsLock)
        _lock.Enter();

    return new ConditionalValueDisposable(() => _lock.Exit(), needsLock);
}
```

## Supported Frameworks

- .NET 6.0, 7.0, 8.0, 9.0, 10.0
- .NET Framework 4.7.2, 4.8
- .NET Standard 2.0

## Building from Source

```shell
git clone --recursive https://github.com/TedToolkit/TedToolkit.Disposables.git
cd TedToolkit.Disposables
dotnet build
```

> The `--recursive` flag is needed to fetch the [TedToolkit](https://github.com/TedToolkit/TedToolkit) build infrastructure submodule under `externals/`.

## License

This project is licensed under the [LGPL-3.0](COPYING.LESSER). See [COPYING](COPYING) and [COPYING.LESSER](COPYING.LESSER) for details.
