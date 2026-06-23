// -----------------------------------------------------------------------
// <copyright file="ToStringTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitResultTests;

internal sealed class ToStringTests
{
    /// <summary>
    /// Verifies: should format finite value when state is finite.
    /// </summary>
    [Test]
    public async Task Should_format_finite_value_when_state_is_finite()
    {
        var result = new LimitResult<double>(LimitState.FINITE, 12.5);

        await Assert.That(result.ToString("0.0", null)).IsEqualTo("12.5");
        await Assert.That(result.ToString()).IsEqualTo("12.5");
    }

    /// <summary>
    /// Verifies: should format positive infinity symbol when state is positive infinity.
    /// </summary>
    [Test]
    public async Task Should_format_positive_infinity_symbol_when_state_is_positive_infinity()
    {
        var result = new LimitResult<double>(LimitState.POSITIVE_INFINITY, 0);

        await Assert.That(result.ToString()).IsEqualTo("+∞");
    }

    /// <summary>
    /// Verifies: should format negative infinity symbol when state is negative infinity.
    /// </summary>
    [Test]
    public async Task Should_format_negative_infinity_symbol_when_state_is_negative_infinity()
    {
        var result = new LimitResult<double>(LimitState.NEGATIVE_INFINITY, 0);

        await Assert.That(result.ToString()).IsEqualTo("-∞");
    }
}