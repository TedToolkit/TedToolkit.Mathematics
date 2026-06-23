namespace TedToolkit.Mathematics.Asymptotic;

public readonly partial record struct PolynomialTerm<TCoefficient, TExponent> :
    IComparable<PolynomialTerm<TCoefficient, TExponent>>
{
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

    internal PolynomialTerm<TCoefficient, TExponent> Add(PolynomialTerm<TCoefficient, TExponent> other)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(other.Exponent, Exponent);
        return new(Coefficient + other.Coefficient, Exponent);
    }

    internal PolynomialTerm<TCoefficient, TExponent> Substract(PolynomialTerm<TCoefficient, TExponent> other)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(other.Exponent, Exponent);
        return new(Coefficient - other.Coefficient, Exponent);
    }

    internal PolynomialTerm<TCoefficient, TExponent> Multiply(PolynomialTerm<TCoefficient, TExponent> other)
    {
        return new(Coefficient * other.Coefficient, Exponent + other.Exponent);
    }

    internal PolynomialTerm<TCoefficient, TExponent> Divide(PolynomialTerm<TCoefficient, TExponent> other)
    {
        return new(Coefficient / other.Coefficient, Exponent - other.Exponent);
    }

    internal PolynomialTerm<TCoefficient, TExponent> Module(PolynomialTerm<TCoefficient, TExponent> other)
    {
        if (Coefficient == TCoefficient.Zero)
        {
            throw new DivideByZeroException();
        }

        return Exponent.CompareTo(Exponent) < 0
            ? this
            : new(TCoefficient.Zero, TExponent.Zero);
    }

    internal PolynomialTerm<TCoefficient, TExponent> Abs()
    {
        return new(TCoefficient.Abs(Coefficient), Exponent);
    }

    internal bool IsEvenInteger
        => Exponent == TExponent.Zero && TCoefficient.IsEvenInteger(Coefficient);

    internal bool IsFinite => Exponent <= TExponent.Zero;

    internal bool IsInfinity
        => Exponent > TExponent.Zero;

    internal bool IsInteger
        => Exponent < TExponent.Zero || Exponent == TExponent.Zero && TCoefficient.IsInteger(Coefficient);

    internal bool IsNegative => Coefficient < TCoefficient.Zero;

    internal bool IsNegativeInfinity => IsInfinity && IsNegative;

    internal bool IsNormal
        => !IsZero && !IsSubnormal && !IsInfinity;

    internal bool IsOddInteger
        => Exponent == TExponent.Zero && TCoefficient.IsOddInteger(Coefficient);

    internal bool IsPositive => Coefficient > TCoefficient.Zero;

    internal bool IsPositiveInfinity => IsInfinity && IsPositive;

    internal bool IsSubnormal
        => TCoefficient.IsSubnormal(Coefficient) && TExponent.IsSubnormal(Exponent);

    internal bool IsZero => TCoefficient.IsZero(Coefficient);

    internal PolynomialTerm<TCoefficient, TExponent> MaxMagnitude(PolynomialTerm<TCoefficient, TExponent> other)
    {
        var magnitudeCompare = Abs().CompareTo(other.Abs());
        if (magnitudeCompare != 0)
        {
            return magnitudeCompare > 0 ? this : other;
        }

        return this.CompareTo(other) >= 0 ? this : other;
    }

    internal PolynomialTerm<TCoefficient, TExponent> MinMagnitude(PolynomialTerm<TCoefficient, TExponent> other)
    {
        var magnitudeCompare = Abs().CompareTo(other.Abs());
        if (magnitudeCompare != 0)
        {
            return magnitudeCompare < 0 ? this : other;
        }

        return CompareTo(other) <= 0 ? this : other;
    }
}