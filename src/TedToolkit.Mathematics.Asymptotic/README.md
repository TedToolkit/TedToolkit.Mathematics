# TedToolkit.Mathematics.Asymptotic

`TedToolkit.Mathematics.Asymptotic` models polynomial-like asymptotic expressions as `x -> +infinity`.

Use it when your code needs explicit reasoning about:

- normalized terms
- leading-order growth
- limit classification
- arithmetic over asymptotic expressions

## Installation

```shell
dotnet add package TedToolkit.Mathematics.Asymptotic
```

## Core Types

### `PolynomialTerm<TCoefficient, TExponent>`

Represents a single term such as `3ω²`, `-5ω`, `7`, or `2ω⁻¹`.

- `TCoefficient` must satisfy `INumber<TCoefficient>`
- `TExponent` must satisfy `ISignedNumber<TExponent>` and `INumber<TExponent>`
- a zero coefficient is normalized to exponent `0`

### `LimitPolynomial<TCoefficient, TExponent>`

Represents a normalized collection of terms ordered by descending exponent.

During construction:

- terms with the same exponent are merged
- zero-coefficient terms are removed
- terms are stored in descending exponent order
- an all-zero input becomes a single zero term

### `LimitResult<TValue>` and `LimitState`

Represents the limit classification returned by `LimitValue`.

Possible states are:

- `FINITE`
- `POSITIVE_INFINITY`
- `NEGATIVE_INFINITY`
- `POSITIVE_INFINITESIMAL`
- `NEGATIVE_INFINITESIMAL`

## Basic Usage

```csharp
using TedToolkit.Mathematics.Asymptotic;

var expression = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(3, 2),
    new PolynomialTerm<double, int>(-4, 1),
    new PolynomialTerm<double, int>(10, 0),
]);

Console.WriteLine(expression);
```

If multiple terms use the same exponent, they are merged automatically:

```csharp
var simplified = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(3, 2),
    new PolynomialTerm<double, int>(5, 2),
    new PolynomialTerm<double, int>(-8, 2),
    new PolynomialTerm<double, int>(1, 0),
]);

Console.WriteLine(simplified); // 1
```

## Limit Evaluation

`LimitValue` is determined by the leading term.

```csharp
var polynomial = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(-2, 3),
    new PolynomialTerm<double, int>(5, 0),
]);

Console.WriteLine(polynomial.LimitValue);
```

Examples of the returned classification:

- positive coefficient with positive exponent -> positive infinity
- negative coefficient with positive exponent -> negative infinity
- exponent `0` -> finite constant
- positive coefficient with negative exponent -> positive infinitesimal
- negative coefficient with negative exponent -> negative infinitesimal

## Growth Comparison

Use `CompareGrowthRateTo` when only the leading term matters:

```csharp
var left = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 3));
var right = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(1, 2));

Console.WriteLine(left.CompareGrowthRateTo(right) > 0); // True
```

Use `EqualsGrowthRate` when two expressions should be treated as having the same leading-order behavior:

```csharp
var a = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(2, 3),
    new PolynomialTerm<double, int>(1, 0),
]);

var b = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(2, 3),
    new PolynomialTerm<double, int>(-100, 1),
]);

Console.WriteLine(a.EqualsGrowthRate(b)); // True
```

## Arithmetic and Comparison

`LimitPolynomial<TCoefficient, TExponent>` supports:

- comparison operators: `==`, `!=`, `<`, `<=`, `>`, `>=`
- arithmetic operators: `+`, `-`, unary `+`, unary `-`, `*`, `/`, `%`

```csharp
var x2 = new LimitPolynomial<double, int>([new PolynomialTerm<double, int>(1, 2)]);
var x = new LimitPolynomial<double, int>([new PolynomialTerm<double, int>(1, 1)]);

var sum = x2 + x;
var product = x2 * x;
var quotient = product / x2;

Console.WriteLine(sum);
Console.WriteLine(product);
Console.WriteLine(quotient);
```

## Formatting

`PolynomialTerm<TCoefficient, TExponent>` and `LimitPolynomial<TCoefficient, TExponent>` support formatted string output.

```csharp
var polynomial = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(12.5, 12),
    new PolynomialTerm<double, int>(-3.25, -2),
]);

Console.WriteLine(polynomial.ToString("0.00", "00", null));
```

The default string form uses `ω` notation, for example `(2ω³ - 4ω + 6)`.

## Notes

- This package models behavior only for `x -> +infinity`.
- `LimitPolynomial<TCoefficient, TExponent>` implements numeric-style interfaces and exposes parse members, but parsing is not currently supported.
- Generic parameters must satisfy the .NET numeric interface constraints used by the public types.

## Target Frameworks

- `net7.0`
- `net8.0`
- `net9.0`
- `net10.0`
