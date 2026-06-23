// -----------------------------------------------------------------------
// <copyright file="UnionTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalSetTests;

internal sealed class UnionTests
{
    [Test]
    public async Task Should_return_other_set_when_current_set_is_empty()
    {
        var empty = new IntervalSet<int>([
            Interval<int>.Create(
                new IntervalEnd<int>(1, BoundaryType.OPEN),
                new IntervalEnd<int>(1, BoundaryType.OPEN),
                out _),
        ]);
        var other = new IntervalSet<int>(Interval<int>.Create(2, 3, out _));

        var result = empty.Union(other);

        await Assert.That(result).IsEqualTo(other);
    }

    [Test]
    public async Task Should_merge_intervals_from_both_sets_when_union_is_taken()
    {
        var left = new IntervalSet<int>(Interval<int>.Create(1, 2, out _), Interval<int>.Create(5, 6, out _));
        var right = new IntervalSet<int>([
            Interval<int>.Create(2, 4, out _),
        ]);

        var result = left.Union(right);

        await Assert.That(result.Intervals.Count).IsEqualTo(2);
        await Assert.That(result.Intervals[0]).IsEqualTo(Interval<int>.Create(1, 4, out _));
        await Assert.That(result.Intervals[1]).IsEqualTo(Interval<int>.Create(5, 6, out _));
    }
}