// -----------------------------------------------------------------------
// <copyright file="DivisionOperatorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class DivisionOperatorTests
{
    [Test]
    public async Task Should_compute_quotient_when_polynomials_are_divided()
    {
        var dividend = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(1, 2), new PolynomialTerm<double, int>(3, 1), new PolynomialTerm<double, int>(2, 0));
        var divisor = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(1, 1), new PolynomialTerm<double, int>(1, 0));

        await Assert.That(dividend / divisor).IsEqualTo(new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(1, 1), new PolynomialTerm<double, int>(2, 0)));
    }

    [Test]
    public async Task Should_throw_when_divisor_is_zero()
    {
        var dividend = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(1, 0));

        await Assert.ThrowsAsync<DivideByZeroException>(() =>
        {
            _ = dividend / LimitPolynomial<double, int>.Zero;
            return Task.CompletedTask;
        });
    }
}