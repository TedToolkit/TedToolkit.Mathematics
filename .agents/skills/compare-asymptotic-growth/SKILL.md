---
name: compare-asymptotic-growth
description: >-
  Use TedToolkit.Mathematics.Asymptotic to model polynomial-like growth and classify limits as x approaches positive infinity.
---

# Compare Asymptotic Growth

- Express terms as `PolynomialTerm<TCoefficient, TExponent>` and construct a `LimitPolynomial<TCoefficient, TExponent>` using numeric types compatible with the operation.
- Use `LimitValue` for classification and `CompareGrowthRateTo` to compare leading growth. Keep a finite limit and a growth comparison as separate questions.
- Exercise cancellation of equal leading terms, a constant expression, and differing dominant exponents before relying on a comparison.
