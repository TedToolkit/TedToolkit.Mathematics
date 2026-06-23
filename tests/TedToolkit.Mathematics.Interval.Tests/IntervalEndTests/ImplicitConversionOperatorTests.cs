// -----------------------------------------------------------------------
// <copyright file="ImplicitConversionOperatorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.IntervalEndTests;

internal sealed class ImplicitConversionOperatorTests
{
    /// <summary>
    /// Verifies: should convert value to closed endpoint when value is assigned directly.
    /// </summary>
    [Test]
    public async Task Should_convert_value_to_closed_endpoint_when_value_is_assigned_directly()
    {
        IntervalEnd<int> endpoint = 5;

        await Assert.That(endpoint.Value).IsEqualTo(5);
        await Assert.That(endpoint.Type).IsEqualTo(BoundaryType.CLOSED);
    }

    /// <summary>
    /// Verifies: should convert value to closed endpoint when named conversion is called.
    /// </summary>
    [Test]
    public async Task Should_convert_value_to_closed_endpoint_when_named_conversion_is_called()
    {
        var endpoint = IntervalEnd<int>.FromTData(5);

        await Assert.That(endpoint.Value).IsEqualTo(5);
        await Assert.That(endpoint.Type).IsEqualTo(BoundaryType.CLOSED);
    }
}