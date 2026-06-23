// -----------------------------------------------------------------------
// <copyright file="SubtractionOperatorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class SubtractionOperatorTests
{
    /// <summary>
    /// Verifies: should negate right hand side when polynomials are subtracted.
    /// </summary>
    [Test]
    public async Task Should_negate_right_hand_side_when_polynomials_are_subtracted()
    {
        var left = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(7, 3),
            new PolynomialTerm<double, int>(4, 0));
        var right = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(5, 3),
            new PolynomialTerm<double, int>(1, 1));

        var result = left - right;

        await Assert.That(result).IsEqualTo(
            new LimitPolynomial<double, int>(
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(-1, 1),
                new PolynomialTerm<double, int>(4, 0)));
    }

    /// <summary>
    /// Verifies: should negate all coefficients when unary minus is applied.
    /// </summary>
    [Test]
    public async Task Should_negate_all_coefficients_when_unary_minus_is_applied()
    {
        var value = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(2, 3),
            new PolynomialTerm<double, int>(-4, 0));

        await Assert.That(-value).IsEqualTo(
            new LimitPolynomial<double, int>(
                new PolynomialTerm<double, int>(-2, 3),
                new PolynomialTerm<double, int>(4, 0)));
    }
}