// -----------------------------------------------------------------------
// <copyright file="PropertyTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitResultTests;

internal sealed class PropertyTests
{
    /// <summary>
    /// Verifies: should expose constructor values when state and value properties are read.
    /// </summary>
    [Test]
    [Arguments(LimitState.FINITE, 12.5)]
    [Arguments(LimitState.POSITIVE_INFINITY, 0d)]
    [Arguments(LimitState.NEGATIVE_INFINITESIMAL, -3.25)]
    public async Task Should_expose_constructor_values_when_state_and_value_properties_are_read(
        LimitState state,
        double value)
    {
        var result = new LimitResult<double>(state, value);

        await Assert.That(result.State).IsEqualTo(state);
        await Assert.That(result.Value).IsEqualTo(value);
    }
}