// -----------------------------------------------------------------------
// <copyright file="GetRelationToTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalTests;

internal sealed class GetRelationToTests
{
    [Test]
    public async Task Should_return_equal_when_intervals_have_same_membership()
    {
        var left = Interval<int>.Create(1, 3, out _);
        var right = Interval<int>.Create(1, 3, out _);

        await Assert.That(left.GetRelationTo(right)).IsEqualTo(RelationType.Equal);
    }

    [Test]
    public async Task Should_return_proper_subset_when_left_is_strictly_contained()
    {
        var left = Interval<int>.Create(
            new IntervalEnd<int>(1, BoundaryType.OPEN),
            new IntervalEnd<int>(3, BoundaryType.OPEN),
            out _);
        var right = Interval<int>.Create(1, 3, out _);

        await Assert.That(left.GetRelationTo(right)).IsEqualTo(RelationType.ProperSubset);
    }

    [Test]
    public async Task Should_return_proper_superset_when_right_is_empty()
    {
        var left = Interval<int>.Create(1, 3, out _);
        var right = Interval<int>.Create(
            new IntervalEnd<int>(2, BoundaryType.OPEN),
            new IntervalEnd<int>(2, BoundaryType.OPEN),
            out _);

        await Assert.That(left.GetRelationTo(right)).IsEqualTo(RelationType.ProperSuperset);
    }

    [Test]
    public async Task Should_return_disjoint_when_intervals_do_not_share_any_value()
    {
        var left = Interval<int>.Create(
            new IntervalEnd<int>(1, BoundaryType.CLOSED),
            new IntervalEnd<int>(2, BoundaryType.OPEN),
            out _);
        var right = Interval<int>.Create(
            new IntervalEnd<int>(2, BoundaryType.OPEN),
            new IntervalEnd<int>(4, BoundaryType.CLOSED),
            out _);

        await Assert.That(left.GetRelationTo(right)).IsEqualTo(RelationType.Disjoint);
    }

    [Test]
    public async Task Should_return_overlapping_when_intervals_share_only_part_of_their_values()
    {
        var left = Interval<int>.Create(1, 3, out _);
        var right = Interval<int>.Create(2, 4, out _);

        await Assert.That(left.GetRelationTo(right)).IsEqualTo(RelationType.Overlapping);
    }
}