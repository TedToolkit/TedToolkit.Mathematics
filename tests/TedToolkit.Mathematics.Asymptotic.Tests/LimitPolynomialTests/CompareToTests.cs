// -----------------------------------------------------------------------
// <copyright file="CompareToTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class CompareToTests
{
    /// <summary>
    /// Verifies: should compare highest different exponent first when polynomials differ.
    /// </summary>
    [Test]
    public async Task Should_compare_highest_different_exponent_first_when_polynomials_differ()
    {
        var left = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(1, 3));
        var right = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(100, 2));

        await Assert.That(left.CompareTo(right)).IsGreaterThan(0);
        await Assert.That(right.CompareTo(left)).IsLessThan(0);
    }

    /// <summary>
    /// Verifies: should fall back to coefficients when exponents match.
    /// </summary>
    [Test]
    public async Task Should_fall_back_to_coefficients_when_exponents_match()
    {
        var left = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 3));
        var right = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(5, 3));

        await Assert.That(left.CompareTo(right)).IsLessThan(0);
    }

    /// <summary>
    /// Verifies: should compare remaining terms when leading terms are equal.
    /// </summary>
    [Test]
    public async Task Should_compare_remaining_terms_when_leading_terms_are_equal()
    {
        var left = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(2, 3),
            new PolynomialTerm<double, int>(1, 0));
        var right = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(2, 3),
            new PolynomialTerm<double, int>(-1, 0));

        await Assert.That(left.CompareTo(right)).IsGreaterThan(0);
        await Assert.That(right.CompareTo(left)).IsLessThan(0);
    }
}