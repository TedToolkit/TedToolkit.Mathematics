---
name: compare-asymptotic-growth
description: >-
  Use TedToolkit.Mathematics.Asymptotic to model polynomial-like growth and classify limits as x approaches positive infinity.
---

# Compare Asymptotic Growth

Model the expression as the limit variable approaches positive infinity. Normalize terms before inferring the leading exponent or the sign of an infinite limit.

## Workflow

- Express terms as `PolynomialTerm<TCoefficient, TExponent>` and construct a `LimitPolynomial<TCoefficient, TExponent>` using numeric types compatible with the operation.
- Use `LimitValue` for classification and `CompareGrowthRateTo` to compare leading growth. Keep a finite limit and a growth comparison as separate questions.
- Exercise cancellation of equal leading terms, a constant expression, and differing dominant exponents before relying on a comparison.

## Example

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

## Check the result

Verify the cancellation case before comparing growth rates. Check the limit classification separately so equal growth does not imply equal values.
