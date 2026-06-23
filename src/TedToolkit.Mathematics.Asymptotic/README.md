# TedToolkit.Mathematics.Asymptotic

`TedToolkit.Mathematics.Asymptotic` is a small .NET library for modeling asymptotic expressions and comparing their behavior as `x -> +infinity`.

It is useful when you want code to reason about growth rate, leading terms, and limit behavior instead of treating these ideas as ad-hoc numeric rules.

## Installation

```shell
dotnet add package TedToolkit.Mathematics.Asymptotic
```

## Core Types

### `PolynomialTerm<TCoefficient, TExponent>`

Represents one term of an asymptotic expression, such as:

- `3x^2`
- `-5x`
- `7`
- `2x^-1`

Each term has:

- a finite coefficient
- an exponent

For a zero coefficient, the exponent is normalized to `0`.

### `LimitPolynomial<TCoefficient, TExponent>`

Represents a normalized asymptotic expression made of multiple terms.

When you construct it:

- terms with the same exponent are merged by summing coefficients
- zero terms are removed
- terms are kept in descending exponent order
- an all-zero expression becomes `0`

### `LimitResult<TValue>`

Represents the limit of a term or expression as `x -> +infinity`.

Possible states are:

- `FINITE`
- `POSITIVE_INFINITY`
- `NEGATIVE_INFINITY`

## Basic Usage

```csharp
using TedToolkit.Mathematics.Asymptotic;

var term = new PolynomialTerm<double, int>(3, 2);   // 3x^2
var constant = new PolynomialTerm<double, int>(5);  // 5
```

Build an asymptotic expression from several terms:

```csharp
using TedToolkit.Mathematics.Asymptotic;

var expression = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(3, 2),
    new PolynomialTerm<double, int>(-4, 1),
    new PolynomialTerm<double, int>(10, 0),
]);
```

If you add duplicated exponents, they are merged automatically:

```csharp
var simplified = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(3, 2),
    new PolynomialTerm<double, int>(5, 2),
    new PolynomialTerm<double, int>(-8, 2),
    new PolynomialTerm<double, int>(1, 0),
]);

// simplified is 1
```

## Evaluating Limits

The limit is determined by the leading term.

```csharp
var a = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(2, 3),
    new PolynomialTerm<double, int>(-1, 1),
]);

Console.WriteLine(a.LimitValue); // +∞
```

Examples:

- positive coefficient with positive exponent -> `+∞`
- negative coefficient with positive exponent -> `-∞`
- exponent `0` -> the finite constant value
- negative exponent -> finite limit `0`

```csharp
Console.WriteLine(new PolynomialTerm<double, int>(4, 0).LimitValue);   // 4
Console.WriteLine(new PolynomialTerm<double, int>(4, -2).LimitValue);  // 0
Console.WriteLine(new PolynomialTerm<double, int>(-4, 3).LimitValue);  // -∞
```

## Comparing Growth Rate

Use `CompareGrowthRateTo` when you care only about leading-order growth.

```csharp
var quadratic = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(1, 2),
]);

var linear = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(100, 1),
]);

Console.WriteLine(quadratic.CompareGrowthRateTo(linear) > 0); // True
```

Use `EqualsGrowthRate` when two expressions should be treated as having the same leading term:

```csharp
var left = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(2, 3),
    new PolynomialTerm<double, int>(1, 0),
]);

var right = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(2, 3),
    new PolynomialTerm<double, int>(-100, 1),
]);

Console.WriteLine(left.EqualsGrowthRate(right)); // True
```

## Comparing Full Expressions

`LimitPolynomial<TCoefficient, TExponent>` also supports numeric-style comparison operators.

These comparisons are lexicographic by exponent first and coefficient second, not only by final limit category.

```csharp
var left = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(1, 2),
    new PolynomialTerm<double, int>(3, 0),
]);

var right = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(1, 2),
    new PolynomialTerm<double, int>(1, 0),
]);

Console.WriteLine(left > right); // True
```

## Arithmetic

You can combine expressions with the usual operators:

- `+`
- `-`
- `*`
- `/`
- `%`

```csharp
var x2 = new LimitPolynomial<double, int>([new PolynomialTerm<double, int>(1, 2)]);
var x = new LimitPolynomial<double, int>([new PolynomialTerm<double, int>(1, 1)]);

var sum = x2 + x;
var product = x2 * x;
var quotient = product / x2;

Console.WriteLine(sum);      // lim[x→∞] (x² + x)
Console.WriteLine(product);  // lim[x→∞] x³
Console.WriteLine(quotient); // lim[x→∞] x
```

## Formatting

Both `PolynomialTerm<TCoefficient, TExponent>` and `LimitPolynomial<TCoefficient, TExponent>` provide formatted string output.

```csharp
var expression = new LimitPolynomial<double, int>(
[
    new PolynomialTerm<double, int>(3.5, 2),
    new PolynomialTerm<double, int>(-1.25, 1),
    new PolynomialTerm<double, int>(8, 0),
]);

Console.WriteLine(expression.ToString("0.##", "0", null));
```

The default string form is intended to be readable for asymptotic notation, for example `lim[x→∞] (x² + x)`. For multi-term expressions, output may be truncated with `maxTerms`.

## Notes

- This library models expressions as `x -> +infinity`.
- Coefficients must be finite values.
- Parsing from strings is not currently supported.
- Generic parameters must satisfy .NET numeric interfaces such as `INumber<T>` and `ISignedNumber<T>`.

## Supported Frameworks

- .NET 7.0
- .NET 8.0
- .NET 9.0
- .NET 10.0
