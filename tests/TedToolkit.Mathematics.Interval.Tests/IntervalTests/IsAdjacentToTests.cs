// -----------------------------------------------------------------------
// <copyright file="IsAdjacentToTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalTests;

internal sealed class IsAdjacentToTests
{
    /// <summary>
    /// Verifies: should return true when intervals touch at closed boundary.
    /// </summary>
    [Test]
    public async Task Should_return_true_when_intervals_touch_at_closed_boundary()
    {
        var left = Interval<int>.Create(1, 2, out _);
        var right = Interval<int>.Create(2, 3, out _);

        await Assert.That(left.IsAdjacentTo(right)).IsTrue();
        await Assert.That(right.IsAdjacentTo(left)).IsTrue();
    }

    /// <summary>
    /// Verifies: should return false when intervals only touch at open boundaries.
    /// </summary>
    [Test]
    public async Task Should_return_false_when_intervals_only_touch_at_open_boundaries()
    {
        var left = Interval<int>.Create(
            new IntervalEnd<int>(1, BoundaryType.CLOSED),
            new IntervalEnd<int>(2, BoundaryType.OPEN),
            out _);
        var right = Interval<int>.Create(
            new IntervalEnd<int>(2, BoundaryType.OPEN),
            new IntervalEnd<int>(3, BoundaryType.CLOSED),
            out _);

        await Assert.That(left.IsAdjacentTo(right)).IsFalse();
    }
}