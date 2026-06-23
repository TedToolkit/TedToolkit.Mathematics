// -----------------------------------------------------------------------
// <copyright file="IsEmptyTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalTests;

internal sealed class IsEmptyTests
{
    [Test]
    public async Task Should_return_true_when_equal_endpoints_have_an_open_boundary()
    {
        var interval = Interval<int>.Create(
            new IntervalEnd<int>(2, BoundaryType.OPEN),
            new IntervalEnd<int>(2, BoundaryType.CLOSED),
            out _);

        await Assert.That(interval.IsEmpty).IsTrue();
    }

    [Test]
    public async Task Should_return_false_when_equal_endpoints_are_both_closed()
    {
        var interval = Interval<int>.Create(2, 2, out _);

        await Assert.That(interval.IsEmpty).IsFalse();
    }
}