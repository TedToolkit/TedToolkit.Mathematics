using System.Collections;
using System.Numerics;

using Cysharp.Text;

namespace TedToolkit.Mathematics.Asymptotic;

public readonly partial struct LimitPolynomial<TCoefficient, TExponent> : IReadOnlyCollection<PolynomialTerm<TCoefficient, TExponent>>
    where TCoefficient : INumber<TCoefficient>
    where TExponent : ISignedNumber<TExponent>, INumber<TExponent>
{
    private static readonly Comparer<PolynomialTerm<TCoefficient, TExponent>> _descendingExponentComparer
        = Comparer<PolynomialTerm<TCoefficient, TExponent>>.Create(static (x, y) =>
            y.Exponent.CompareTo(x.Exponent));

    private readonly SortedSet<PolynomialTerm<TCoefficient, TExponent>> _terms;

    public IReadOnlyCollection<PolynomialTerm<TCoefficient, TExponent>> Terms => _terms;

    public PolynomialTerm<TCoefficient, TExponent> LeadingTerm
        => _terms.First();

    public LimitPolynomial(params IEnumerable<PolynomialTerm<TCoefficient, TExponent>> values)
    {
        _terms = new(
            values
                .GroupBy(v => v.Exponent)
                .Select(g =>
                {
                    var sum = TCoefficient.Zero;
                    foreach (var polynomialTerm in g)
                    {
                        sum += polynomialTerm.Coefficient;
                    }

                    return new PolynomialTerm<TCoefficient, TExponent>(sum, g.Key);
                })
                .Where(t => t.Coefficient != TCoefficient.Zero),
            _descendingExponentComparer);

        if (_terms.Count == 0)
        {
            _terms = new([new PolynomialTerm<TCoefficient, TExponent>(TCoefficient.Zero, TExponent.Zero)]);
        }
    }

    public LimitResult<TCoefficient> LimitValue
    {
        get { return LeadingTerm.LimitValue; }
    }

    public IEnumerator<PolynomialTerm<TCoefficient, TExponent>> GetEnumerator() => _terms.GetEnumerator();

    public override string ToString()
    {
        return ToString(null, null, null);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public string ToString(string? coefficientFormat, string? exponentFormat, IFormatProvider? formatProvider,
        uint maxTerms = 10)
    {
        using var sb = ZString.CreateStringBuilder();
        sb.Append(StringHelpers.LIMIT_PREFIX);
        sb.Append('(');

        var count = 0;
        foreach (var polynomialTerm in _terms)
        {
            if (polynomialTerm.Coefficient == TCoefficient.Zero)
            {
                continue;
            }

            count++;
            if (count is 1)
            {
                sb.Append(polynomialTerm.ToString(coefficientFormat, exponentFormat, formatProvider));
            }
            else if (count >= maxTerms)
            {
                sb.Append(" + ...");
                break;
            }
            else
            {
                var key = polynomialTerm.Coefficient > TCoefficient.Zero ? '+' : '-';
                sb.Append(' ');
                sb.Append(key);
                sb.Append(' ');
                sb.Append(polynomialTerm.ToString(coefficientFormat, exponentFormat, formatProvider).TrimStart(key));
            }
        }

        sb.Append(')');
        return sb.ToString();
    }


    private bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> coefficientFormat,
        ReadOnlySpan<char> exponentFormat, IFormatProvider? provider, uint maxTerms)
    {
        charsWritten = 0;
        if (!StringHelpers.TryCopyTo(StringHelpers.LIMIT_PREFIX, destination, ref charsWritten)
            || !StringHelpers.TryCopyTo('(', destination, ref charsWritten))
        {
            charsWritten = 0;
            return false;
        }

        var count = 0U;
        foreach (var polynomialTerm in _terms ?? [])
        {
            if (polynomialTerm.Coefficient == TCoefficient.Zero)
            {
                continue;
            }

            count++;
            if (count is 1)
            {
                if (!polynomialTerm.TryFormat(destination[charsWritten..], out var termCharsWritten, coefficientFormat,
                        exponentFormat, provider, omitCoefficientSign: false))
                {
                    charsWritten = 0;
                    return false;
                }

                charsWritten += termCharsWritten;
            }
            else if (count >= maxTerms)
            {
                if (!StringHelpers.TryCopyTo(" + ...", destination, ref charsWritten))
                {
                    charsWritten = 0;
                    return false;
                }

                break;
            }
            else
            {
                var sign = polynomialTerm.Coefficient > TCoefficient.Zero ? '+' : '-';
                if (!StringHelpers.TryCopyTo(' ', destination, ref charsWritten)
                    || !StringHelpers.TryCopyTo(sign, destination, ref charsWritten)
                    || !StringHelpers.TryCopyTo(' ', destination, ref charsWritten))
                {
                    charsWritten = 0;
                    return false;
                }

                if (!polynomialTerm.TryFormat(destination[charsWritten..], out var termCharsWritten, coefficientFormat,
                        exponentFormat, provider, omitCoefficientSign: sign == '-'))
                {
                    charsWritten = 0;
                    return false;
                }

                charsWritten += termCharsWritten;
            }
        }

        if (!StringHelpers.TryCopyTo(')', destination, ref charsWritten))
        {
            charsWritten = 0;
            return false;
        }

        return true;
    }

    public int Count => _terms.Count;
}