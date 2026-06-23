// -----------------------------------------------------------------------
// <copyright file="MultiplyOperatorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class MultiplyOperatorTests
{
    /// <summary>
    /// Verifies: should perform polynomial convolution when polynomials are multiplied.
    /// </summary>
    [Test]
    public async Task Should_perform_polynomial_convolution_when_polynomials_are_multiplied()
    {
        var left = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(2, 1),
            new PolynomialTerm<double, int>(3, 0));
        var right = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(4, 2),
            new PolynomialTerm<double, int>(5, 0));

        var result = left * right;

        await Assert.That(result).IsEqualTo(
            new LimitPolynomial<double, int>(
                new PolynomialTerm<double, int>(8, 3),
                new PolynomialTerm<double, int>(12, 2),
                new PolynomialTerm<double, int>(10, 1),
                new PolynomialTerm<double, int>(15, 0)));
    }
}