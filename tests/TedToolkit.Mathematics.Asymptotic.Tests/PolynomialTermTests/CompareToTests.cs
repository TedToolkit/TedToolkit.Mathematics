// -----------------------------------------------------------------------
// <copyright file="CompareToTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.PolynomialTermTests;

internal sealed class CompareToTests
{
    /// <summary>
    /// Verifies: should order by sign before degree when terms have different signs.
    /// </summary>
    [Test]
    public async Task Should_order_by_sign_before_degree_when_terms_have_different_signs()
    {
        var negative = new PolynomialTerm<double, int>(-1, 100);
        var positive = new PolynomialTerm<double, int>(1, -100);

        await Assert.That(negative.CompareTo(positive)).IsLessThan(0);
        await Assert.That(positive.CompareTo(negative)).IsGreaterThan(0);
    }

    /// <summary>
    /// Verifies: should order positive terms by exponent when signs match.
    /// </summary>
    [Test]
    public async Task Should_order_positive_terms_by_exponent_when_signs_match()
    {
        var lower = new PolynomialTerm<double, int>(2, 1);
        var higher = new PolynomialTerm<double, int>(2, 3);

        await Assert.That(lower.CompareTo(higher)).IsLessThan(0);
        await Assert.That(higher.CompareTo(lower)).IsGreaterThan(0);
    }

    /// <summary>
    /// Verifies: should order negative terms by reverse exponent when signs match.
    /// </summary>
    [Test]
    public async Task Should_order_negative_terms_by_reverse_exponent_when_signs_match()
    {
        var lower = new PolynomialTerm<double, int>(-2, 1);
        var higher = new PolynomialTerm<double, int>(-2, 3);

        await Assert.That(lower.CompareTo(higher)).IsGreaterThan(0);
        await Assert.That(higher.CompareTo(lower)).IsLessThan(0);
    }
}