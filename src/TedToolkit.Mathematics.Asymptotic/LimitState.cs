// -----------------------------------------------------------------------
// <copyright file="LimitState.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic;

/// <summary>
/// Represents the possible outcomes of a limit evaluation.
/// </summary>
public enum LimitState
{
    /// <summary>
    /// The limit converges to a finite value.
    /// </summary>
    FINITE = 0,

    /// <summary>
    /// The limit diverges to positive infinity.
    /// </summary>
    POSITIVE_INFINITY = 1,

    /// <summary>
    /// The limit diverges to negative infinity.
    /// </summary>
    NEGATIVE_INFINITY = 2,

    /// <summary>
    /// The limit approaches zero from the positive side.
    /// </summary>
    POSITIVE_INFINITESIMAL = 3,

    /// <summary>
    /// The limit approaches zero from the negative side.
    /// </summary>
    NEGATIVE_INFINITESIMAL = 4,
}