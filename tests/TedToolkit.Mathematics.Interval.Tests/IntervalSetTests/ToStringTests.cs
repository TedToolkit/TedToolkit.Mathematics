// -----------------------------------------------------------------------
// <copyright file="ToStringTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalSetTests;

internal sealed class ToStringTests
{
    [Test]
    public async Task Should_return_empty_symbol_when_set_is_empty()
    {
        var empty = Interval<int>.Create(
            new IntervalEnd<int>(1, BoundaryType.OPEN),
            new IntervalEnd<int>(1, BoundaryType.OPEN),
            out _);

        var set = new IntervalSet<int>([empty,]);

        await Assert.That(set.ToString()).IsEqualTo("Ø");
    }

    [Test]
    public async Task Should_join_intervals_with_union_symbol_when_set_contains_multiple_intervals()
    {
        var first = Interval<int>.Create(1, 2, out _);
        var second = Interval<int>.Create(4, 5, out _);

        var set = new IntervalSet<int>(first, second);

        await Assert.That(set.ToString()).IsEqualTo("[1, 2] ∪ [4, 5]");
    }
}