// -----------------------------------------------------------------------
// <copyright file="ConstructorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalSetTests;

internal sealed class ConstructorTests
{
    [Test]
    public async Task Should_drop_empty_intervals_when_set_is_created()
    {
        var empty = Interval<int>.Create(
            new IntervalEnd<int>(1, BoundaryType.OPEN),
            new IntervalEnd<int>(1, BoundaryType.OPEN),
            out _);
        var nonEmpty = Interval<int>.Create(2, 3, out _);

        var set = new IntervalSet<int>(empty, nonEmpty);

        await Assert.That(set.Intervals.Count).IsEqualTo(1);
        await Assert.That(set.Intervals[0]).IsEqualTo(nonEmpty);
    }

    [Test]
    public async Task Should_merge_overlapping_and_adjacent_intervals_when_set_is_normalized()
    {
        var first = Interval<int>.Create(1, 2, out _);
        var second = Interval<int>.Create(2, 4, out _);
        var third = Interval<int>.Create(6, 7, out _);

        var set = new IntervalSet<int>(third, second, first);

        await Assert.That(set.Intervals.Count).IsEqualTo(2);
        await Assert.That(set.Intervals[0]).IsEqualTo(Interval<int>.Create(1, 4, out _));
        await Assert.That(set.Intervals[1]).IsEqualTo(third);
    }

    [Test]
    public async Task Should_create_single_interval_set_when_constructor_receives_one_interval()
    {
        var interval = Interval<int>.Create(1, 2, out _);

        var set = new IntervalSet<int>(interval);

        await Assert.That(set.Intervals.Count).IsEqualTo(1);
        await Assert.That(set.Intervals[0]).IsEqualTo(interval);
    }
}