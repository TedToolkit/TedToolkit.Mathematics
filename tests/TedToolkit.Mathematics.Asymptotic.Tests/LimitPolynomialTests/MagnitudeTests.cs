// -----------------------------------------------------------------------
// <copyright file="MagnitudeTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class MagnitudeTests
{
    /// <summary>
    /// Verifies: should choose larger magnitude when max magnitude is requested.
    /// </summary>
    [Test]
    public async Task Should_choose_larger_magnitude_when_max_magnitude_is_requested()
    {
        var smaller = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 1));
        var larger = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(3, 2));

        await Assert.That(LimitPolynomial<double, int>.MaxMagnitude(smaller, larger)).IsEqualTo(larger);
        await Assert.That(LimitPolynomial<double, int>.MaxMagnitudeNumber(smaller, larger)).IsEqualTo(larger);
    }

    /// <summary>
    /// Verifies: should choose smaller magnitude when min magnitude is requested.
    /// </summary>
    [Test]
    public async Task Should_choose_smaller_magnitude_when_min_magnitude_is_requested()
    {
        var smaller = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 1));
        var larger = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(3, 2));

        await Assert.That(LimitPolynomial<double, int>.MinMagnitude(smaller, larger)).IsEqualTo(smaller);
        await Assert.That(LimitPolynomial<double, int>.MinMagnitudeNumber(smaller, larger)).IsEqualTo(smaller);
    }

    /// <summary>
    /// Verifies: should break equal magnitude ties by ordinary comparison.
    /// </summary>
    [Test]
    public async Task Should_break_equal_magnitude_ties_by_ordinary_comparison()
    {
        var negative = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(-5, 0));
        var positive = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(5, 0));

        await Assert.That(LimitPolynomial<double, int>.MaxMagnitude(negative, positive)).IsEqualTo(positive);
        await Assert.That(LimitPolynomial<double, int>.MinMagnitude(negative, positive)).IsEqualTo(negative);
    }
}