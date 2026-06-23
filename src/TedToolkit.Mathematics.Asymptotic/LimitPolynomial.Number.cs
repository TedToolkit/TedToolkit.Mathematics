using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace TedToolkit.Mathematics.Asymptotic;

public readonly partial struct LimitPolynomial<TCoefficient, TExponent> :
    INumber<LimitPolynomial<TCoefficient, TExponent>>
{
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            LimitPolynomial<TCoefficient, TExponent> other => CompareTo(other),
            _ => throw new InvalidOperationException(),
        };
    }

    public int CompareGrowthRateTo(LimitPolynomial<TCoefficient, TExponent> other)
    {
        return LeadingTerm.CompareTo(other.LeadingTerm);
    }

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

    public bool Equals(LimitPolynomial<TCoefficient, TExponent> other) => CompareTo(other) == 0;

    public bool EqualsGrowthRate(LimitPolynomial<TCoefficient, TExponent> other) => LeadingTerm.Equals(other.LeadingTerm);

    public override bool Equals(object? obj) => obj is LimitPolynomial<TCoefficient, TExponent> other && Equals(other);

    public static bool operator ==(LimitPolynomial<TCoefficient, TExponent> left, LimitPolynomial<TCoefficient, TExponent> right) => left.Equals(right);

    public static bool operator !=(LimitPolynomial<TCoefficient, TExponent> left, LimitPolynomial<TCoefficient, TExponent> right) => !left.Equals(right);
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        foreach (var term in _terms ?? [])
        {
            hashCode.Add(term);
        }

        return hashCode.ToHashCode();
    }

    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format, format, formatProvider);

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => TryFormat(destination, out charsWritten, format, format, provider, maxTerms: 10);

    public static LimitPolynomial<TCoefficient, TExponent> Parse(string s, IFormatProvider? provider) => throw new NotSupportedException();

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out LimitPolynomial<TCoefficient, TExponent> result) => throw new NotSupportedException();

    public static LimitPolynomial<TCoefficient, TExponent> Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => throw new NotSupportedException();

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out LimitPolynomial<TCoefficient, TExponent> result) => throw new NotSupportedException();


    public static bool operator >(LimitPolynomial<TCoefficient, TExponent> left, LimitPolynomial<TCoefficient, TExponent> right) => left.CompareTo(right) > 0;

    public static bool operator >=(LimitPolynomial<TCoefficient, TExponent> left,
        LimitPolynomial<TCoefficient, TExponent> right) => left.CompareTo(right) >= 0;

    public static bool operator <(LimitPolynomial<TCoefficient, TExponent> left, LimitPolynomial<TCoefficient, TExponent> right) => left.CompareTo(right) < 0;

    public static bool operator <=(LimitPolynomial<TCoefficient, TExponent> left,
        LimitPolynomial<TCoefficient, TExponent> right) => left.CompareTo(right) <= 0;


    public static LimitPolynomial<TCoefficient, TExponent> operator +(LimitPolynomial<TCoefficient, TExponent> left,
        LimitPolynomial<TCoefficient, TExponent> right)
    {
        return new LimitPolynomial<TCoefficient, TExponent>([..left, ..right]);
    }


    public static LimitPolynomial<TCoefficient, TExponent> operator --(LimitPolynomial<TCoefficient, TExponent> value) => value - One;

    public static LimitPolynomial<TCoefficient, TExponent> operator /(LimitPolynomial<TCoefficient, TExponent> left, LimitPolynomial<TCoefficient, TExponent> right)
    {
        return Divide(left, right).Quotient;
    }

    public static LimitPolynomial<TCoefficient, TExponent> operator ++(LimitPolynomial<TCoefficient, TExponent> value) => value + One;

    public static LimitPolynomial<TCoefficient, TExponent> operator %(LimitPolynomial<TCoefficient, TExponent> left, LimitPolynomial<TCoefficient, TExponent> right)
    {
        return Divide(left, right).Remainder;
    }

    public static LimitPolynomial<TCoefficient, TExponent> operator *(LimitPolynomial<TCoefficient, TExponent> left, LimitPolynomial<TCoefficient, TExponent> right)
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

    public static LimitPolynomial<TCoefficient, TExponent> operator -(LimitPolynomial<TCoefficient, TExponent> left, LimitPolynomial<TCoefficient, TExponent> right) => left + (-right);

    public static LimitPolynomial<TCoefficient, TExponent> operator -(LimitPolynomial<TCoefficient, TExponent> value)
    {
        return new LimitPolynomial<TCoefficient, TExponent>(value._terms.Select(static term =>
            new PolynomialTerm<TCoefficient, TExponent>(-term.Coefficient, term.Exponent)));
    }

    public static LimitPolynomial<TCoefficient, TExponent> operator +(LimitPolynomial<TCoefficient, TExponent> value) => value;

    public static LimitPolynomial<TCoefficient, TExponent> Abs(LimitPolynomial<TCoefficient, TExponent> value) => IsNegative(value) ? -value : value;

    public static bool IsCanonical(LimitPolynomial<TCoefficient, TExponent> value) => true;

    public static bool IsComplexNumber(LimitPolynomial<TCoefficient, TExponent> value) => false;

    public static bool IsEvenInteger(LimitPolynomial<TCoefficient, TExponent> value) => TryGetConstant(value, out var constant) && TCoefficient.IsEvenInteger(constant);

    public static bool IsFinite(LimitPolynomial<TCoefficient, TExponent> value) => value._terms.All(static term => term.IsFinite);

    public static bool IsImaginaryNumber(LimitPolynomial<TCoefficient, TExponent> value) => false;

    public static bool IsInfinity(LimitPolynomial<TCoefficient, TExponent> value) => !IsZero(value) && value.LeadingTerm.IsInfinity;

    public static bool IsInteger(LimitPolynomial<TCoefficient, TExponent> value) => TryGetConstant(value, out var constant) && TCoefficient.IsInteger(constant);

    public static bool IsNaN(LimitPolynomial<TCoefficient, TExponent> value) => false;

    public static bool IsNegative(LimitPolynomial<TCoefficient, TExponent> value) => value.CompareTo(Zero) < 0;

    public static bool IsNegativeInfinity(LimitPolynomial<TCoefficient, TExponent> value) => !IsZero(value) && value.LeadingTerm.IsNegativeInfinity;

    public static bool IsNormal(LimitPolynomial<TCoefficient, TExponent> value) => !IsZero(value) && !IsSubnormal(value) && !IsInfinity(value);

    public static bool IsOddInteger(LimitPolynomial<TCoefficient, TExponent> value) => TryGetConstant(value, out var constant) && TCoefficient.IsOddInteger(constant);

    public static bool IsPositive(LimitPolynomial<TCoefficient, TExponent> value) => value.CompareTo(Zero) > 0;

    public static bool IsPositiveInfinity(LimitPolynomial<TCoefficient, TExponent> value) => !IsZero(value) && value.LeadingTerm.IsPositiveInfinity;

    public static bool IsRealNumber(LimitPolynomial<TCoefficient, TExponent> value) => true;

    public static bool IsSubnormal(LimitPolynomial<TCoefficient, TExponent> value) => value._terms.All(static term => term.IsZero || term.IsSubnormal);

    public static bool IsZero(LimitPolynomial<TCoefficient, TExponent> value) => value._terms.All(static term => term.IsZero);

    public static LimitPolynomial<TCoefficient, TExponent> MaxMagnitude(LimitPolynomial<TCoefficient, TExponent> x, LimitPolynomial<TCoefficient, TExponent> y)
    {
        var magnitudeCompare = Abs(x).CompareTo(Abs(y));
        if (magnitudeCompare != 0)
        {
            return magnitudeCompare > 0 ? x : y;
        }

        return x.CompareTo(y) >= 0 ? x : y;
    }

    public static LimitPolynomial<TCoefficient, TExponent> MaxMagnitudeNumber(LimitPolynomial<TCoefficient, TExponent> x, LimitPolynomial<TCoefficient, TExponent> y) => MaxMagnitude(x, y);

    public static LimitPolynomial<TCoefficient, TExponent> MinMagnitude(LimitPolynomial<TCoefficient, TExponent> x, LimitPolynomial<TCoefficient, TExponent> y)
    {
        var magnitudeCompare = Abs(x).CompareTo(Abs(y));
        if (magnitudeCompare != 0)
        {
            return magnitudeCompare < 0 ? x : y;
        }

        return x.CompareTo(y) <= 0 ? x : y;
    }

    public static LimitPolynomial<TCoefficient, TExponent> MinMagnitudeNumber(LimitPolynomial<TCoefficient, TExponent> x, LimitPolynomial<TCoefficient, TExponent> y) => MinMagnitude(x, y);

    public static LimitPolynomial<TCoefficient, TExponent> Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider) => throw new NotSupportedException();

    public static LimitPolynomial<TCoefficient, TExponent> Parse(string s, NumberStyles style, IFormatProvider? provider) => throw new NotSupportedException();

    public static bool TryConvertFromChecked<TOther>(TOther value, out LimitPolynomial<TCoefficient, TExponent> result) where TOther : INumberBase<TOther>
    {
        if (TCoefficient.TryConvertFromChecked(value, out var coefficient))
        {
            result = new([new PolynomialTerm<TCoefficient, TExponent>(coefficient, TExponent.Zero)]);
            return true;
        }

        result = Zero;
        return false;
    }

    public static bool TryConvertFromSaturating<TOther>(TOther value, out LimitPolynomial<TCoefficient, TExponent> result) where TOther : INumberBase<TOther>
    {
        if (TCoefficient.TryConvertFromSaturating(value, out var coefficient))
        {
            result = new([new PolynomialTerm<TCoefficient, TExponent>(coefficient, TExponent.Zero)]);
            return true;
        }

        result = Zero;
        return false;
    }

    public static bool TryConvertFromTruncating<TOther>(TOther value, out LimitPolynomial<TCoefficient, TExponent> result) where TOther : INumberBase<TOther>
    {
        if (TCoefficient.TryConvertFromTruncating(value, out var coefficient))
        {
            result = new([new PolynomialTerm<TCoefficient, TExponent>(coefficient, TExponent.Zero)]);
            return true;
        }

        result = Zero;
        return false;
    }

    public static bool TryConvertToChecked<TOther>(LimitPolynomial<TCoefficient, TExponent> value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
    {
        if (TryGetConstant(value, out var constant))
        {
            try
            {
                result = TOther.CreateChecked(constant);
                return true;
            }
            catch (Exception) when (true)
            {
            }
        }

        result = default;
        return false;
    }

    public static bool TryConvertToSaturating<TOther>(LimitPolynomial<TCoefficient, TExponent> value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
    {
        if (TryGetConstant(value, out var constant))
        {
            try
            {
                result = TOther.CreateSaturating(constant);
                return true;
            }
            catch (Exception) when (true)
            {
            }
        }

        result = default;
        return false;
    }

    public static bool TryConvertToTruncating<TOther>(LimitPolynomial<TCoefficient, TExponent> value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
    {
        if (TryGetConstant(value, out var constant))
        {
            try
            {
                result = TOther.CreateTruncating(constant);
                return true;
            }
            catch (Exception) when (true)
            {
            }
        }

        result = default;
        return false;
    }

    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out LimitPolynomial<TCoefficient, TExponent> result)
    {
        result = Zero;
        return false;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, out LimitPolynomial<TCoefficient, TExponent> result)
    {
        result = Zero;
        return false;
    }

    public static LimitPolynomial<TCoefficient, TExponent> One { get; } = new([new PolynomialTerm<TCoefficient, TExponent>(TCoefficient.One, TExponent.Zero)]);
    public static int Radix { get; } = TCoefficient.Radix;
    public static LimitPolynomial<TCoefficient, TExponent> Zero { get; } = new([new PolynomialTerm<TCoefficient, TExponent>(TCoefficient.Zero, TExponent.Zero)]);
    public static LimitPolynomial<TCoefficient, TExponent> AdditiveIdentity { get; } = Zero;
    public static LimitPolynomial<TCoefficient, TExponent> MultiplicativeIdentity { get; } = One;

    private static (LimitPolynomial<TCoefficient, TExponent> Quotient, LimitPolynomial<TCoefficient, TExponent> Remainder) Divide(
        LimitPolynomial<TCoefficient, TExponent> dividend,
        LimitPolynomial<TCoefficient, TExponent> divisor)
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
            remainder -= divisor * new LimitPolynomial<TCoefficient, TExponent>([quotientTerm]);
        }

        return (new(quotientTerms), remainder);
    }

    private static bool TryGetConstant(LimitPolynomial<TCoefficient, TExponent> value, [MaybeNullWhen(false)]out TCoefficient constant)
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

    private string FormatToString(ReadOnlySpan<char> coefficientFormat, ReadOnlySpan<char> exponentFormat, IFormatProvider? provider, uint maxTerms)
    {
        Span<char> initialBuffer = stackalloc char[256];
        char[]? rentedBuffer = null;

        try
        {
            var buffer = initialBuffer;
            var charsWritten = 0;
            while (!TryFormat(buffer, out charsWritten, coefficientFormat, exponentFormat, provider, maxTerms))
            {
                if (rentedBuffer is not null)
                {
                    ArrayPool<char>.Shared.Return(rentedBuffer);
                }

                rentedBuffer = ArrayPool<char>.Shared.Rent(buffer.Length * 2);
                buffer = rentedBuffer;
            }

            return new string(buffer[..charsWritten]);
        }
        finally
        {
            if (rentedBuffer is not null)
            {
                ArrayPool<char>.Shared.Return(rentedBuffer);
            }
        }
    }

}
