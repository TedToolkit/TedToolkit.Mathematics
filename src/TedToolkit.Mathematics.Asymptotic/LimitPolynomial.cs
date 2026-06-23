// -----------------------------------------------------------------------
// <copyright file="LimitPolynomial.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections;
using System.Numerics;

using Cysharp.Text;

namespace TedToolkit.Mathematics.Asymptotic;

/// <summary>
/// Represents a polynomial-like asymptotic expression ordered by descending exponent.
/// </summary>
/// <typeparam name="TCoefficient">The numeric type used for coefficients.</typeparam>
/// <typeparam name="TExponent">The numeric type used for exponents.</typeparam>
public readonly partial struct
#pragma warning disable CA1710
    LimitPolynomial<TCoefficient, TExponent> : IReadOnlyCollection<PolynomialTerm<TCoefficient, TExponent>>
#pragma warning restore CA1710
    where TCoefficient : INumber<TCoefficient>
    where TExponent : ISignedNumber<TExponent>, INumber<TExponent>
{
    private static readonly Comparer<PolynomialTerm<TCoefficient, TExponent>> _descendingExponentComparer =
        Comparer<PolynomialTerm<TCoefficient, TExponent>>.Create(static (x, y) =>
            y.Exponent.CompareTo(x.Exponent));

    private readonly SortedSet<PolynomialTerm<TCoefficient, TExponent>> _terms;

    /// <summary>
    /// Gets the normalized collection of terms.
    /// </summary>
    public IReadOnlyCollection<PolynomialTerm<TCoefficient, TExponent>> Terms
    {
        get
        {
            return _terms;
        }
    }

    /// <summary>
    /// Gets the term with the highest exponent.
    /// </summary>
    public PolynomialTerm<TCoefficient, TExponent> LeadingTerm
    {
        get
        {
            return _terms.First();
        }
    }

    /// <summary>
    /// Initializes a limit polynomial from a sequence of terms.
    /// </summary>
    /// <param name="values">The source terms.</param>
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

        if (_terms.Count is not 0)
        {
            return;
        }

        _terms = new([new PolynomialTerm<TCoefficient, TExponent>(TCoefficient.Zero, TExponent.Zero),]);
    }

    /// <summary>
    /// Gets the limit of the leading term as <c>x</c> approaches infinity.
    /// </summary>
    public LimitResult<TCoefficient> LimitValue
    {
        get
        {
            return LeadingTerm.LimitValue;
        }
    }

    /// <inheritdoc />
    public IEnumerator<PolynomialTerm<TCoefficient, TExponent>> GetEnumerator()
    {
        return _terms.GetEnumerator();
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return ToString(null, null, null);
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Formats the limit polynomial using separate coefficient and exponent format strings.
    /// </summary>
    /// <param name="coefficientFormat">The optional coefficient format string.</param>
    /// <param name="exponentFormat">The optional exponent format string.</param>
    /// <param name="formatProvider">The optional format provider.</param>
    /// <param name="maxTerms">The maximum number of terms to include before truncating the output.</param>
    /// <returns>The formatted asymptotic expression.</returns>
    public string ToString(string? coefficientFormat, string? exponentFormat, IFormatProvider? formatProvider,
        uint maxTerms = 10)
    {
        using var sb = ZString.CreateStringBuilder();

        if (_terms.Any(t => t.Exponent != TExponent.Zero))
        {
            sb.Append(StringHelpers.LIMIT_PREFIX);
        }

        if (_terms.Count > 1)
        {
            sb.Append('(');
        }

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
                sb.Append(polynomialTerm.Coefficient.ToString(coefficientFormat, formatProvider));
                sb.Append(StringHelpers.ToX(polynomialTerm.Exponent, exponentFormat, formatProvider));
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
                sb.Append(polynomialTerm.Coefficient.ToString(coefficientFormat, formatProvider).TrimStart(key));
                sb.Append(StringHelpers.ToX(polynomialTerm.Exponent, exponentFormat, formatProvider));
            }
        }

        if (_terms.Count > 1)
        {
            sb.Append(')');
        }

        return sb.ToString();
    }

    /// <inheritdoc />
    public int Count
    {
        get
        {
            return _terms.Count;
        }
    }
}