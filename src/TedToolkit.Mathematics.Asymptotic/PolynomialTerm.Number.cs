// -----------------------------------------------------------------------
// <copyright file="PolynomialTerm.Number.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic;

public readonly partial record struct PolynomialTerm<TCoefficient, TExponent> :
    IComparable,
    IComparable<PolynomialTerm<TCoefficient, TExponent>>
{
    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            PolynomialTerm<TCoefficient, TExponent> other => CompareTo(other),
            _ => throw new InvalidOperationException(),
        };
    }

    /// <inheritdoc />
    public int CompareTo(PolynomialTerm<TCoefficient, TExponent> other)
    {
        var sign = TCoefficient.Sign(Coefficient);
        var otherSign = TCoefficient.Sign(other.Coefficient);

        if (sign != otherSign)
        {
            return sign.CompareTo(otherSign);
        }

        if (sign == 0)
        {
            return 0;
        }

        var exponentCompare = Exponent.CompareTo(other.Exponent);

        if (exponentCompare != 0)
        {
            return sign * exponentCompare;
        }

        return Coefficient.CompareTo(other.Coefficient);
    }

    /// <summary>
    /// Determines whether one term compares greater than another.
    /// </summary>
    /// <param name="left">The left-hand term.</param>
    /// <param name="right">The right-hand term.</param>
    public static bool operator >(in PolynomialTerm<TCoefficient, TExponent> left,
        in PolynomialTerm<TCoefficient, TExponent> right)
    {
        return left.CompareTo(right) > 0;
    }

    /// <summary>
    /// Determines whether one term compares greater than or equal to another.
    /// </summary>
    /// <param name="left">The left-hand term.</param>
    /// <param name="right">The right-hand term.</param>
    public static bool operator >=(in PolynomialTerm<TCoefficient, TExponent> left,
        in PolynomialTerm<TCoefficient, TExponent> right)
    {
        return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Determines whether one term compares less than another.
    /// </summary>
    /// <param name="left">The left-hand term.</param>
    /// <param name="right">The right-hand term.</param>
    public static bool operator <(in PolynomialTerm<TCoefficient, TExponent> left,
        in PolynomialTerm<TCoefficient, TExponent> right)
    {
        return left.CompareTo(right) < 0;
    }

    /// <summary>
    /// Determines whether one term compares less than or equal to another.
    /// </summary>
    /// <param name="left">The left-hand term.</param>
    /// <param name="right">The right-hand term.</param>
    public static bool operator <=(in PolynomialTerm<TCoefficient, TExponent> left,
        in PolynomialTerm<TCoefficient, TExponent> right)
    {
        return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Multiplies this term by another term.
    /// </summary>
    /// <param name="other">The term to multiply by.</param>
    /// <returns>The product term.</returns>
    internal PolynomialTerm<TCoefficient, TExponent> Multiply(in PolynomialTerm<TCoefficient, TExponent> other)
    {
        return new(Coefficient * other.Coefficient, Exponent + other.Exponent);
    }

    /// <summary>
    /// Divides this term by another term.
    /// </summary>
    /// <param name="other">The divisor term.</param>
    /// <returns>The quotient term.</returns>
    internal PolynomialTerm<TCoefficient, TExponent> Divide(in PolynomialTerm<TCoefficient, TExponent> other)
    {
        return new(Coefficient / other.Coefficient, Exponent - other.Exponent);
    }

    /// <summary>
    /// Gets a value indicating whether the term has a finite limit as <c>x</c> approaches infinity.
    /// </summary>
    internal bool IsFinite
    {
        get
        {
            return Exponent <= TExponent.Zero;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the term diverges to infinity in magnitude.
    /// </summary>
    internal bool IsInfinity
    {
        get
        {
            return Exponent > TExponent.Zero;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the coefficient is negative.
    /// </summary>
    internal bool IsNegative
    {
        get
        {
            return Coefficient < TCoefficient.Zero;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the term diverges to negative infinity.
    /// </summary>
    internal bool IsNegativeInfinity
    {
        get
        {
            return IsInfinity && IsNegative;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the coefficient is positive.
    /// </summary>
    internal bool IsPositive
    {
        get
        {
            return Coefficient > TCoefficient.Zero;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the term diverges to positive infinity.
    /// </summary>
    internal bool IsPositiveInfinity
    {
        get
        {
            return IsInfinity && IsPositive;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the coefficient is zero.
    /// </summary>
    internal bool IsZero
    {
        get
        {
            return TCoefficient.IsZero(Coefficient);
        }
    }
}