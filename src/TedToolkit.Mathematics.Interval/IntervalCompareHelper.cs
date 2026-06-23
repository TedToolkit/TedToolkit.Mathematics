// -----------------------------------------------------------------------
// <copyright file="IntervalCompareHelper.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval;

/// <summary>
/// Provides shared comparison helpers for interval endpoints and boundary types.
/// </summary>
internal static class IntervalCompareHelper
{
    /// <summary>
    /// Gets the boundary comparer that orders closed minima before open minima.
    /// </summary>
    internal static IComparer<BoundaryType> MinimumBoundaryTypeComparerInstance { get; } =
        new MinimumBoundaryTypeComparer();

    /// <summary>
    /// Gets the boundary comparer that orders open maxima before closed maxima.
    /// </summary>
    internal static IComparer<BoundaryType> MaximumBoundaryTypeComparerInstance { get; } =
        new MaximumBoundaryTypeComparer();

    /// <summary>
    /// Compares two interval endpoints using the supplied boundary-type ordering.
    /// </summary>
    /// <typeparam name="TData">The endpoint value type.</typeparam>
    /// <param name="left">The left endpoint.</param>
    /// <param name="right">The right endpoint.</param>
    /// <param name="comparer">The comparer used when both endpoint values are equal.</param>
    /// <returns>A signed comparison result.</returns>
    internal static int CompareIntervalEnd<TData>(
        scoped in IntervalEnd<TData> left,
        scoped in IntervalEnd<TData> right,
        IComparer<BoundaryType> comparer)
        where TData : IComparable<TData>
    {
        return new IntervalEndComparer<TData>(comparer).Compare(left, right);
    }

    private sealed class MinimumBoundaryTypeComparer : IComparer<BoundaryType>
    {
        public int Compare(BoundaryType x, BoundaryType y)
        {
            if (x == y)
            {
                return 0;
            }

            return x == BoundaryType.CLOSED ? -1 : 1;
        }
    }

    private sealed class MaximumBoundaryTypeComparer : IComparer<BoundaryType>
    {
        public int Compare(BoundaryType x, BoundaryType y)
        {
            if (x == y)
            {
                return 0;
            }

            return x == BoundaryType.OPEN ? -1 : 1;
        }
    }

    private readonly struct IntervalEndComparer<TData>(IComparer<BoundaryType> boundaryTypeComparer)
        : IComparer<IntervalEnd<TData>>
        where TData : IComparable<TData>
    {
        public int Compare(IntervalEnd<TData> x, IntervalEnd<TData> y)
        {
            var valueComparison = x.Value.CompareTo(y.Value);
            if (valueComparison is not 0)
            {
                return valueComparison;
            }

            return boundaryTypeComparer.Compare(x.Type, y.Type);
        }
    }
}