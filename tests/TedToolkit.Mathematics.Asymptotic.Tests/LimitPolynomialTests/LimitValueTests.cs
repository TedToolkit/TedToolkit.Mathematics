// -----------------------------------------------------------------------
// <copyright file="LimitValueTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class LimitValueTests
{
    /// <summary>
    /// Verifies: should forward limit value of leading term when limit value is requested.
    /// </summary>
    [Test]
    public async Task Should_forward_limit_value_of_leading_term_when_limit_value_is_requested()
    {
        var polynomial = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(-2, 3), new PolynomialTerm<double, int>(5, 0));

        await Assert.That(polynomial.LimitValue)
            .IsEqualTo(new LimitResult<double>(LimitState.NEGATIVE_INFINITY, 0));
    }

    /// <summary>
    /// Verifies: should preserve constant limit value and positive approach side when tail is positive infinitesimal.
    /// </summary>
    [Test]
    public async Task Should_preserve_constant_limit_value_and_positive_approach_side_when_tail_is_positive_infinitesimal()
    {
        var polynomial = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(5, 0), new PolynomialTerm<double, int>(2, -1));

        await Assert.That(polynomial.LimitValue)
            .IsEqualTo(new LimitResult<double>(LimitState.POSITIVE_INFINITESIMAL, 5));
    }

    /// <summary>
    /// Verifies: should preserve constant limit value and negative approach side when tail is negative infinitesimal.
    /// </summary>
    [Test]
    public async Task Should_preserve_constant_limit_value_and_negative_approach_side_when_tail_is_negative_infinitesimal()
    {
        var polynomial = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(5, 0), new PolynomialTerm<double, int>(-2, -1));

        await Assert.That(polynomial.LimitValue)
            .IsEqualTo(new LimitResult<double>(LimitState.NEGATIVE_INFINITESIMAL, 5));
    }

    /// <summary>
    /// Verifies: should return finite constant value when lower order tail is absent.
    /// </summary>
    [Test]
    public async Task Should_return_finite_constant_value_when_lower_order_tail_is_absent()
    {
        var polynomial = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(5, 0));

        await Assert.That(polynomial.LimitValue)
            .IsEqualTo(new LimitResult<double>(LimitState.FINITE, 5));
    }

    /// <summary>
    /// Verifies: should return zero with positive approach side when leading term is positive infinitesimal.
    /// </summary>
    [Test]
    public async Task Should_return_zero_with_positive_approach_side_when_leading_term_is_positive_infinitesimal()
    {
        var polynomial = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, -1), new PolynomialTerm<double, int>(-3, -2));

        await Assert.That(polynomial.LimitValue)
            .IsEqualTo(new LimitResult<double>(LimitState.POSITIVE_INFINITESIMAL, 0));
    }
}