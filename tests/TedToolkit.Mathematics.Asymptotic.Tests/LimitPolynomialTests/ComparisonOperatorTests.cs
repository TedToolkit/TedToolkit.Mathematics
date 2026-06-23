// -----------------------------------------------------------------------
// <copyright file="ComparisonOperatorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class ComparisonOperatorTests
{
    /// <summary>
    /// Verifies: should reflect compare to results when relational operators are used.
    /// </summary>
    [Test]
    public async Task Should_reflect_compare_to_results_when_relational_operators_are_used()
    {
        var smaller = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(1, 0));
        var larger = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(2, 0));

        await Assert.That(smaller < larger).IsTrue();
        await Assert.That(smaller <= larger).IsTrue();
        await Assert.That(larger > smaller).IsTrue();
        await Assert.That(larger >= smaller).IsTrue();
    }
}