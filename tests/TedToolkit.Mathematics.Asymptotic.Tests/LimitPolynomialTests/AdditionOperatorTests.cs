// -----------------------------------------------------------------------
// <copyright file="AdditionOperatorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class AdditionOperatorTests
{
    [Test]
    public async Task Should_combine_like_terms_when_polynomials_are_added()
    {
        var left = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 3), new PolynomialTerm<double, int>(1, 0));
        var right = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(5, 3), new PolynomialTerm<double, int>(4, 1));

        var result = left + right;

        await Assert.That(result).IsEqualTo(new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(7, 3), new PolynomialTerm<double, int>(4, 1), new PolynomialTerm<double, int>(1, 0)));
    }

    [Test]
    public async Task Should_preserve_polynomial_when_unary_plus_is_applied()
    {
        var value = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 3));

        await Assert.That(+value).IsEqualTo(value);
    }
}