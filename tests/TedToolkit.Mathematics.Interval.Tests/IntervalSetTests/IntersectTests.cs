// -----------------------------------------------------------------------
// <copyright file="IntersectTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalSetTests;

internal sealed class IntersectTests
{
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
}