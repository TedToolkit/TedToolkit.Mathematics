// -----------------------------------------------------------------------
// <copyright file="LimitPolynomial.Number.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace TedToolkit.Mathematics.Asymptotic;

public readonly partial struct LimitPolynomial<TCoefficient, TExponent> :
    INumber<LimitPolynomial<TCoefficient, TExponent>>
{
    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            LimitPolynomial<TCoefficient, TExponent> other => CompareTo(other),
            _ => throw new InvalidOperationException(),
        };
    }

    /// <summary>
    /// Compares two expressions by growth rate only.
    /// </summary>
    /// <param name="other">The expression to compare with.</param>
    /// <returns>
    /// A signed value indicating whether this expression grows more slowly than, the same as, or faster than <paramref name="other"/>.
    /// </returns>
    public int CompareGrowthRateTo(in LimitPolynomial<TCoefficient, TExponent> other)
    {
        return LeadingTerm.CompareTo(other.LeadingTerm);
    }

    /// <inheritdoc />
    public int CompareTo(LimitPolynomial<TCoefficient, TExponent> other)
    {
        IEnumerable<PolynomialTerm<TCoefficient, TExponent>> leftTerms = _terms ?? [];
        IEnumerable<PolynomialTerm<TCoefficient, TExponent>> rightTerms = other._terms ?? [];

        using var leftEnumerator = leftTerms.GetEnumerator();
        using var rightEnumerator = rightTerms.GetEnumerator();

        var leftHasTerm = leftEnumerator.MoveNext();
        var rightHasTerm = rightEnumerator.MoveNext();

        while (leftHasTerm || rightHasTerm)
        {
            if (!leftHasTerm)
            {
                return TCoefficient.Zero.CompareTo(rightEnumerator.Current.Coefficient);
            }

            if (!rightHasTerm)
            {
                return leftEnumerator.Current.Coefficient.CompareTo(TCoefficient.Zero);
            }

            var leftTerm = leftEnumerator.Current;
            var rightTerm = rightEnumerator.Current;
            var exponentCompare = leftTerm.Exponent.CompareTo(rightTerm.Exponent);

            if (exponentCompare == 0)
            {
                var coefficientCompare = leftTerm.Coefficient.CompareTo(rightTerm.Coefficient);
                if (coefficientCompare != 0)
                {
                    return coefficientCompare;
                }

                leftHasTerm = leftEnumerator.MoveNext();
                rightHasTerm = rightEnumerator.MoveNext();
                continue;
            }

            if (exponentCompare > 0)
            {
                return leftTerm.Coefficient.CompareTo(TCoefficient.Zero);
            }

            return TCoefficient.Zero.CompareTo(rightTerm.Coefficient);
        }

        return 0;
    }

    /// <inheritdoc />
    public bool Equals(LimitPolynomial<TCoefficient, TExponent> other)
    {
        return CompareTo(other) == 0;
    }

    /// <summary>
    /// Determines whether two expressions have the same leading-order growth.
    /// </summary>
    /// <param name="other">The expression to compare with.</param>
    /// <returns><see langword="true"/> if the leading terms are equal; otherwise, <see langword="false"/>.</returns>
    public bool EqualsGrowthRate(in LimitPolynomial<TCoefficient, TExponent> other)
    {
        return LeadingTerm.Equals(other.LeadingTerm);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is LimitPolynomial<TCoefficient, TExponent> other && Equals(other);
    }

    /// <inheritdoc />
    public static bool operator ==(LimitPolynomial<TCoefficient, TExponent> left,
        LimitPolynomial<TCoefficient, TExponent> right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc />
    public static bool operator !=(LimitPolynomial<TCoefficient, TExponent> left,
        LimitPolynomial<TCoefficient, TExponent> right)
    {
        return !left.Equals(right);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = default(HashCode);
        foreach (var term in _terms ?? [])
        {
            hashCode.Add(term);
        }

        return hashCode.ToHashCode();
    }

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return ToString(format, null, formatProvider);
    }

    /// <inheritdoc />
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format,
        IFormatProvider? provider)
    {
        var formatted = ToString(format.ToString(), formatProvider: provider);
        charsWritten = 0;

        if (StringHelpers.TryCopyTo(formatted, destination, ref charsWritten))
        {
            return true;
        }

        charsWritten = 0;
        return false;
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> Parse(string s, IFormatProvider? provider)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        out LimitPolynomial<TCoefficient, TExponent> result)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider,
        out LimitPolynomial<TCoefficient, TExponent> result)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public static bool operator >(LimitPolynomial<TCoefficient, TExponent> left,
        LimitPolynomial<TCoefficient, TExponent> right)
    {
        return left.CompareTo(right) > 0;
    }

    /// <inheritdoc />
    public static bool operator >=(LimitPolynomial<TCoefficient, TExponent> left,
        LimitPolynomial<TCoefficient, TExponent> right)
    {
        return left.CompareTo(right) >= 0;
    }

    /// <inheritdoc />
    public static bool operator <(LimitPolynomial<TCoefficient, TExponent> left,
        LimitPolynomial<TCoefficient, TExponent> right)
    {
        return left.CompareTo(right) < 0;
    }

    /// <inheritdoc />
    public static bool operator <=(LimitPolynomial<TCoefficient, TExponent> left,
        LimitPolynomial<TCoefficient, TExponent> right)
    {
        return left.CompareTo(right) <= 0;
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> operator +(LimitPolynomial<TCoefficient, TExponent> left,
        LimitPolynomial<TCoefficient, TExponent> right)
    {
        return new([..left, ..right,]);
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> operator --(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return value - One;
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> operator /(
        LimitPolynomial<TCoefficient, TExponent> left,
        LimitPolynomial<TCoefficient, TExponent> right)
    {
        return DivRem(left, right).Quotient;
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> operator ++(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return value + One;
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> operator %(
        LimitPolynomial<TCoefficient, TExponent> left,
        LimitPolynomial<TCoefficient, TExponent> right)
    {
        return DivRem(left, right).Remainder;
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> operator *(
        LimitPolynomial<TCoefficient, TExponent> left,
        LimitPolynomial<TCoefficient, TExponent> right)
    {
        var terms = new List<PolynomialTerm<TCoefficient, TExponent>>();

        foreach (var leftTerm in left._terms)
        {
            foreach (var rightTerm in right._terms)
            {
                terms.Add(leftTerm.Multiply(rightTerm));
            }
        }

        return new(terms);
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> operator -(
        LimitPolynomial<TCoefficient, TExponent> left,
        LimitPolynomial<TCoefficient, TExponent> right)
    {
        return left + (-right);
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> operator -(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return new(value._terms.Select(static term =>
            new PolynomialTerm<TCoefficient, TExponent>(-term.Coefficient, term.Exponent)));
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> operator +(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return value;
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> Abs(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return IsNegative(value) ? -value : value;
    }

    /// <inheritdoc />
    public static bool IsCanonical(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return true;
    }

    /// <inheritdoc />
    public static bool IsComplexNumber(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return false;
    }

    /// <inheritdoc />
    public static bool IsEvenInteger(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return TryGetConstant(value, out var constant) && TCoefficient.IsEvenInteger(constant);
    }

    /// <inheritdoc />
    public static bool IsFinite(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return value._terms.All(static term => term.IsFinite);
    }

    /// <inheritdoc />
    public static bool IsImaginaryNumber(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return false;
    }

    /// <inheritdoc />
    public static bool IsInfinity(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return !IsZero(value) && value.LeadingTerm.IsInfinity;
    }

    /// <inheritdoc />
    public static bool IsInteger(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return TryGetConstant(value, out var constant) && TCoefficient.IsInteger(constant);
    }

    /// <inheritdoc />
    public static bool IsNaN(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return false;
    }

    /// <inheritdoc />
    public static bool IsNegative(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return value.CompareTo(Zero) < 0;
    }

    /// <inheritdoc />
    public static bool IsNegativeInfinity(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return !IsZero(value) && value.LeadingTerm.IsNegativeInfinity;
    }

    /// <inheritdoc />
    public static bool IsNormal(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return !IsZero(value) && !IsSubnormal(value) && !IsInfinity(value);
    }

    /// <inheritdoc />
    public static bool IsOddInteger(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return TryGetConstant(value, out var constant) && TCoefficient.IsOddInteger(constant);
    }

    /// <inheritdoc />
    public static bool IsPositive(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return value.CompareTo(Zero) > 0;
    }

    /// <inheritdoc />
    public static bool IsPositiveInfinity(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return !IsZero(value) && value.LeadingTerm.IsPositiveInfinity;
    }

    /// <inheritdoc />
    public static bool IsRealNumber(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return true;
    }

    /// <inheritdoc />
    public static bool IsSubnormal(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return TryGetConstant(value, out var constant) && TCoefficient.IsSubnormal(constant);
    }

    /// <inheritdoc />
    public static bool IsZero(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return value._terms.All(static term => term.IsZero);
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> MaxMagnitude(
        LimitPolynomial<TCoefficient, TExponent> x,
        LimitPolynomial<TCoefficient, TExponent> y)
    {
        var magnitudeCompare = Abs(x).CompareTo(Abs(y));
        if (magnitudeCompare != 0)
        {
            return magnitudeCompare > 0 ? x : y;
        }

        return x.CompareTo(y) >= 0 ? x : y;
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> MaxMagnitudeNumber(
        LimitPolynomial<TCoefficient, TExponent> x,
        LimitPolynomial<TCoefficient, TExponent> y)
    {
        return MaxMagnitude(x, y);
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> MinMagnitude(
        LimitPolynomial<TCoefficient, TExponent> x,
        LimitPolynomial<TCoefficient, TExponent> y)
    {
        var magnitudeCompare = Abs(x).CompareTo(Abs(y));
        if (magnitudeCompare != 0)
        {
            return magnitudeCompare < 0 ? x : y;
        }

        return x.CompareTo(y) <= 0 ? x : y;
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> MinMagnitudeNumber(
        LimitPolynomial<TCoefficient, TExponent> x,
        LimitPolynomial<TCoefficient, TExponent> y)
    {
        return MinMagnitude(x, y);
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> Parse(ReadOnlySpan<char> s, NumberStyles style,
        IFormatProvider? provider)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> Parse(string s, NumberStyles style,
        IFormatProvider? provider)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public static bool TryConvertFromChecked<TOther>(
        TOther value,
        out LimitPolynomial<TCoefficient, TExponent> result)
        where TOther : INumberBase<TOther>
    {
        if (TCoefficient.TryConvertFromChecked(value, out var coefficient))
        {
            result = new(new PolynomialTerm<TCoefficient, TExponent>(coefficient, TExponent.Zero));
            return true;
        }

        result = Zero;
        return false;
    }

    /// <inheritdoc />
    public static bool TryConvertFromSaturating<TOther>(
        TOther value,
        out LimitPolynomial<TCoefficient, TExponent> result)
        where TOther : INumberBase<TOther>
    {
        if (TCoefficient.TryConvertFromSaturating(value, out var coefficient))
        {
            result = new(new PolynomialTerm<TCoefficient, TExponent>(coefficient, TExponent.Zero));
            return true;
        }

        result = Zero;
        return false;
    }

    /// <inheritdoc />
    public static bool TryConvertFromTruncating<TOther>(
        TOther value,
        out LimitPolynomial<TCoefficient, TExponent> result)
        where TOther : INumberBase<TOther>
    {
        if (TCoefficient.TryConvertFromTruncating(value, out var coefficient))
        {
            result = new(new PolynomialTerm<TCoefficient, TExponent>(coefficient, TExponent.Zero));
            return true;
        }

        result = Zero;
        return false;
    }

    /// <inheritdoc />
    public static bool TryConvertToChecked<TOther>(
        LimitPolynomial<TCoefficient, TExponent> value,
        [MaybeNullWhen(false)] out TOther result)
        where TOther : INumberBase<TOther>
    {
        if (TryGetConstant(value, out var constant)
            && TryConvertConstantChecked(constant, out result))
        {
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc />
    public static bool TryConvertToSaturating<TOther>(
        LimitPolynomial<TCoefficient, TExponent> value,
        [MaybeNullWhen(false)] out TOther result)
        where TOther : INumberBase<TOther>
    {
        if (TryGetConstant(value, out var constant)
            && TryConvertConstantSaturating(constant, out result))
        {
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc />
    public static bool TryConvertToTruncating<TOther>(
        LimitPolynomial<TCoefficient, TExponent> value,
        [MaybeNullWhen(false)] out TOther result)
        where TOther : INumberBase<TOther>
    {
        if (TryGetConstant(value, out var constant)
            && TryConvertConstantTruncating(constant, out result))
        {
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc />
    public static bool TryParse(
        ReadOnlySpan<char> s,
        NumberStyles style,
        IFormatProvider? provider,
        out LimitPolynomial<TCoefficient, TExponent> result)
    {
        result = Zero;
        return false;
    }

    /// <inheritdoc />
    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        NumberStyles style,
        IFormatProvider? provider,
        out LimitPolynomial<TCoefficient, TExponent> result)
    {
        result = Zero;
        return false;
    }

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> One { get; } =
        new(new PolynomialTerm<TCoefficient, TExponent>(TCoefficient.One, TExponent.Zero));

    /// <inheritdoc />
    public static int Radix { get; } = TCoefficient.Radix;

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> Zero { get; } =
        new(new PolynomialTerm<TCoefficient, TExponent>(TCoefficient.Zero, TExponent.Zero));

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> AdditiveIdentity { get; } = Zero;

    /// <inheritdoc />
    public static LimitPolynomial<TCoefficient, TExponent> MultiplicativeIdentity { get; } = One;

    /// <inheritdoc cref="IBinaryInteger{TSelf}.DivRem"/>
    public static (LimitPolynomial<TCoefficient, TExponent> Quotient, LimitPolynomial<TCoefficient, TExponent>
        Remainder) DivRem(
            in LimitPolynomial<TCoefficient, TExponent> dividend,
            in LimitPolynomial<TCoefficient, TExponent> divisor)
    {
        if (IsZero(divisor))
        {
            throw new DivideByZeroException();
        }

        var quotientTerms = new List<PolynomialTerm<TCoefficient, TExponent>>();
        var remainder = dividend;

        while (!IsZero(remainder) && remainder.LeadingTerm.Exponent.CompareTo(divisor.LeadingTerm.Exponent) >= 0)
        {
            var quotientTerm = remainder.LeadingTerm.Divide(divisor.LeadingTerm);
            quotientTerms.Add(quotientTerm);
            remainder -= divisor * new LimitPolynomial<TCoefficient, TExponent>(quotientTerm);
        }

        return (new(quotientTerms), remainder);
    }

    private static bool TryGetConstant(in LimitPolynomial<TCoefficient, TExponent> value,
        [MaybeNullWhen(false)] out TCoefficient constant)
    {
        if (value._terms.Count == 1)
        {
            var term = value._terms.Min;
            if (term.Exponent == TExponent.Zero)
            {
                constant = term.Coefficient;
                return true;
            }
        }

        constant = default!;
        return false;
    }

    private static bool TryConvertConstantChecked<TOther>(
        TCoefficient constant,
        [MaybeNullWhen(false)] out TOther result)
        where TOther : INumberBase<TOther>
    {
        if (constant is TOther sameType)
        {
            result = sameType;
            return true;
        }

        return TCoefficient.TryConvertToChecked(constant, out result)
            || TOther.TryConvertFromChecked(constant, out result);
    }

    private static bool TryConvertConstantSaturating<TOther>(
        TCoefficient constant,
        [MaybeNullWhen(false)] out TOther result)
        where TOther : INumberBase<TOther>
    {
        if (constant is TOther sameType)
        {
            result = sameType;
            return true;
        }

        return TCoefficient.TryConvertToSaturating(constant, out result)
            || TOther.TryConvertFromSaturating(constant, out result);
    }

    private static bool TryConvertConstantTruncating<TOther>(
        TCoefficient constant,
        [MaybeNullWhen(false)] out TOther result)
        where TOther : INumberBase<TOther>
    {
        if (constant is TOther sameType)
        {
            result = sameType;
            return true;
        }

        return TCoefficient.TryConvertToTruncating(constant, out result)
            || TOther.TryConvertFromTruncating(constant, out result);
    }
}