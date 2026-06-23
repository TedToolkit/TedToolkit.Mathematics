// -----------------------------------------------------------------------
// <copyright file="BoundaryType.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Interval;

/// <summary>
/// Indicates whether an interval boundary includes its endpoint.
/// </summary>
public enum BoundaryType
{
    /// <summary>
    /// Closed boundary, written as <c>[ ]</c>.
    /// </summary>
    CLOSED = 0,

    /// <summary>
    /// Open boundary, written as <c>( )</c>.
    /// </summary>
    OPEN = 1,
}
