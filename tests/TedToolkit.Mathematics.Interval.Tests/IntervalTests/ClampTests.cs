// -----------------------------------------------------------------------
// <copyright file="ClampTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalTests;

internal sealed class ClampTests
{
    [Test]
    public async Task Should_return_original_value_when_value_is_inside_interval()
    {
        var interval = Interval<int>.Create(1, 3, out _);

        await Assert.That(interval.Clamp(2)).IsEqualTo(2);
    }

    [Test]
    public async Task Should_return_nearest_endpoint_when_value_is_outside_interval()
    {
        var interval = Interval<int>.Create(1, 3, out _);

        await Assert.That(interval.Clamp(0)).IsEqualTo(1);
        await Assert.That(interval.Clamp(4)).IsEqualTo(3);
    }

    [Test]
    public async Task Should_throw_when_interval_is_empty()
    {
        var interval = Interval<int>.Create(
            new IntervalEnd<int>(1, BoundaryType.OPEN),
            new IntervalEnd<int>(1, BoundaryType.OPEN),
            out _);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
        {
            interval.Clamp(1);
            return Task.CompletedTask;
        });
    }
}