// -----------------------------------------------------------------------
// <copyright file="ConstructorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class ConstructorTests
{
    /// <summary>
    /// Verifies: should combine like terms and remove zero sums when polynomial is created.
    /// </summary>
    [Test]
    public async Task Should_combine_like_terms_and_remove_zero_sums_when_polynomial_is_created()
    {
        var polynomial = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(2, 3),
            new PolynomialTerm<double, int>(-2, 3),
            new PolynomialTerm<double, int>(4, 1),
            new PolynomialTerm<double, int>(3, 1));

        await Assert.That(polynomial.Count).IsEqualTo(1);
        await Assert.That(polynomial.LeadingTerm).IsEqualTo(new PolynomialTerm<double, int>(7, 1));
    }

    /// <summary>
    /// Verifies: should fallback to zero term when all terms cancel out.
    /// </summary>
    [Test]
    public async Task Should_fallback_to_zero_term_when_all_terms_cancel_out()
    {
        var polynomial = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 1), new PolynomialTerm<double, int>(-2, 1));

        await Assert.That(polynomial).IsEqualTo(LimitPolynomial<double, int>.Zero);
    }
}