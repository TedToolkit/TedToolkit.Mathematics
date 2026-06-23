// -----------------------------------------------------------------------
// <copyright file="IntervalEnd.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval;

/// <summary>
/// Represents one endpoint of an interval together with its boundary type.
/// </summary>
/// <typeparam name="TData">The type of the endpoint value.</typeparam>
public readonly record struct IntervalEnd<TData>
    where TData : IComparable<TData>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntervalEnd{TData}"/> struct.
    /// </summary>
    /// <param name="value">The endpoint value.</param>
    /// <param name="type">The boundary type applied to the endpoint.</param>
    public IntervalEnd(TData value, BoundaryType type = BoundaryType.CLOSED)
    {
        Value = value;
        Type = type;
    }

    /// <summary>
    /// Gets the endpoint value.
    /// </summary>
    public TData Value { get; }

    /// <summary>
    /// Gets the boundary type.
    /// </summary>
    public BoundaryType Type { get; }

    /// <summary>
    /// Implicitly converts a plain value into a closed interval endpoint.
    /// </summary>
    /// <param name="value">The endpoint value.</param>
    public static implicit operator IntervalEnd<TData>(TData value)
    {
        return new(value);
    }
}