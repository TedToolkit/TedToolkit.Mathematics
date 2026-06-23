// -----------------------------------------------------------------------
// <copyright file="Interval.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Cysharp.Text;

namespace TedToolkit.Mathematics.Interval;

/// <summary>
/// Represents an interval with configurable open or closed boundaries.
/// </summary>
/// <typeparam name="TData">The type of the values stored at the interval boundaries.</typeparam>
public readonly record struct Interval<TData>
    where TData : IComparable<TData>
{
    /// <summary>
    /// Gets a value indicating whether the interval contains no valid values.
    /// An interval is empty when both endpoints are equal and at least one side is open.
    /// </summary>
    public bool IsEmpty
    {
        get
        {
            return Min.Value.CompareTo(Max.Value) is 0
                && (Min.Type is BoundaryType.OPEN || Max.Type is BoundaryType.OPEN);
        }
    }

    /// <summary>
    /// Gets the lower endpoint of the interval.
    /// </summary>
    public IntervalEnd<TData> Min { get; }

    /// <summary>
    /// Gets the upper endpoint of the interval.
    /// </summary>
    public IntervalEnd<TData> Max { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Interval{TData}"/> struct.
    /// </summary>
    /// <param name="min">The lower endpoint.</param>
    /// <param name="max">The upper endpoint.</param>
    internal Interval(
        scoped in IntervalEnd<TData> min,
        scoped in IntervalEnd<TData> max)
    {
        Min = min;
        Max = max;
    }

#pragma warning disable CA1000
    /// <summary>
    /// Creates an interval whose endpoints are ordered from low to high.
    /// </summary>
    /// <param name="start">The first endpoint supplied by the caller.</param>
    /// <param name="end">The second endpoint supplied by the caller.</param>
    /// <param name="isReversed">Indicates whether the input endpoints had to be swapped.</param>
    /// <returns>An interval with ordered endpoints.</returns>
    public static Interval<TData> Create(
        scoped in IntervalEnd<TData> start,
        scoped in IntervalEnd<TData> end,
        out bool isReversed)
    {
        var compare = start.Value.CompareTo(end.Value);
        isReversed = compare > 0;
        if (isReversed)
        {
            return new(end, start);
        }

        return new(start, end);
    }
#pragma warning restore CA1000

    /// <summary>
    /// Determines whether the specified value lies inside the current interval.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <returns><see langword="true"/> if the value is inside the interval; otherwise, <see langword="false"/>.</returns>
    public bool IsInside(scoped in TData value)
    {
        var valueVsLeft = value.CompareTo(Min.Value);
        var valueVsRight = value.CompareTo(Max.Value);

        var leftCheck = Min.Type == BoundaryType.CLOSED
            ? valueVsLeft >= 0
            : valueVsLeft > 0;

        if (!leftCheck)
        {
            return false;
        }

        return Max.Type == BoundaryType.CLOSED
            ? valueVsRight <= 0
            : valueVsRight < 0;
    }

    /// <summary>
    /// Clamps the specified value so that the result lies within the current interval.
    /// </summary>
    /// <param name="parameter">The value to clamp.</param>
    /// <returns>The clamped result, or an error when the interval is empty.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the interval is empty.</exception>
    public TData Clamp(scoped in TData parameter)
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Cannot clamp a value to an empty interval.");
        }

        if (IsInside(parameter))
        {
            return parameter;
        }

        if (parameter.CompareTo(Min.Value) <= 0)
        {
            return Min.Value;
        }

        return Max.Value;
    }

    /// <summary>
    /// Gets the set relationship between the current interval and another interval.
    /// </summary>
    /// <param name="other">The other interval to compare with.</param>
    /// <returns>The relationship between the two intervals.</returns>
    public RelationType GetRelationTo(scoped in Interval<TData> other)
    {
        if (IsEmpty)
        {
            return other.IsEmpty ? RelationType.Equal : RelationType.ProperSubset;
        }

        if (other.IsEmpty)
        {
            return RelationType.ProperSuperset;
        }

        if (CompareMaximumToMinimum(Max, other.Min) < 0
            || CompareMaximumToMinimum(other.Max, Min) < 0)
        {
            return RelationType.Disjoint;
        }

        var minComp = IntervalCompareHelper.CompareIntervalEnd(
            Min,
            other.Min,
            IntervalCompareHelper.MinimumBoundaryTypeComparerInstance);
        var maxComp = IntervalCompareHelper.CompareIntervalEnd(
            Max,
            other.Max,
            IntervalCompareHelper.MaximumBoundaryTypeComparerInstance);

        if (minComp == 0 && maxComp == 0)
        {
            return RelationType.Equal;
        }

        if (minComp >= 0 && maxComp <= 0)
        {
            return RelationType.ProperSubset;
        }

        if (minComp <= 0 && maxComp >= 0)
        {
            return RelationType.ProperSuperset;
        }

        return RelationType.Overlapping;
    }

    /// <inheritdoc/>
    public override string? ToString()
    {
        if (IsEmpty)
        {
            return "Ø";
        }

        if (Min.Value.CompareTo(Max.Value) is 0)
        {
            return Min.Value.ToString();
        }

        using var builder = ZString.CreateStringBuilder();
        builder.Append(Min.Type == BoundaryType.CLOSED ? "[" : "(");
        builder.Append(Min);
        builder.Append(", ");
        builder.Append(Max);
        builder.Append(Max.Type == BoundaryType.CLOSED ? "]" : ")");
        return builder.ToString();
    }

    /// <summary>
    /// Converts the current interval into an interval set that contains exactly this interval.
    /// </summary>
    /// <returns>An interval set containing the current interval.</returns>
    public IntervalSet<TData> ToIntervalSet()
    {
        return new(this);
    }

    /// <summary>
    /// Implicitly converts an interval into a single-element interval set.
    /// </summary>
    /// <param name="interval">The source interval.</param>
    public static implicit operator IntervalSet<TData>(scoped in Interval<TData> interval)
    {
        return interval.ToIntervalSet();
    }

    /// <summary>
    /// Determines whether the current interval is adjacent to another interval at a shared boundary value.
    /// </summary>
    /// <param name="other">The other interval to compare with.</param>
    /// <returns><see langword="true"/> if the intervals are adjacent; otherwise, <see langword="false"/>.</returns>
    public bool IsAdjacentTo(scoped in Interval<TData> other)
    {
        return Touches(Max, other.Min) || Touches(other.Max, Min);

        static bool Touches(scoped in IntervalEnd<TData> left, scoped in IntervalEnd<TData> right)
        {
            if (left.Value.CompareTo(right.Value) is not 0)
            {
                return false;
            }

            return left.Type is BoundaryType.CLOSED || right.Type is BoundaryType.CLOSED;
        }
    }

    private static int CompareMaximumToMinimum(scoped in IntervalEnd<TData> maximum, scoped in IntervalEnd<TData> minimum)
    {
        var valueComparison = IntervalCompareHelper.CompareIntervalEnd(
            maximum,
            minimum,
            IntervalCompareHelper.MaximumBoundaryTypeComparerInstance);

        if (valueComparison == 0
            && maximum.Value.CompareTo(minimum.Value) is 0
            && maximum.Type == BoundaryType.OPEN
            && minimum.Type == BoundaryType.OPEN)
        {
            return -1;
        }

        return valueComparison;
    }
}
