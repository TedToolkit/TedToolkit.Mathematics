// -----------------------------------------------------------------------
// <copyright file="CompareGrowthRateToTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class CompareGrowthRateToTests
{
    /// <summary>
    /// Verifies: should compare leading terms when growth rate is compared.
    /// </summary>
    [Test]
    public async Task Should_compare_leading_terms_when_growth_rate_is_compared()
    {
        var left = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 3));
        var right = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(1, 2));

        await Assert.That(left.CompareGrowthRateTo(right)).IsGreaterThan(0);
        await Assert.That(right.CompareGrowthRateTo(left)).IsLessThan(0);
    }
}