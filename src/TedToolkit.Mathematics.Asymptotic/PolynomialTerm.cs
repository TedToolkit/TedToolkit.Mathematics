using System.Buffers;
using System.Numerics;

namespace TedToolkit.Mathematics.Asymptotic;

public readonly partial record struct PolynomialTerm<TCoefficient, TExponent>
    where TCoefficient : INumber<TCoefficient>
    where TExponent : ISignedNumber<TExponent>, INumber<TExponent>
{
    public TCoefficient Coefficient { get; }
    public TExponent Exponent { get; }

    public PolynomialTerm(TCoefficient coefficient)
        : this(coefficient, TExponent.Zero)
    {
    }

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

    public static implicit operator PolynomialTerm<TCoefficient, TExponent>(TCoefficient coefficient)
    {
        return new(coefficient);
    }

    public static implicit operator LimitResult<TCoefficient>(PolynomialTerm<TCoefficient, TExponent> term)
    {
        return term.LimitValue;
    }

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

    public string ToString(string? coefficientFormat, string? exponentFormat, IFormatProvider? formatProvider)
    {
        return FormatToString(coefficientFormat.AsSpan(), exponentFormat.AsSpan(), formatProvider, omitCoefficientSign: false);
    }

    internal bool TryFormat(
        Span<char> destination,
        out int charsWritten,
        ReadOnlySpan<char> coefficientFormat,
        ReadOnlySpan<char> exponentFormat,
        IFormatProvider? formatProvider,
        bool omitCoefficientSign)
    {
        charsWritten = 0;

        if (TCoefficient.IsZero(Coefficient))
        {
            return TCoefficient.Zero.TryFormat(destination, out charsWritten, coefficientFormat, formatProvider);
        }

        var coefficient = omitCoefficientSign && Coefficient < TCoefficient.Zero
            ? TCoefficient.Abs(Coefficient)
            : Coefficient;

        if (!coefficient.TryFormat(destination, out charsWritten, coefficientFormat, formatProvider))
        {
            charsWritten = 0;
            return false;
        }

        if (Exponent == TExponent.Zero)
        {
            return true;
        }

        if (!StringHelpers.TryCopyTo(StringHelpers.LIMIT_PREFIX, destination, ref charsWritten))
        {
            charsWritten = 0;
            return false;
        }

        if (!StringHelpers.TryFormatX(Exponent, destination[charsWritten..], out var exponentCharsWritten, exponentFormat, formatProvider))
        {
            charsWritten = 0;
            return false;
        }

        charsWritten += exponentCharsWritten;
        return true;
    }

    public override string ToString()
    {
        return ToString(null, null, null);
    }

    private string FormatToString(
        ReadOnlySpan<char> coefficientFormat,
        ReadOnlySpan<char> exponentFormat,
        IFormatProvider? formatProvider,
        bool omitCoefficientSign)
    {
        Span<char> initialBuffer = stackalloc char[64];
        char[]? rentedBuffer = null;

        try
        {
            var buffer = initialBuffer;
            var charsWritten = 0;
            while (!TryFormat(buffer, out charsWritten, coefficientFormat, exponentFormat, formatProvider, omitCoefficientSign))
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
