// -----------------------------------------------------------------------
// <copyright file="ConstructorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.PolynomialTermTests;

internal sealed class ConstructorTests
{
    [Test]
    public async Task Should_create_constant_term_when_only_coefficient_is_provided()
    {
        var term = new PolynomialTerm<double, int>(3);

        await Assert.That(term.Coefficient).IsEqualTo(3);
        await Assert.That(term.Exponent).IsEqualTo(0);
    }

    [Test]
    public async Task Should_normalize_exponent_to_zero_when_coefficient_is_zero()
    {
        var term = new PolynomialTerm<double, int>(0, 5);

        await Assert.That(term.Exponent).IsEqualTo(0);
    }

    [Test]
    public async Task Should_throw_when_coefficient_is_nan()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
        {
            _ = new(double.NaN, 1);
            return Task.CompletedTask;
        });
    }

    [Test]
    public async Task Should_throw_when_coefficient_is_infinity()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
        {
            _ = new(double.PositiveInfinity, 1);
            return Task.CompletedTask;
        });
    }
}