// -----------------------------------------------------------------------
// <copyright file="ToStringTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class ToStringTests
{
    [Test]
    public async Task Should_format_polynomial_with_default_limit_prefix_when_to_string_is_called()
    {
        var polynomial = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 3), new PolynomialTerm<double, int>(-4, 1), new PolynomialTerm<double, int>(6, 0));

        await Assert.That(polynomial.ToString()).IsEqualTo("lim[x→∞] (2lim[x→∞] x³ - 4lim[x→∞] x + 6)");
    }

    [Test]
    public async Task Should_use_same_format_for_coefficient_and_exponent_when_format_overload_is_called()
    {
        var polynomial = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(12.5, 2), new PolynomialTerm<double, int>(-3.25, -1));

        await Assert.That(polynomial.ToString("0.0", null)).IsEqualTo("lim[x→∞] (12.5lim[x→∞] x² - 3.3lim[x→∞] x⁻¹)");
    }
}