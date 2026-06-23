// -----------------------------------------------------------------------
// <copyright file="CreateTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalTests;

internal sealed class CreateTests
{
    [Test]
    public async Task Should_preserve_endpoint_order_when_start_is_less_than_end()
    {
        var interval = Interval<int>.Create(1, 3, out var isReversed);

        await Assert.That(isReversed).IsFalse();
        await Assert.That(interval.Min).IsEqualTo(new IntervalEnd<int>(1));
        await Assert.That(interval.Max).IsEqualTo(new IntervalEnd<int>(3));
    }

    [Test]
    public async Task Should_swap_endpoints_when_start_is_greater_than_end()
    {
        var interval = Interval<int>.Create(3, 1, out var isReversed);

        await Assert.That(isReversed).IsTrue();
        await Assert.That(interval.Min).IsEqualTo(new IntervalEnd<int>(1));
        await Assert.That(interval.Max).IsEqualTo(new IntervalEnd<int>(3));
    }
}