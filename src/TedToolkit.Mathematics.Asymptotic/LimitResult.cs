// -----------------------------------------------------------------------
// <copyright file="LimitResult.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Numerics;

using Cysharp.Text;

namespace TedToolkit.Mathematics.Asymptotic;

/// <summary>
/// Represents the result of evaluating a limit.
/// </summary>
/// <typeparam name="TValue">The numeric type used for finite values.</typeparam>
/// <param name="State">The category of the evaluated limit.</param>
/// <param name="Value">The finite value when <paramref name="State"/> is <see cref="LimitState.FINITE"/>.</param>
public readonly record struct LimitResult<TValue>(LimitState State, TValue Value) :
    IFormattable
    where TValue : INumber<TValue>
{
    /// <inheritdoc />
    public override string ToString()
    {
        return ToString(null, null);
    }

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        switch (State)
        {
            case LimitState.FINITE:
                return Value.ToString(format, formatProvider);

            case LimitState.POSITIVE_INFINITY:
                return "+∞";

            case LimitState.NEGATIVE_INFINITY:
                return "-∞";

            case LimitState.POSITIVE_INFINITESIMAL:
                return ZString.Concat(Value.ToString(format, formatProvider), '⁺');

            case LimitState.NEGATIVE_INFINITESIMAL:
                return ZString.Concat(Value.ToString(format, formatProvider), '⁻');

            default:
                throw new NotSupportedException();
        }
    }
}