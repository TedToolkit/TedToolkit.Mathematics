// -----------------------------------------------------------------------
// <copyright file="SwapTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.RelationTypeTests;

internal sealed class SwapTests
{
    /// <summary>
    /// Verifies: should swap subset and superset when relation is directional.
    /// </summary>
    [Test]
    public async Task Should_swap_subset_and_superset_when_relation_is_directional()
    {
        await Assert.That(RelationType.ProperSubset.Swap()).IsEqualTo(RelationType.ProperSuperset);
        await Assert.That(RelationType.ProperSuperset.Swap()).IsEqualTo(RelationType.ProperSubset);
    }

    /// <summary>
    /// Verifies: should preserve symmetric relations when relation is swapped.
    /// </summary>
    [Test]
    public async Task Should_preserve_symmetric_relations_when_relation_is_swapped()
    {
        await Assert.That(RelationType.Disjoint.Swap()).IsEqualTo(RelationType.Disjoint);
        await Assert.That(RelationType.Overlapping.Swap()).IsEqualTo(RelationType.Overlapping);
        await Assert.That(RelationType.Equal.Swap()).IsEqualTo(RelationType.Equal);
    }
}