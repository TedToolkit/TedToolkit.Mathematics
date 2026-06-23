// -----------------------------------------------------------------------
// <copyright file="AbsTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class AbsTests
{
    /// <summary>
    /// Verifies: should return positive equivalent when polynomial is negative.
    /// </summary>
    [Test]
    public async Task Should_return_positive_equivalent_when_polynomial_is_negative()
    {
        var value = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(-2, 3));

        await Assert.That(LimitPolynomial<double, int>.Abs(value))
            .IsEqualTo(new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 3)));
    }
}