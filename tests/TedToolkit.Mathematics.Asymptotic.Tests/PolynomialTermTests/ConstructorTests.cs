// -----------------------------------------------------------------------
// <copyright file="ConstructorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.PolynomialTermTests;

internal sealed class ConstructorTests
{
    /// <summary>
    /// Verifies: should create constant term when only coefficient is provided.
    /// </summary>
    [Test]
    public async Task Should_create_constant_term_when_only_coefficient_is_provided()
    {
        var term = new PolynomialTerm<double, int>(3);

        await Assert.That(term.Coefficient).IsEqualTo(3);
        await Assert.That(term.Exponent).IsEqualTo(0);
    }

    /// <summary>
    /// Verifies: should normalize exponent to zero when coefficient is zero.
    /// </summary>
    [Test]
    public async Task Should_normalize_exponent_to_zero_when_coefficient_is_zero()
    {
        var term = new PolynomialTerm<double, int>(0, 5);

        await Assert.That(term.Exponent).IsEqualTo(0);
    }

    /// <summary>
    /// Verifies: should throw when coefficient is nan.
    /// </summary>
    [Test]
    public async Task Should_throw_when_coefficient_is_nan()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
        {
            var _ = new PolynomialTerm<double, int>(double.NaN, 1);
            return Task.CompletedTask;
        });
    }

    /// <summary>
    /// Verifies: should throw when coefficient is infinity.
    /// </summary>
    [Test]
    public async Task Should_throw_when_coefficient_is_infinity()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
        {
            var _ = new PolynomialTerm<double, int>(double.PositiveInfinity, 1);
            return Task.CompletedTask;
        });
    }
}