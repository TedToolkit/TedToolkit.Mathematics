// -----------------------------------------------------------------------
// <copyright file="ToIntervalSetTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalTests;

internal sealed class ToIntervalSetTests
{
    [Test]
    public async Task Should_wrap_interval_in_single_element_set_when_interval_is_converted()
    {
        var interval = Interval<int>.Create(1, 3, out _);

        var set = interval.ToIntervalSet();

        await Assert.That(set.Intervals.Count).IsEqualTo(1);
        await Assert.That(set.Intervals[0]).IsEqualTo(interval);
    }
}