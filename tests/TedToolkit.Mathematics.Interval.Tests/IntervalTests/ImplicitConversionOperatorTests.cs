// -----------------------------------------------------------------------
// <copyright file="ImplicitConversionOperatorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalTests;

internal sealed class ImplicitConversionOperatorTests
{
    [Test]
    public async Task Should_convert_interval_to_single_element_set_when_interval_is_assigned_to_set()
    {
        var interval = Interval<int>.Create(1, 3, out _);

        IntervalSet<int> set = interval;

        await Assert.That(set.Intervals.Count).IsEqualTo(1);
        await Assert.That(set.Intervals[0]).IsEqualTo(interval);
    }
}