// -----------------------------------------------------------------------
// <copyright file="TermsTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class TermsTests
{
    /// <summary>
    /// Verifies: should expose terms in descending exponent order when terms are enumerated.
    /// </summary>
    [Test]
    public async Task Should_expose_terms_in_descending_exponent_order_when_terms_are_enumerated()
    {
        var polynomial = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(1, 0),
            new PolynomialTerm<double, int>(2, 3),
            new PolynomialTerm<double, int>(4, 1));

        var terms = polynomial.Terms.ToArray();

        await Assert.That(terms.Length).IsEqualTo(3);
        await Assert.That(terms[0]).IsEqualTo(new PolynomialTerm<double, int>(2, 3));
        await Assert.That(terms[1]).IsEqualTo(new PolynomialTerm<double, int>(4, 1));
        await Assert.That(terms[2]).IsEqualTo(new PolynomialTerm<double, int>(1, 0));
    }
}