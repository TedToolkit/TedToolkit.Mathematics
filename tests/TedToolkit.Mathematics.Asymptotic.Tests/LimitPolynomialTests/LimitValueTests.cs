// -----------------------------------------------------------------------
// <copyright file="LimitValueTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class LimitValueTests
{
    /// <summary>
    /// Verifies: should forward limit value of leading term when limit value is requested.
    /// </summary>
    [Test]
    public async Task Should_forward_limit_value_of_leading_term_when_limit_value_is_requested()
    {
        var polynomial = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(-2, 3), new PolynomialTerm<double, int>(5, 0));

        await Assert.That(polynomial.LimitValue)
            .IsEqualTo(new LimitResult<double>(LimitState.NEGATIVE_INFINITY, 0));
    }
}