// -----------------------------------------------------------------------
// <copyright file="IsInsideTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalTests;

internal sealed class IsInsideTests
{
    /// <summary>
    /// Verifies: should include closed boundaries when value matches endpoint.
    /// </summary>
    [Test]
    public async Task Should_include_closed_boundaries_when_value_matches_endpoint()
    {
        var interval = Interval<int>.Create(1, 3, out _);

        await Assert.That(interval.IsInside(1)).IsTrue();
        await Assert.That(interval.IsInside(3)).IsTrue();
    }

    /// <summary>
    /// Verifies: should exclude open boundaries when value matches endpoint.
    /// </summary>
    [Test]
    public async Task Should_exclude_open_boundaries_when_value_matches_endpoint()
    {
        var interval = Interval<int>.Create(
            new IntervalEnd<int>(1, BoundaryType.OPEN),
            new IntervalEnd<int>(3, BoundaryType.OPEN),
            out _);

        await Assert.That(interval.IsInside(1)).IsFalse();
        await Assert.That(interval.IsInside(3)).IsFalse();
        await Assert.That(interval.IsInside(2)).IsTrue();
    }
}