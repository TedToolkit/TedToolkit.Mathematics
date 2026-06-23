using TedToolkit.Mathematics.Interval;

namespace TedToolkit.Mathematics.Interval.Tests;

internal sealed class IntervalDefinitionTests
{
    [Test]
    public async Task Create_ReordersEndpointsAndReportsReversed()
    {
        var interval = Interval<int>.Create(3, 1, out var isReversed);

        await Assert.That(isReversed).IsTrue();
        await Assert.That(interval.Min.Value).IsEqualTo(1);
        await Assert.That(interval.Max.Value).IsEqualTo(3);
    }

    [Test]
    public async Task IsEmpty_TreatsSameOpenEndpointsAsEmpty()
    {
        var interval = Interval<int>.Create(
            new(1, BoundaryType.OPEN),
            new(1, BoundaryType.OPEN),
            out _);

        await Assert.That(interval.IsEmpty).IsTrue();
        await Assert.That(interval.IsInside(1)).IsFalse();
        await Assert.That(interval.ToString()).IsEqualTo("Ø");
    }

    [Test]
    public async Task IsEmpty_TreatsClosedSingletonAsNonEmpty()
    {
        var interval = Interval<int>.Create(1, 1, out _);

        await Assert.That(interval.IsEmpty).IsFalse();
        await Assert.That(interval.IsInside(1)).IsTrue();
        await Assert.That(interval.ToString()).IsEqualTo("1");
    }

    [Test]
    public async Task IsInside_RespectsOpenAndClosedBoundaries()
    {
        var interval = Interval<int>.Create(
            new(1, BoundaryType.OPEN),
            new(3, BoundaryType.CLOSED),
            out _);

        await Assert.That(interval.IsInside(1)).IsFalse();
        await Assert.That(interval.IsInside(2)).IsTrue();
        await Assert.That(interval.IsInside(3)).IsTrue();
    }

    [Test]
    public async Task Clamp_ThrowsForEmptyInterval()
    {
        var interval = Interval<int>.Create(
            new(1, BoundaryType.OPEN),
            new(1, BoundaryType.OPEN),
            out _);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
        {
            interval.Clamp(1);
            return Task.CompletedTask;
        });
    }

    [Test]
    public async Task IsAdjacentTo_IsSymmetricForTouchingIntervals()
    {
        var left = Interval<int>.Create(1, 2, out _);
        var right = Interval<int>.Create(2, 3, out _);

        await Assert.That(left.IsAdjacentTo(right)).IsTrue();
        await Assert.That(right.IsAdjacentTo(left)).IsTrue();
    }

    [Test]
    public async Task GetRelationTo_TreatsAllEmptyIntervalsAsEqual()
    {
        var left = Interval<int>.Create(
            new(1, BoundaryType.OPEN),
            new(1, BoundaryType.OPEN),
            out _);
        var right = Interval<int>.Create(
            new(2, BoundaryType.OPEN),
            new(2, BoundaryType.OPEN),
            out _);

        var relation = left.GetRelationTo(right);

        await Assert.That(relation).IsEqualTo(RelationType.Equal);
    }
}
