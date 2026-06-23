# TedToolkit.Mathematics

[![License: LGPL-3.0](https://img.shields.io/badge/License-LGPL--3.0-blue.svg)](COPYING.LESSER)
[![NuGet: Asymptotic](https://img.shields.io/nuget/v/TedToolkit.Mathematics.Asymptotic?label=Asymptotic)](https://www.nuget.org/packages/TedToolkit.Mathematics.Asymptotic)
[![NuGet: Interval](https://img.shields.io/nuget/v/TedToolkit.Mathematics.Interval?label=Interval)](https://www.nuget.org/packages/TedToolkit.Mathematics.Interval)

`TedToolkit.Mathematics` contains small .NET libraries for mathematical concepts that benefit from explicit domain types instead of loosely related primitive values.

This repository currently ships two NuGet packages:

| Package | Description |
| --- | --- |
| `TedToolkit.Mathematics.Asymptotic` | Models polynomial-like asymptotic expressions, exposes limit classification, and compares leading-order growth. |
| `TedToolkit.Mathematics.Interval` | Models intervals and normalized interval sets with explicit open and closed boundary semantics. |

## Packages

### TedToolkit.Mathematics.Asymptotic

```shell
dotnet add package TedToolkit.Mathematics.Asymptotic
```

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

Console.WriteLine(quadratic.LimitValue);
Console.WriteLine(quadratic.CompareGrowthRateTo(linear) > 0);
```

Key public types:

- `PolynomialTerm<TCoefficient, TExponent>`
- `LimitPolynomial<TCoefficient, TExponent>`
- `LimitResult<TValue>`
- `LimitState`

Target frameworks: `net7.0`, `net8.0`, `net9.0`, `net10.0`.

Package documentation: [src/TedToolkit.Mathematics.Asymptotic/README.md](src/TedToolkit.Mathematics.Asymptotic/README.md)

### TedToolkit.Mathematics.Interval

```shell
dotnet add package TedToolkit.Mathematics.Interval
```

```csharp
using TedToolkit.Mathematics.Interval;

var closed = Interval<int>.Create(1, 5, out _);
var openLeft = Interval<int>.Create(
    new IntervalEnd<int>(1, BoundaryType.OPEN),
    new IntervalEnd<int>(5),
    out _);

Console.WriteLine(closed.IsInside(1));
Console.WriteLine(openLeft.IsInside(1));
Console.WriteLine(closed.GetRelationTo(openLeft));
```

Key public types:

- `Interval<TData>`
- `IntervalEnd<TData>`
- `IntervalSet<TData>`
- `BoundaryType`
- `RelationType`

Target frameworks: `net6.0`, `net7.0`, `net8.0`, `net9.0`, `net10.0`, `net472`, `net48`, `netstandard2.0`, `netstandard2.1`.

Package documentation: [src/TedToolkit.Mathematics.Interval/README.md](src/TedToolkit.Mathematics.Interval/README.md)

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

The `externals/TedToolkit` submodule provides shared build props and repository build infrastructure.

## Development

Clone with submodules:

```shell
git clone --recursive <repository-url>
cd TedToolkit.Mathematics
```

If needed later:

```shell
git submodule update --init --recursive
```

Build the solution:

```shell
dotnet build TedToolkit.Mathematics.slnx -c Release
```

Run the test projects:

```shell
dotnet run --project tests/TedToolkit.Mathematics.Asymptotic.Tests/TedToolkit.Mathematics.Asymptotic.Tests.csproj -c Release
dotnet run --project tests/TedToolkit.Mathematics.Interval.Tests/TedToolkit.Mathematics.Interval.Tests.csproj -c Release
```

Run the repository pipeline:

```shell
dotnet run --project Build/Build.csproj
```

## License

Licensed under [LGPL-3.0](COPYING.LESSER). See [COPYING](COPYING) and [COPYING.LESSER](COPYING.LESSER).
