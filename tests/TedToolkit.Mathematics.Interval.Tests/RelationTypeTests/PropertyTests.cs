// -----------------------------------------------------------------------
// <copyright file="PropertyTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval.Tests.RelationTypeTests;

internal sealed class PropertyTests
{
    /// <summary>
    /// Verifies: should expose expected flags when relation is disjoint.
    /// </summary>
    [Test]
    public async Task Should_expose_expected_flags_when_relation_is_disjoint()
    {
        var relation = RelationType.Disjoint;

        await Assert.That(relation.IsDisjoint).IsTrue();
        await Assert.That(relation.IsIntersecting).IsFalse();
        await Assert.That(relation.IsSubset).IsFalse();
        await Assert.That(relation.IsSuperset).IsFalse();
    }

    /// <summary>
    /// Verifies: should expose expected flags when relation is equal.
    /// </summary>
    [Test]
    public async Task Should_expose_expected_flags_when_relation_is_equal()
    {
        var relation = RelationType.Equal;

        await Assert.That(relation.IsEqual).IsTrue();
        await Assert.That(relation.IsSubset).IsTrue();
        await Assert.That(relation.IsSuperset).IsTrue();
        await Assert.That(relation.IsIntersecting).IsTrue();
    }

    /// <summary>
    /// Verifies: should expose expected flags when relation is overlapping.
    /// </summary>
    [Test]
    public async Task Should_expose_expected_flags_when_relation_is_overlapping()
    {
        var relation = RelationType.Overlapping;

        await Assert.That(relation.IsOverlapping).IsTrue();
        await Assert.That(relation.IsIntersecting).IsTrue();
        await Assert.That(relation.IsSubset).IsFalse();
        await Assert.That(relation.IsSuperset).IsFalse();
    }

    /// <summary>
    /// Verifies: should expose expected flags when relation is proper subset.
    /// </summary>
    [Test]
    public async Task Should_expose_expected_flags_when_relation_is_proper_subset()
    {
        var relation = RelationType.ProperSubset;

        await Assert.That(relation.IsProperSubset).IsTrue();
        await Assert.That(relation.IsSubset).IsTrue();
        await Assert.That(relation.IsSuperset).IsFalse();
    }

    /// <summary>
    /// Verifies: should expose expected flags when relation is proper superset.
    /// </summary>
    [Test]
    public async Task Should_expose_expected_flags_when_relation_is_proper_superset()
    {
        var relation = RelationType.ProperSuperset;

        await Assert.That(relation.IsProperSuperset).IsTrue();
        await Assert.That(relation.IsSubset).IsFalse();
        await Assert.That(relation.IsSuperset).IsTrue();
    }
}