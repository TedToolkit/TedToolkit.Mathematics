// -----------------------------------------------------------------------
// <copyright file="LimitValueTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.PolynomialTermTests;

internal sealed class LimitValueTests
{
    /// <summary>
    /// Verifies: should return finite value when exponent is zero.
    /// </summary>
    [Test]
    public async Task Should_return_finite_value_when_exponent_is_zero()
    {
        var term = new PolynomialTerm<double, int>(5, 0);

        await Assert.That(term.LimitValue).IsEqualTo(new LimitResult<double>(LimitState.FINITE, 5));
    }

    /// <summary>
    /// Verifies: should return zero finite value when exponent is negative.
    /// </summary>
    [Test]
    public async Task Should_return_zero_finite_value_when_exponent_is_negative()
    {
        var term = new PolynomialTerm<double, int>(5, -1);

        await Assert.That(term.LimitValue).IsEqualTo(new LimitResult<double>(LimitState.FINITE, 0));
    }

    /// <summary>
    /// Verifies: should return infinite state when exponent is positive.
    /// </summary>
    [Test]
    public async Task Should_return_infinite_state_when_exponent_is_positive()
    {
        await Assert.That(new PolynomialTerm<double, int>(5, 1).LimitValue)
            .IsEqualTo(new LimitResult<double>(LimitState.POSITIVE_INFINITY, 0));
        await Assert.That(new PolynomialTerm<double, int>(-5, 1).LimitValue)
            .IsEqualTo(new LimitResult<double>(LimitState.NEGATIVE_INFINITY, 0));
    }
}