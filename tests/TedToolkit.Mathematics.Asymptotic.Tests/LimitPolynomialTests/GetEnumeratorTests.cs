// -----------------------------------------------------------------------
// <copyright file="GetEnumeratorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class GetEnumeratorTests
{
    /// <summary>
    /// Verifies: should enumerate normalized terms in descending exponent order when foreach is used.
    /// </summary>
    [Test]
    public async Task Should_enumerate_normalized_terms_in_descending_exponent_order_when_foreach_is_used()
    {
        var polynomial = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(3, -2),
            new PolynomialTerm<double, int>(4, 3),
            new PolynomialTerm<double, int>(5, 0),
            new PolynomialTerm<double, int>(-1, 3));

        var terms = new List<PolynomialTerm<double, int>>();
        terms.AddRange(polynomial);

        await Assert.That(terms.Count).IsEqualTo(3);
        await Assert.That(terms[0]).IsEqualTo(new PolynomialTerm<double, int>(3, 3));
        await Assert.That(terms[1]).IsEqualTo(new PolynomialTerm<double, int>(5, 0));
        await Assert.That(terms[2]).IsEqualTo(new PolynomialTerm<double, int>(3, -2));
    }
}