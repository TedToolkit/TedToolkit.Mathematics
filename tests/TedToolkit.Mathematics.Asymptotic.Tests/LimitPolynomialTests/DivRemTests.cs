// -----------------------------------------------------------------------
// <copyright file="DivRemTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class DivRemTests
{
    /// <summary>
    /// Verifies: should return quotient and zero remainder when dividend is exactly divisible.
    /// </summary>
    [Test]
    public async Task Should_return_quotient_and_zero_remainder_when_dividend_is_exactly_divisible()
    {
        var dividend = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(1, 2),
            new PolynomialTerm<double, int>(3, 1),
            new PolynomialTerm<double, int>(2, 0));
        var divisor = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(1, 1),
            new PolynomialTerm<double, int>(1, 0));

        var (quotient, remainder) = LimitPolynomial<double, int>.DivRem(dividend, divisor);

        await Assert.That(quotient).IsEqualTo(
            new LimitPolynomial<double, int>(
                new PolynomialTerm<double, int>(1, 1),
                new PolynomialTerm<double, int>(2, 0)));
        await Assert.That(remainder).IsEqualTo(LimitPolynomial<double, int>.Zero);
    }

    /// <summary>
    /// Verifies: should return remaining lower order terms when dividend is not exactly divisible.
    /// </summary>
    [Test]
    public async Task Should_return_remaining_lower_order_terms_when_dividend_is_not_exactly_divisible()
    {
        var dividend = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(1, 2),
            new PolynomialTerm<double, int>(1, 0));
        var divisor = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(1, 1),
            new PolynomialTerm<double, int>(1, 0));

        var (quotient, remainder) = LimitPolynomial<double, int>.DivRem(dividend, divisor);

        await Assert.That(quotient).IsEqualTo(
            new LimitPolynomial<double, int>(
                new PolynomialTerm<double, int>(1, 1),
                new PolynomialTerm<double, int>(-1, 0)));
        await Assert.That(remainder).IsEqualTo(new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 0)));
    }
}