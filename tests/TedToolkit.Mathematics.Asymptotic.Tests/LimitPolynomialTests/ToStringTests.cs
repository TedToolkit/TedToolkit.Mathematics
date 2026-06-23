// -----------------------------------------------------------------------
// <copyright file="ToStringTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class ToStringTests
{
    /// <summary>
    /// Verifies: should format polynomial with default limit prefix when to string is called.
    /// </summary>
    [Test]
    public async Task Should_format_polynomial_with_default_limit_prefix_when_to_string_is_called()
    {
        var polynomial = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(2, 3),
            new PolynomialTerm<double, int>(-4, 1),
            new PolynomialTerm<double, int>(6, 0));

        await Assert.That(polynomial.ToString()).IsEqualTo("(2\u03C9³ - 4\u03C9 + 6)");
    }

    /// <summary>
    /// Verifies: should use same format for coefficient and exponent when format overload is called.
    /// </summary>
    [Test]
    public async Task Should_use_same_format_for_coefficient_and_exponent_when_format_overload_is_called()
    {
        var polynomial = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(12.5, 2),
            new PolynomialTerm<double, int>(-3.25, -1));

        await Assert.That(polynomial.ToString("0.0", null)).IsEqualTo("(12.5\u03C9² - 3.3\u03C9⁻¹)");
    }

    /// <summary>
    /// Verifies: should support distinct coefficient and exponent formats when full overload is called.
    /// </summary>
    [Test]
    public async Task Should_support_distinct_coefficient_and_exponent_formats_when_full_overload_is_called()
    {
        var polynomial = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(12.5, 12),
            new PolynomialTerm<double, int>(-3.25, -2));

        await Assert.That(polynomial.ToString("0.00", "00", null)).IsEqualTo("(12.50\u03C9¹² - 3.25\u03C9⁻⁰²)");
    }

    /// <summary>
    /// Verifies: should truncate output when max terms limit is reached.
    /// </summary>
    [Test]
    public async Task Should_truncate_output_when_max_terms_limit_is_reached()
    {
        var polynomial = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(5, 4),
            new PolynomialTerm<double, int>(4, 3),
            new PolynomialTerm<double, int>(3, 2),
            new PolynomialTerm<double, int>(2, 1));

        await Assert.That(polynomial.ToString(null, null, null, 3)).IsEqualTo("(5\u03C9⁴ + 4\u03C9³ + ...)");
    }

    /// <summary>
    /// Verifies: should omit parentheses when polynomial has only one non-zero term.
    /// </summary>
    [Test]
    public async Task Should_omit_parentheses_when_polynomial_has_only_one_non_zero_term()
    {
        var polynomial = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 3));

        await Assert.That(polynomial.ToString()).IsEqualTo("2\u03C9³");
    }
}