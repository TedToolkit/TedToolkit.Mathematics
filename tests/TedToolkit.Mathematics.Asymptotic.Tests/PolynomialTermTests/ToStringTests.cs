// -----------------------------------------------------------------------
// <copyright file="ToStringTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.PolynomialTermTests;

internal sealed class ToStringTests
{
    [Test]
    public async Task Should_not_expose_span_based_try_format()
    {
        var tryFormat = typeof(PolynomialTerm<double, int>).GetMethod(
            "TryFormat",
            System.Reflection.BindingFlags.Instance
            | System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.NonPublic
            | System.Reflection.BindingFlags.DeclaredOnly);

        await Assert.That(tryFormat).IsNull();
    }

    [Test]
    public async Task Should_return_coefficient_only_when_term_is_constant()
    {
        var term = new PolynomialTerm<double, int>(6, 0);

        await Assert.That(term.ToString()).IsEqualTo("6");
    }

    [Test]
    public async Task Should_append_limit_variable_when_term_has_non_zero_exponent()
    {
        var term = new PolynomialTerm<double, int>(2, 3);

        await Assert.That(term.ToString()).IsEqualTo("2lim[x→∞] x³");
    }

    [Test]
    public async Task Should_respect_custom_format_when_term_is_formatted()
    {
        var term = new PolynomialTerm<double, int>(12.5, -2);

        await Assert.That(term.ToString("0.0", "0", null)).IsEqualTo("12.5lim[x→∞] x⁻²");
    }
}