// -----------------------------------------------------------------------
// <copyright file="DecrementOperatorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class DecrementOperatorTests
{
    /// <summary>
    /// Verifies: should subtract one from constant term when decrement operator is used.
    /// </summary>
    [Test]
    public async Task Should_subtract_one_from_constant_term_when_decrement_operator_is_used()
    {
        var value = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(2, 3),
            new PolynomialTerm<double, int>(6, 0));

        var result = --value;

        await Assert.That(result).IsEqualTo(
            new LimitPolynomial<double, int>(
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(5, 0)));
    }
}