// -----------------------------------------------------------------------
// <copyright file="EqualsTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class EqualsTests
{
    /// <summary>
    /// Verifies: should treat polynomials with same terms as equal when equals is called.
    /// </summary>
    [Test]
    public async Task Should_treat_polynomials_with_same_terms_as_equal_when_equals_is_called()
    {
        var left = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(3, 2), new PolynomialTerm<double, int>(4, 0));
        var right = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(3, 2), new PolynomialTerm<double, int>(4, 0));

        await Assert.That(left.Equals(right)).IsTrue();
        await Assert.That(left.Equals((object)right)).IsTrue();
        await Assert.That(left == right).IsTrue();
        await Assert.That(left != right).IsFalse();
        await Assert.That(left.GetHashCode()).IsEqualTo(right.GetHashCode());
    }

    /// <summary>
    /// Verifies: should only compare leading term when growth rate equality is checked.
    /// </summary>
    [Test]
    public async Task Should_only_compare_leading_term_when_growth_rate_equality_is_checked()
    {
        var left = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 3), new PolynomialTerm<double, int>(1, 0));
        var right = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 3), new PolynomialTerm<double, int>(9, 0));

        await Assert.That(left.EqualsGrowthRate(right)).IsTrue();
    }
}