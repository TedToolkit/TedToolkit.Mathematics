// -----------------------------------------------------------------------
// <copyright file="PolynomialTerm.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Numerics;

using Cysharp.Text;

namespace TedToolkit.Mathematics.Asymptotic;

/// <summary>
/// Represents a single term in a limit polynomial.
/// </summary>
/// <typeparam name="TCoefficient">The numeric type used for coefficients.</typeparam>
/// <typeparam name="TExponent">The numeric type used for exponents.</typeparam>
public readonly partial record struct PolynomialTerm<TCoefficient, TExponent>
    where TCoefficient : INumber<TCoefficient>
    where TExponent : ISignedNumber<TExponent>, INumber<TExponent>
{
    /// <summary>
    /// Gets the coefficient of the term.
    /// </summary>
    public TCoefficient Coefficient { get; }

    /// <summary>
    /// Gets the exponent of the term.
    /// </summary>
    public TExponent Exponent { get; }

    /// <summary>
    /// Initializes a constant term.
    /// </summary>
    /// <param name="coefficient">The coefficient of the constant term.</param>
    public PolynomialTerm(TCoefficient coefficient)
        : this(coefficient, TExponent.Zero)
    {
    }

    /// <summary>
    /// Initializes a polynomial term with the specified coefficient and exponent.
    /// </summary>
    /// <param name="coefficient">The coefficient of the term.</param>
    /// <param name="exponent">The exponent of the term.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="coefficient"/> or <paramref name="exponent"/> is not a supported finite value.
    /// </exception>
    public PolynomialTerm(TCoefficient coefficient, TExponent exponent)
    {
        if (TCoefficient.IsNaN(coefficient))
        {
            throw new ArgumentException("Coefficient must not be NaN.", nameof(coefficient));
        }

        if (TCoefficient.IsInfinity(coefficient))
        {
            throw new ArgumentException("Coefficient must not be infinity.", nameof(coefficient));
        }

        if (TExponent.IsNaN(exponent))
        {
            throw new ArgumentException("Exponent must not be NaN.", nameof(exponent));
        }

        Coefficient = coefficient;
        Exponent = TCoefficient.IsZero(coefficient) ? TExponent.Zero : exponent;
    }

    /// <summary>
    /// Converts a coefficient into a constant polynomial term.
    /// </summary>
    /// <param name="coefficient">The coefficient to convert.</param>
    public static implicit operator PolynomialTerm<TCoefficient, TExponent>(TCoefficient coefficient)
    {
        return new(coefficient);
    }

    /// <summary>
    /// Converts a polynomial term to its limit result.
    /// </summary>
    /// <param name="term">The term to evaluate.</param>
    public static implicit operator LimitResult<TCoefficient>(in PolynomialTerm<TCoefficient, TExponent> term)
    {
        return term.LimitValue;
    }

    /// <summary>
    /// Gets the limit contributed by this term as <c>x</c> approaches infinity.
    /// </summary>
    public LimitResult<TCoefficient> LimitValue
    {
        get
        {
            if (Exponent == TExponent.Zero)
            {
                return new(LimitState.FINITE, Coefficient);
            }

            if (Exponent < TExponent.Zero)
            {
                return new(LimitState.FINITE, TCoefficient.Zero);
            }

            if (Coefficient > TCoefficient.Zero)
            {
                return new(LimitState.POSITIVE_INFINITY, TCoefficient.Zero);
            }

            return new(LimitState.NEGATIVE_INFINITY, TCoefficient.Zero);
        }
    }

    /// <summary>
    /// Formats the term using separate coefficient and exponent format strings.
    /// </summary>
    /// <param name="coefficientFormat">The optional coefficient format string.</param>
    /// <param name="exponentFormat">The optional exponent format string.</param>
    /// <param name="formatProvider">The optional format provider.</param>
    /// <returns>The formatted term.</returns>
    public string ToString(string? coefficientFormat, string? exponentFormat, IFormatProvider? formatProvider)
    {
        using var sb = ZString.CreateStringBuilder();
        if (Exponent != TExponent.Zero)
        {
            sb.Append(StringHelpers.LIMIT_PREFIX);
        }

        sb.Append(Coefficient.ToString(coefficientFormat, formatProvider));

        if (Exponent != TExponent.Zero)
        {
            sb.Append(StringHelpers.ToX(Exponent, exponentFormat, formatProvider));
        }

        return sb.ToString();
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return ToString(null, null, null);
    }
}