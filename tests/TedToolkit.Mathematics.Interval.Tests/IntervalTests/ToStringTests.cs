// -----------------------------------------------------------------------
// <copyright file="ToStringTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalTests;

internal sealed class ToStringTests
{
    /// <summary>
    /// Verifies: should return empty symbol when interval is empty.
    /// </summary>
    [Test]
    public async Task Should_return_empty_symbol_when_interval_is_empty()
    {
        var interval = Interval<int>.Create(
            new IntervalEnd<int>(1, BoundaryType.OPEN),
            new IntervalEnd<int>(1, BoundaryType.OPEN),
            out _);

        await Assert.That(interval.ToString()).IsEqualTo("Ø");
    }

    /// <summary>
    /// Verifies: should return scalar value when interval is singleton.
    /// </summary>
    [Test]
    public async Task Should_return_scalar_value_when_interval_is_singleton()
    {
        var interval = Interval<int>.Create(1, 1, out _);

        await Assert.That(interval.ToString()).IsEqualTo("1");
    }

    /// <summary>
    /// Verifies: should return mathematical interval notation when interval has range.
    /// </summary>
    [Test]
    public async Task Should_return_mathematical_interval_notation_when_interval_has_range()
    {
        var interval = Interval<int>.Create(
            new IntervalEnd<int>(1, BoundaryType.OPEN),
            new IntervalEnd<int>(3, BoundaryType.CLOSED),
            out _);

        await Assert.That(interval.ToString()).IsEqualTo("(1, 3]");
    }
}