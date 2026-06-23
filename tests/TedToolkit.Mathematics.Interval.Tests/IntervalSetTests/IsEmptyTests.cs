// -----------------------------------------------------------------------
// <copyright file="IsEmptyTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalSetTests;

internal sealed class IsEmptyTests
{
    /// <summary>
    /// Verifies: should return true when no non empty intervals remain after normalization.
    /// </summary>
    [Test]
    public async Task Should_return_true_when_no_non_empty_intervals_remain_after_normalization()
    {
        var empty = Interval<int>.Create(
            new IntervalEnd<int>(1, BoundaryType.OPEN),
            new IntervalEnd<int>(1, BoundaryType.OPEN),
            out _);

        var set = new IntervalSet<int>([empty,]);

        await Assert.That(set.IsEmpty).IsTrue();
    }

    /// <summary>
    /// Verifies: should return false when set contains any interval.
    /// </summary>
    [Test]
    public async Task Should_return_false_when_set_contains_any_interval()
    {
        var set = new IntervalSet<int>(Interval<int>.Create(1, 2, out _));

        await Assert.That(set.IsEmpty).IsFalse();
    }
}