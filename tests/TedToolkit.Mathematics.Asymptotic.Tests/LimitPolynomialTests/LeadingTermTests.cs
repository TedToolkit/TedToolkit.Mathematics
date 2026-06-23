// -----------------------------------------------------------------------
// <copyright file="LeadingTermTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class LeadingTermTests
{
    /// <summary>
    /// Verifies: should return highest degree non zero term when leading term is requested.
    /// </summary>
    [Test]
    public async Task Should_return_highest_degree_non_zero_term_when_leading_term_is_requested()
    {
        var polynomial = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(1, 0),
            new PolynomialTerm<double, int>(2, 3),
            new PolynomialTerm<double, int>(4, 1));

        await Assert.That(polynomial.LeadingTerm).IsEqualTo(new PolynomialTerm<double, int>(2, 3));
    }
}