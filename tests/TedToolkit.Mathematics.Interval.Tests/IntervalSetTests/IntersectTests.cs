// -----------------------------------------------------------------------
// <copyright file="IntersectTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalSetTests;

internal sealed class IntersectTests
{
    /// <summary>
    /// Verifies: should return empty set when either side is empty.
    /// </summary>
    [Test]
    public async Task Should_return_empty_set_when_either_side_is_empty()
    {
        var empty = new IntervalSet<int>([
            Interval<int>.Create(
                new IntervalEnd<int>(1, BoundaryType.OPEN),
                new IntervalEnd<int>(1, BoundaryType.OPEN),
                out _),
        ]);
        var other = new IntervalSet<int>(Interval<int>.Create(2, 3, out _));

        var result = empty.Intersect(other);

        await Assert.That(result.IsEmpty).IsTrue();
    }

    /// <summary>
    /// Verifies: should return shared portions when sets overlap.
    /// </summary>
    [Test]
    public async Task Should_return_shared_portions_when_sets_overlap()
    {
        var left = new IntervalSet<int>(Interval<int>.Create(1, 4, out _), Interval<int>.Create(6, 8, out _));
        var right = new IntervalSet<int>([
            Interval<int>.Create(3, 7, out _),
        ]);

        var result = left.Intersect(right);

        await Assert.That(result.Intervals.Count).IsEqualTo(2);
        await Assert.That(result.Intervals[0]).IsEqualTo(Interval<int>.Create(3, 4, out _));
        await Assert.That(result.Intervals[1]).IsEqualTo(Interval<int>.Create(6, 7, out _));
    }

    /// <summary>
    /// Verifies: should return singleton interval when sets intersect at one closed boundary value.
    /// </summary>
    [Test]
    public async Task Should_return_singleton_interval_when_sets_intersect_at_one_closed_boundary_value()
    {
        var left = new IntervalSet<int>(Interval<int>.Create(
            new IntervalEnd<int>(1, BoundaryType.CLOSED),
            new IntervalEnd<int>(3, BoundaryType.CLOSED),
            out _));
        var right = new IntervalSet<int>(Interval<int>.Create(
            new IntervalEnd<int>(3, BoundaryType.CLOSED),
            new IntervalEnd<int>(5, BoundaryType.CLOSED),
            out _));

        var result = left.Intersect(right);

        await Assert.That(result.Intervals.Count).IsEqualTo(1);
        await Assert.That(result.Intervals[0]).IsEqualTo(Interval<int>.Create(3, 3, out _));
    }

    /// <summary>
    /// Verifies: should return empty set when intervals only touch at open boundary value.
    /// </summary>
    [Test]
    public async Task Should_return_empty_set_when_intervals_only_touch_at_open_boundary_value()
    {
        var left = new IntervalSet<int>(Interval<int>.Create(
            new IntervalEnd<int>(1, BoundaryType.CLOSED),
            new IntervalEnd<int>(3, BoundaryType.OPEN),
            out _));
        var right = new IntervalSet<int>(Interval<int>.Create(
            new IntervalEnd<int>(3, BoundaryType.OPEN),
            new IntervalEnd<int>(5, BoundaryType.CLOSED),
            out _));

        var result = left.Intersect(right);

        await Assert.That(result.IsEmpty).IsTrue();
    }
}