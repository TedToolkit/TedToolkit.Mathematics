// -----------------------------------------------------------------------
// <copyright file="ModuloOperatorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class ModuloOperatorTests
{
    /// <summary>
    /// Verifies: should return remainder when polynomials are divided.
    /// </summary>
    [Test]
    public async Task Should_return_remainder_when_polynomials_are_divided()
    {
        var dividend = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(1, 2), new PolynomialTerm<double, int>(1, 0));
        var divisor = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(1, 1), new PolynomialTerm<double, int>(1, 0));

        await Assert.That(dividend % divisor).IsEqualTo(new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 0)));
    }
}