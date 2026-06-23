using TedToolkit.Mathematics.Interval;

namespace TedToolkit.Mathematics.Interval.Tests;

public class IntervalRelationTests
{
    [Test]
    public async Task GetRelationTo_TreatsSameEndpointsWithDifferentBoundaryTypesAsSubset()
    {
        var left = Interval<int>.Create(
            new(1, BoundaryType.OPEN),
            new(2, BoundaryType.OPEN),
            out _);
        var right = Interval<int>.Create(
            new(1, BoundaryType.CLOSED),
            new(2, BoundaryType.CLOSED),
            out _);

        var relation = left.GetRelationTo(right);

        await Assert.That(relation).IsEqualTo(RelationType.ProperSubset);
    }

    [Test]
    public async Task GetRelationTo_TreatsTouchingOpenBoundariesAsDisjoint()
    {
        var left = Interval<int>.Create(
            new(1, BoundaryType.CLOSED),
            new(2, BoundaryType.OPEN),
            out _);
        var right = Interval<int>.Create(
            new(2, BoundaryType.OPEN),
            new(3, BoundaryType.CLOSED),
            out _);

        var relation = left.GetRelationTo(right);

        await Assert.That(relation).IsEqualTo(RelationType.Disjoint);
    }

    [Test]
    public async Task GetRelationTo_TreatsTouchingClosedBoundariesAsOverlapping()
    {
        var left = Interval<int>.Create(
            new(1, BoundaryType.CLOSED),
            new(2, BoundaryType.CLOSED),
            out _);
        var right = Interval<int>.Create(
            new(2, BoundaryType.CLOSED),
            new(3, BoundaryType.CLOSED),
            out _);

        var relation = left.GetRelationTo(right);

        await Assert.That(relation).IsEqualTo(RelationType.Overlapping);
    }
}
