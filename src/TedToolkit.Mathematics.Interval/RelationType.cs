// -----------------------------------------------------------------------
// <copyright file="RelationType.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval;

/// <summary>
/// Represents the set relationship between two operands.
/// </summary>
public readonly record struct RelationType
{
    private enum Type
    {
        DISJOINT = 0,

        OVERLAPPING = 1,

        PROPER_SUPERSET = 2,

        PROPER_SUBSET = 3,

        EQUAL = 4,
    }

    private readonly Type _type;

    private RelationType(Type type)
    {
        _type = type;
    }

    /// <summary>
    /// The two sets share no elements.
    /// </summary>
    public static RelationType Disjoint { get; } = new(Type.DISJOINT);

    /// <summary>
    /// The two sets overlap, but neither is a subset of the other.
    /// </summary>
    public static RelationType Overlapping { get; } = new(Type.OVERLAPPING);

    /// <summary>
    /// The right-hand set is fully contained in the left-hand set, and the left-hand set contains additional elements.
    /// </summary>
    public static RelationType ProperSuperset { get; } = new(Type.PROPER_SUPERSET);

    /// <summary>
    /// The left-hand set is fully contained in the right-hand set, and the right-hand set contains additional elements.
    /// </summary>
    public static RelationType ProperSubset { get; } = new(Type.PROPER_SUBSET);

    /// <summary>
    /// The two sets contain exactly the same elements.
    /// </summary>
    public static RelationType Equal { get; } = new(Type.EQUAL);

    /// <inheritdoc cref="Disjoint"/>
    public bool IsDisjoint
    {
        get
        {
            return _type is Type.DISJOINT;
        }
    }

    /// <inheritdoc cref="Overlapping"/>
    public bool IsOverlapping
    {
        get
        {
            return _type is Type.OVERLAPPING;
        }
    }

    /// <inheritdoc cref="ProperSuperset"/>
    public bool IsProperSuperset
    {
        get
        {
            return _type is Type.PROPER_SUPERSET;
        }
    }

    /// <inheritdoc cref="ProperSubset"/>
    public bool IsProperSubset
    {
        get
        {
            return _type is Type.PROPER_SUBSET;
        }
    }

    /// <inheritdoc cref="Equal"/>
    public bool IsEqual
    {
        get
        {
            return _type is Type.EQUAL;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the left-hand set is contained in the right-hand set.
    /// </summary>
    public bool IsSubset
    {
        get
        {
            return _type is Type.EQUAL or Type.PROPER_SUBSET;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the left-hand set contains the right-hand set.
    /// </summary>
    public bool IsSuperset
    {
        get
        {
            return _type is Type.EQUAL or Type.PROPER_SUPERSET;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the two sets share at least one element.
    /// </summary>
    public bool IsIntersecting
    {
        get
        {
            return _type is not Type.DISJOINT;
        }
    }

    /// <summary>
    /// Swaps the perspective of the relation by exchanging left and right operands.
    /// </summary>
    /// <returns>The relation after the operands are swapped.</returns>
    public RelationType Swap()
    {
        if (IsProperSubset)
        {
            return ProperSuperset;
        }

        if (IsProperSuperset)
        {
            return ProperSubset;
        }

        return this;
    }
}
