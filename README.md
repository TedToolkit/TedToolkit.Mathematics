# TedToolkit.Mathematics

[![License: LGPL-3.0](https://img.shields.io/badge/License-LGPL--3.0-blue.svg)](COPYING.LESSER)
[![NuGet: Asymptotic](https://img.shields.io/nuget/v/TedToolkit.Mathematics.Asymptotic?label=Asymptotic)](https://www.nuget.org/packages/TedToolkit.Mathematics.Asymptotic)
[![NuGet: Interval](https://img.shields.io/nuget/v/TedToolkit.Mathematics.Interval?label=Interval)](https://www.nuget.org/packages/TedToolkit.Mathematics.Interval)

`TedToolkit.Mathematics` is a collection of small .NET libraries for mathematical models that are easy to get wrong when represented as loose primitive values.

This repository currently contains two NuGet packages:

| Package | Purpose |
| --- | --- |
| `TedToolkit.Mathematics.Asymptotic` | Models polynomial-like asymptotic expressions, evaluates limits as `x -> +infinity`, and compares growth rates. |
| `TedToolkit.Mathematics.Interval` | Models mathematical intervals and normalized interval sets with explicit open and closed boundary behavior. |

## Packages

### TedToolkit.Mathematics.Asymptotic

Install:

```shell
dotnet add package TedToolkit.Mathematics.Asymptotic
```

Use this package when code needs to reason about leading terms, limit values, or growth-rate comparison.

```csharp
using TedToolkit.Mathematics.Asymptotic;

var quadratic = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(1, 2),
    new PolynomialTerm<double, int>(3, 0),
]);

var linear = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(100, 1),
]);

Console.WriteLine(quadratic.LimitValue);                   // +∞
Console.WriteLine(quadratic.CompareGrowthRateTo(linear));  // > 0
```

Core types:

- `PolynomialTerm<TCoefficient, TExponent>`
- `LimitPolynomial<TCoefficient, TExponent>`
- `LimitResult<TValue>`
- `LimitState`

Supported frameworks: `.NET 7.0`, `.NET 8.0`, `.NET 9.0`, `.NET 10.0`.

See [src/TedToolkit.Mathematics.Asymptotic/README.md](src/TedToolkit.Mathematics.Asymptotic/README.md) for package-level API details.

### TedToolkit.Mathematics.Interval

Install:

```shell
dotnet add package TedToolkit.Mathematics.Interval
```

Use this package when code needs precise interval semantics instead of scattered `min`, `max`, and boundary checks.

```csharp
using TedToolkit.Mathematics.Interval;

var closed = Interval<int>.Create(1, 5, out _);
var openLeft = Interval<int>.Create(
    new IntervalEnd<int>(1, BoundaryType.OPEN),
    new IntervalEnd<int>(5),
    out _);

Console.WriteLine(closed.IsInside(1));             // True
Console.WriteLine(openLeft.IsInside(1));           // False
Console.WriteLine(closed.GetRelationTo(openLeft)); // ProperSuperset
```

Core types:

- `Interval<TData>`
- `IntervalEnd<TData>`
- `IntervalSet<TData>`
- `BoundaryType`
- `RelationType`

Supported frameworks: `.NET 6.0`, `.NET 7.0`, `.NET 8.0`, `.NET 9.0`, `.NET 10.0`, `.NET Framework 4.7.2`, `.NET Framework 4.8`, `.NET Standard 2.0`, `.NET Standard 2.1`.

See [src/TedToolkit.Mathematics.Interval/README.md](src/TedToolkit.Mathematics.Interval/README.md) for package-level API details.

## Repository Layout

```text
src/
  TedToolkit.Mathematics.Asymptotic/
  TedToolkit.Mathematics.Interval/
tests/
  TedToolkit.Mathematics.Asymptotic.Tests/
  TedToolkit.Mathematics.Interval.Tests/
Build/
  Build.csproj
externals/
  TedToolkit/
```

The `externals/TedToolkit` submodule provides shared build properties, code analysis configuration, and the repository build pipeline.

## Development

Clone with submodules:

```shell
git clone --recursive <repository-url>
cd TedToolkit.Mathematics
```

If the repository was cloned without submodules:

```shell
git submodule update --init --recursive
```

Build:

```shell
dotnet build TedToolkit.Mathematics.slnx -c Release
```

Run tests:

```shell
dotnet run --project tests/TedToolkit.Mathematics.Asymptotic.Tests/TedToolkit.Mathematics.Asymptotic.Tests.csproj -c Release
dotnet run --project tests/TedToolkit.Mathematics.Interval.Tests/TedToolkit.Mathematics.Interval.Tests.csproj -c Release
```

Run the repository pipeline:

```shell
dotnet run --project Build/Build.csproj
```

The pipeline reads `Build/appsettings.json`. Use `Build/appsettings.example.json` as the local template when pipeline options are needed.

## Contributor Notes

- Keep the root README focused on repository orientation, package indexing, and developer workflow.
- Keep package-level API documentation in the matching `src/<Package>/README.md`.
- Add tests under the matching `tests/<Package>.Tests` project.
- This repository enables nullable reference types, generated XML documentation, central package management, and preview C# language features.

## License

This project is licensed under [LGPL-3.0](COPYING.LESSER). See [COPYING](COPYING) and [COPYING.LESSER](COPYING.LESSER) for the full license text.
