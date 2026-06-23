using TedToolkit.Mathematics.Asymptotic;

namespace TedToolkit.Mathematics.Asymptotic.Tests;

internal sealed class LimitPolynomialNumberTests
{
    [Test]
    public async Task Add_CombinesLikeTermsAcrossPolynomials()
    {
        var left = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(1, 1),
            ]);
        var right = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(5, 3),
                new PolynomialTerm<double, int>(4, 0),
            ]);

        var result = left + right;

        await Assert.That(result).IsEqualTo(new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(7, 3),
                new PolynomialTerm<double, int>(1, 1),
                new PolynomialTerm<double, int>(4, 0),
            ]));
    }

    [Test]
    public async Task Subtract_NegatesRightHandTermsBeforeCombining()
    {
        var left = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(7, 3),
                new PolynomialTerm<double, int>(4, 0),
            ]);
        var right = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(5, 3),
                new PolynomialTerm<double, int>(1, 1),
            ]);

        var result = left - right;

        await Assert.That(result).IsEqualTo(new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(-1, 1),
                new PolynomialTerm<double, int>(4, 0),
            ]));
    }

    [Test]
    public async Task UnaryOperators_PreserveOrNegateAllCoefficients()
    {
        var value = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(-4, 1),
                new PolynomialTerm<double, int>(6, 0),
            ]);

        await Assert.That(+value).IsEqualTo(value);
        await Assert.That(-value).IsEqualTo(new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(-2, 3),
                new PolynomialTerm<double, int>(4, 1),
                new PolynomialTerm<double, int>(-6, 0),
            ]));
    }

    [Test]
    public async Task Increment_AddsOneToConstantTerm()
    {
        var value = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(6, 0),
            ]);

        var result = ++value;

        await Assert.That(result).IsEqualTo(new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(7, 0),
            ]));
    }

    [Test]
    public async Task Decrement_SubtractsOneFromConstantTerm()
    {
        var value = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(6, 0),
            ]);

        var result = --value;

        await Assert.That(result).IsEqualTo(new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(5, 0),
            ]));
    }

    [Test]
    public async Task CompareTo_UsesHighestDifferentExponent()
    {
        var left = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(1, 1),
            ]);
        var right = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(5, 0),
            ]);

        await Assert.That(left.CompareTo(right)).IsGreaterThan(0);
        await Assert.That(right.CompareTo(left)).IsLessThan(0);
    }

    [Test]
    public async Task CompareTo_NegativeHigherDegreeTermIsLessThanPositiveLowerDegreeTerm()
    {
        var left = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(-1, 3),
            ]);
        var right = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(100, 2),
            ]);

        await Assert.That(left.CompareTo(right)).IsLessThan(0);
        await Assert.That(right.CompareTo(left)).IsGreaterThan(0);
    }

    [Test]
    public async Task CompareTo_UsesLeadingCoefficientWhenHighestExponentMatches()
    {
        var left = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
            ]);
        var right = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(5, 3),
            ]);

        await Assert.That(left.CompareTo(right)).IsLessThan(0);
        await Assert.That(right.CompareTo(left)).IsGreaterThan(0);
    }

    [Test]
    public async Task CompareTo_FallsBackToNextTermWhenLeadingTermsMatch()
    {
        var left = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(3, 1),
            ]);
        var right = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(1, 1),
            ]);

        await Assert.That(left.CompareTo(right)).IsGreaterThan(0);
        await Assert.That(right.CompareTo(left)).IsLessThan(0);
    }

    [Test]
    public async Task CompareTo_ZeroPolynomialIsGreaterThanNegativeGrowthAtInfinity()
    {
        var zero = LimitPolynomial<double, int>.Zero;
        var negativeGrowth = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(-1, 2),
            ]);

        await Assert.That(zero.CompareTo(negativeGrowth)).IsGreaterThan(0);
        await Assert.That(negativeGrowth.CompareTo(zero)).IsLessThan(0);
    }

    [Test]
    public async Task CompareTo_DefaultPolynomialBehavesLikeZero()
    {
        LimitPolynomial<double, int> zero = default;
        var positiveGrowth = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(1, 2),
            ]);

        await Assert.That(zero.CompareTo(LimitPolynomial<double, int>.Zero)).IsEqualTo(0);
        await Assert.That(zero.CompareTo(positiveGrowth)).IsLessThan(0);
        await Assert.That(positiveGrowth.CompareTo(zero)).IsGreaterThan(0);
    }

    [Test]
    public async Task CompareTo_MissingHigherDegreeTermBehavesLikeZeroCoefficient()
    {
        var left = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(1, 1),
            ]);
        var right = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(-2, 3),
                new PolynomialTerm<double, int>(1, 1),
            ]);

        await Assert.That(left.CompareTo(right)).IsGreaterThan(0);
        await Assert.That(right.CompareTo(left)).IsLessThan(0);
    }

    [Test]
    public async Task Equals_IgnoresZeroTermsButMatchesAllNonZeroTerms()
    {
        var left = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(3, 2),
                new PolynomialTerm<double, int>(0, 5),
                new PolynomialTerm<double, int>(4, 0),
            ]);
        var right = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(3, 2),
                new PolynomialTerm<double, int>(4, 0),
            ]);
        var different = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(3, 2),
                new PolynomialTerm<double, int>(5, 0),
            ]);

        await Assert.That(left.Equals(right)).IsTrue();
        await Assert.That(right.Equals(left)).IsTrue();
        await Assert.That(left.Equals(different)).IsFalse();
    }

    [Test]
    public async Task EqualsGrowthRate_OnlyComparesLeadingNonZeroTerm()
    {
        var left = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(1, 1),
            ]);
        var sameGrowthRate = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(9, 0),
            ]);
        var differentGrowthRate = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(5, 3),
                new PolynomialTerm<double, int>(1, 1),
            ]);

        await Assert.That(left.EqualsGrowthRate(sameGrowthRate)).IsTrue();
        await Assert.That(left.EqualsGrowthRate(differentGrowthRate)).IsFalse();
    }

    [Test]
    public async Task Multiply_PerformsPolynomialConvolution()
    {
        var left = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 1),
                new PolynomialTerm<double, int>(3, 0),
            ]);
        var right = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(4, 2),
                new PolynomialTerm<double, int>(5, 0),
            ]);

        var result = left * right;

        await Assert.That(result).IsEqualTo(new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(8, 3),
                new PolynomialTerm<double, int>(12, 2),
                new PolynomialTerm<double, int>(10, 1),
                new PolynomialTerm<double, int>(15, 0),
            ]));
    }

    [Test]
    public async Task Divide_UsesPolynomialLongDivision()
    {
        var dividend = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(1, 2),
                new PolynomialTerm<double, int>(3, 1),
                new PolynomialTerm<double, int>(2, 0),
            ]);
        var divisor = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(1, 1),
                new PolynomialTerm<double, int>(1, 0),
            ]);

        var quotient = dividend / divisor;
        var remainder = dividend % divisor;

        await Assert.That(quotient).IsEqualTo(new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(1, 1),
                new PolynomialTerm<double, int>(2, 0),
            ]));
        await Assert.That(remainder).IsEqualTo(LimitPolynomial<double, int>.Zero);
    }

    [Test]
    public async Task Modulo_ReturnsRemainingTermsAfterLongDivision()
    {
        var dividend = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(1, 2),
                new PolynomialTerm<double, int>(1, 0),
            ]);
        var divisor = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(1, 1),
                new PolynomialTerm<double, int>(1, 0),
            ]);

        var remainder = dividend % divisor;

        await Assert.That(remainder).IsEqualTo(new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 0),
            ]));
    }

    [Test]
    public async Task ParseMembers_ThrowNotSupportedException()
    {
        await Assert.That(() => LimitPolynomial<double, int>.Parse("1", provider: null)).Throws<NotSupportedException>();
        await Assert.That(() => LimitPolynomial<double, int>.Parse("1", System.Globalization.NumberStyles.Number, provider: null)).Throws<NotSupportedException>();
        await Assert.That(() => LimitPolynomial<double, int>.Parse("1".AsSpan(), provider: null)).Throws<NotSupportedException>();
        await Assert.That(() => LimitPolynomial<double, int>.Parse("1".AsSpan(), System.Globalization.NumberStyles.Number, provider: null)).Throws<NotSupportedException>();
    }

    [Test]
    public async Task TryConvertFromChecked_CreatesConstantPolynomial()
    {
        var converted = LimitPolynomial<double, int>.TryConvertFromChecked(3, out var result);

        await Assert.That(converted).IsTrue();
        await Assert.That(result).IsEqualTo(new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(3, 0),
            ]));
    }

    [Test]
    public async Task TryConvertToChecked_OnlySucceedsForConstantPolynomials()
    {
        var constant = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(3, 0),
                new PolynomialTerm<double, int>(0, 5),
            ]);
        var nonConstant = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(3, 1),
            ]);

        var constantConverted = LimitPolynomial<double, int>.TryConvertToChecked<double>(constant, out var constantResult);
        var nonConstantConverted = LimitPolynomial<double, int>.TryConvertToChecked<double>(nonConstant, out var nonConstantResult);

        await Assert.That(constantConverted).IsTrue();
        await Assert.That(constantResult).IsEqualTo(3);
        await Assert.That(nonConstantConverted).IsFalse();
        await Assert.That(nonConstantResult).IsEqualTo(default(double));
    }

    [Test]
    public async Task ToString_FormatsLikeExistingPolynomialRepresentation()
    {
        var polynomial = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(-4, 1),
                new PolynomialTerm<double, int>(6, 0),
            ]);

        await Assert.That(polynomial.ToString()).IsEqualTo("lim[x→∞] (2lim[x→∞] x³ - 4lim[x→∞] x + 6)");
    }

    [Test]
    public async Task TryFormat_MatchesToStringOutputAndRespectsFormat()
    {
        var polynomial = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(12.5, 2),
                new PolynomialTerm<double, int>(-3.25, -1),
            ]);
        var expected = polynomial.ToString("0.0", null);
        var buffer = new char[expected.Length];
        var formatted = polynomial.TryFormat(buffer, out var charsWritten, "0.0".AsSpan(), null);
        var actual = new string(buffer.AsSpan(0, charsWritten));

        await Assert.That(formatted).IsTrue();
        await Assert.That(charsWritten).IsEqualTo(expected.Length);
        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    public async Task TryFormat_ReturnsFalseWhenDestinationIsTooSmall()
    {
        var polynomial = new LimitPolynomial<double, int>(
            [
                new PolynomialTerm<double, int>(2, 3),
                new PolynomialTerm<double, int>(1, 0),
            ]);
        var buffer = new char[8];
        var formatted = polynomial.TryFormat(buffer, out var charsWritten, default, null);

        await Assert.That(formatted).IsFalse();
        await Assert.That(charsWritten).IsEqualTo(0);
    }
}
