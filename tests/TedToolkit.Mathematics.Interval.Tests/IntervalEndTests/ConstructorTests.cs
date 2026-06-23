// -----------------------------------------------------------------------
// <copyright file="ConstructorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalEndTests;

internal sealed class ConstructorTests
{
    /// <summary>
    /// Verifies: should create closed endpoint when boundary type is omitted.
    /// </summary>
    [Test]
    public async Task Should_create_closed_endpoint_when_boundary_type_is_omitted()
    {
        var endpoint = new IntervalEnd<int>(3);

        await Assert.That(endpoint.Value).IsEqualTo(3);
        await Assert.That(endpoint.Type).IsEqualTo(BoundaryType.CLOSED);
    }

    /// <summary>
    /// Verifies: should create endpoint with specified boundary type when boundary type is provided.
    /// </summary>
    [Test]
    public async Task Should_create_endpoint_with_specified_boundary_type_when_boundary_type_is_provided()
    {
        var endpoint = new IntervalEnd<int>(3, BoundaryType.OPEN);

        await Assert.That(endpoint.Value).IsEqualTo(3);
        await Assert.That(endpoint.Type).IsEqualTo(BoundaryType.OPEN);
    }
}