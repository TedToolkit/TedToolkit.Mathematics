// -----------------------------------------------------------------------
// <copyright file="IntervalSet.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Cysharp.Text;

namespace TedToolkit.Mathematics.Interval;

/// <summary>
/// Represents a normalized set of intervals.
/// Overlapping or adjacent intervals are merged during construction.
/// </summary>
/// <typeparam name="TData">The type of the values stored in the intervals.</typeparam>
public readonly record struct IntervalSet<TData>
    where TData : IComparable<TData>
{
    /// <summary>
    /// Gets the normalized intervals contained in the set.
    /// </summary>
    public IReadOnlyList<Interval<TData>> Intervals { get; }

    /// <summary>
    /// Gets a value indicating whether the current set contains no intervals.
    /// </summary>
    public bool IsEmpty
    {
        get
        {
            return Intervals.Count is 0;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntervalSet{TData}"/> struct.
    /// </summary>
    /// <param name="interval">The initial interval.</param>
    public IntervalSet(scoped in Interval<TData> interval)
        : this([interval,])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntervalSet{TData}"/> struct.
    /// </summary>
    /// <param name="intervals">The source sequence of intervals.</param>
    public IntervalSet(params IEnumerable<Interval<TData>> intervals)
    {
        Intervals = [.. Normalize(intervals),];

        static IEnumerable<Interval<TData>> Normalize(IEnumerable<Interval<TData>> list)
        {
            Interval<TData>? current = null;
            foreach (var interval in list
                         .Where(i => !i.IsEmpty)
                         .OrderBy(i => i.Min.Value))
            {
                if (current is null)
                {
                    current = interval;
                    continue;
                }

                if (HasIntersection(current.Value, interval))
                {
                    current = new Interval<TData>(current.Value.Min, interval.Max);
                }
                else
                {
                    yield return current.Value;
                    current = interval;
                }
            }

            if (current is null)
            {
                yield break;
            }

            yield return current.Value;
        }

        static bool HasIntersection(scoped in Interval<TData> left, scoped in Interval<TData> right)
        {
            if (CompareMaximumToMinimum(left.Max, right.Min) >= 0)
            {
                return true;
            }

            if (left.Max.Value.CompareTo(right.Min.Value) is not 0)
            {
                return false;
            }

            return left.Max.Type is BoundaryType.CLOSED || right.Min.Type is BoundaryType.CLOSED;
        }
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        if (IsEmpty)
        {
            return "Ø";
        }

        using var builder = ZString.CreateStringBuilder();
        foreach (var interval in Intervals)
        {
            if (builder.Length > 0)
            {
                builder.Append(" ∪ ");
            }

            builder.Append(interval);
        }

        return builder.ToString();
    }

    /// <summary>
    /// Gets the union of the current set and another set.
    /// </summary>
    /// <param name="other">The other set.</param>
    /// <returns>The union of the two sets.</returns>
    public IntervalSet<TData> Union(scoped in IntervalSet<TData> other)
    {
        if (IsEmpty)
        {
            return other;
        }

        if (other.IsEmpty)
        {
            return this;
        }

        return new(Intervals.Concat(other.Intervals));
    }

    /// <summary>
    /// Gets the intersection of the current set and another set.
    /// </summary>
    /// <param name="other">The other set.</param>
    /// <returns>The intersection of the two sets.</returns>
    public IntervalSet<TData> Intersect(scoped in IntervalSet<TData> other)
    {
        if (IsEmpty || other.IsEmpty)
        {
            return new([]);
        }

        var result = new List<Interval<TData>>();
        var i = 0;
        var j = 0;
        while (i < Intervals.Count && j < other.Intervals.Count)
        {
            var left = Intervals[i];
            var right = other.Intervals[j];

            if (CompareMaximumToMinimum(left.Max, right.Min) < 0
                || CompareMaximumToMinimum(right.Max, left.Min) < 0)
            {
                MoveNext();
                continue;
            }

            var intersectMin = IntervalCompareHelper.CompareIntervalEnd(
                left.Min,
                right.Min,
                IntervalCompareHelper.MinimumBoundaryTypeComparerInstance) >= 0 ? left.Min : right.Min;
            var intersectMax = IntervalCompareHelper.CompareIntervalEnd(
                left.Max,
                right.Max,
                IntervalCompareHelper.MaximumBoundaryTypeComparerInstance) <= 0 ? left.Max : right.Max;

            var intersectInterval = Interval<TData>.Create(intersectMin, intersectMax, out var reversed);
            if (!reversed && !intersectInterval.IsEmpty)
            {
                result.Add(intersectInterval);
            }

            MoveNext();

            void MoveNext()
            {
                if (IntervalCompareHelper.CompareIntervalEnd(
                        left.Max,
                        right.Max,
                        IntervalCompareHelper.MaximumBoundaryTypeComparerInstance) < 0)
                {
                    i++;
                }
                else
                {
                    j++;
                }
            }
        }

        return new(result);
    }

    /// <summary>
    /// Gets the set difference after subtracting another set from the current set.
    /// </summary>
    /// <param name="other">The set to subtract.</param>
    /// <returns>The difference between the current set and the specified set.</returns>
    public IntervalSet<TData> Difference(scoped in IntervalSet<TData> other)
    {
        if (IsEmpty || other.IsEmpty)
        {
            return this;
        }

        var result = new List<Interval<TData>>(Intervals);
        foreach (var sub in other.Intervals)
        {
            var nextResult = new List<Interval<TData>>();
            foreach (var cur in result)
            {
                if (CompareMaximumToMinimum(cur.Max, sub.Min) < 0
                    || CompareMaximumToMinimum(sub.Max, cur.Min) < 0)
                {
                    nextResult.Add(cur);
                    continue;
                }

                if (IntervalCompareHelper.CompareIntervalEnd(
                        cur.Min,
                        sub.Min,
                        IntervalCompareHelper.MinimumBoundaryTypeComparerInstance) < 0)
                {
                    var leftPart = new Interval<TData>(
                        cur.Min,
                        new IntervalEnd<TData>(
                            sub.Min.Value,
                            sub.Min.Type == BoundaryType.CLOSED ? BoundaryType.OPEN : BoundaryType.CLOSED));
                    if (!leftPart.IsEmpty)
                    {
                        nextResult.Add(leftPart);
                    }
                }

                if (IntervalCompareHelper.CompareIntervalEnd(
                        sub.Max,
                        cur.Max,
                        IntervalCompareHelper.MaximumBoundaryTypeComparerInstance) < 0)
                {
                    var rightPart = new Interval<TData>(
                        new IntervalEnd<TData>(
                            sub.Max.Value,
                            sub.Max.Type == BoundaryType.CLOSED ? BoundaryType.OPEN : BoundaryType.CLOSED),
                        cur.Max);
                    if (!rightPart.IsEmpty)
                    {
                        nextResult.Add(rightPart);
                    }
                }
            }

            result = nextResult;
        }

        return new(result);
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