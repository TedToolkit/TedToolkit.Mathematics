// -----------------------------------------------------------------------
// <copyright file="ClassificationTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class ClassificationTests
{
    /// <summary>
    /// Verifies: should report constant polynomial characteristics when value is integral constant.
    /// </summary>
    [Test]
    public async Task Should_report_constant_polynomial_characteristics_when_value_is_integral_constant()
    {
        var value = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(4, 0));

        await Assert.That(LimitPolynomial<double, int>.IsCanonical(value)).IsTrue();
        await Assert.That(LimitPolynomial<double, int>.IsComplexNumber(value)).IsFalse();
        await Assert.That(LimitPolynomial<double, int>.IsEvenInteger(value)).IsTrue();
        await Assert.That(LimitPolynomial<double, int>.IsFinite(value)).IsTrue();
        await Assert.That(LimitPolynomial<double, int>.IsImaginaryNumber(value)).IsFalse();
        await Assert.That(LimitPolynomial<double, int>.IsInteger(value)).IsTrue();
        await Assert.That(LimitPolynomial<double, int>.IsNaN(value)).IsFalse();
        await Assert.That(LimitPolynomial<double, int>.IsNormal(value)).IsTrue();
        await Assert.That(LimitPolynomial<double, int>.IsOddInteger(value)).IsFalse();
        await Assert.That(LimitPolynomial<double, int>.IsPositive(value)).IsTrue();
        await Assert.That(LimitPolynomial<double, int>.IsRealNumber(value)).IsTrue();
        await Assert.That(LimitPolynomial<double, int>.IsSubnormal(value)).IsFalse();
        await Assert.That(LimitPolynomial<double, int>.IsZero(value)).IsFalse();
    }

    /// <summary>
    /// Verifies: should report infinite characteristics when leading term grows without bound.
    /// </summary>
    [Test]
    public async Task Should_report_infinite_characteristics_when_leading_term_grows_without_bound()
    {
        var positive = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 3));
        var negative = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(-2, 3));

        await Assert.That(LimitPolynomial<double, int>.IsInfinity(positive)).IsTrue();
        await Assert.That(LimitPolynomial<double, int>.IsPositiveInfinity(positive)).IsTrue();
        await Assert.That(LimitPolynomial<double, int>.IsNegativeInfinity(positive)).IsFalse();
        await Assert.That(LimitPolynomial<double, int>.IsNegative(negative)).IsTrue();
        await Assert.That(LimitPolynomial<double, int>.IsNegativeInfinity(negative)).IsTrue();
    }

    /// <summary>
    /// Verifies: should report zero and subnormal characteristics when polynomial is tiny zero like value.
    /// </summary>
    [Test]
    public async Task Should_report_zero_and_subnormal_characteristics_when_polynomial_is_tiny_zero_like_value()
    {
        var zero = LimitPolynomial<double, int>.Zero;
        var subnormal = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(double.Epsilon, 0));

        await Assert.That(LimitPolynomial<double, int>.IsZero(zero)).IsTrue();
        await Assert.That(LimitPolynomial<double, int>.IsSubnormal(subnormal)).IsTrue();
    }

    /// <summary>
    /// Verifies: should report zero as finite but neither positive nor negative nor normal.
    /// </summary>
    [Test]
    public async Task Should_report_zero_as_finite_but_neither_positive_nor_negative_nor_normal()
    {
        var zero = LimitPolynomial<double, int>.Zero;

        await Assert.That(LimitPolynomial<double, int>.IsFinite(zero)).IsTrue();
        await Assert.That(LimitPolynomial<double, int>.IsInfinity(zero)).IsFalse();
        await Assert.That(LimitPolynomial<double, int>.IsPositive(zero)).IsFalse();
        await Assert.That(LimitPolynomial<double, int>.IsNegative(zero)).IsFalse();
        await Assert.That(LimitPolynomial<double, int>.IsNormal(zero)).IsFalse();
    }
}