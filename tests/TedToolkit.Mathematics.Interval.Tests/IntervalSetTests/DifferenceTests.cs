// -----------------------------------------------------------------------
// <copyright file="DifferenceTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalSetTests;

internal sealed class DifferenceTests
{
    [Test]
    public async Task Should_return_original_set_when_other_set_is_empty()
    {
        var current = new IntervalSet<int>(Interval<int>.Create(1, 4, out _));
        var empty = new IntervalSet<int>([
            Interval<int>.Create(
                new IntervalEnd<int>(1, BoundaryType.OPEN),
                new IntervalEnd<int>(1, BoundaryType.OPEN),
                out _),
        ]);

        var result = current.Difference(empty);

        await Assert.That(result).IsEqualTo(current);
    }

    [Test]
    public async Task Should_split_interval_when_middle_segment_is_subtracted()
    {
        var current = new IntervalSet<int>(Interval<int>.Create(1, 6, out _));
        var other = new IntervalSet<int>(Interval<int>.Create(3, 4, out _));

        var result = current.Difference(other);

        await Assert.That(result.Intervals.Count).IsEqualTo(2);
        await Assert.That(result.Intervals[0]).IsEqualTo(Interval<int>.Create(
            new IntervalEnd<int>(1, BoundaryType.CLOSED),
            new IntervalEnd<int>(3, BoundaryType.OPEN),
            out _));
        await Assert.That(result.Intervals[1]).IsEqualTo(Interval<int>.Create(
            new IntervalEnd<int>(4, BoundaryType.OPEN),
            new IntervalEnd<int>(6, BoundaryType.CLOSED),
            out _));
    }
}