// -----------------------------------------------------------------------
// <copyright file="ComparisonOperatorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.PolynomialTermTests;

internal sealed class ComparisonOperatorTests
{
    /// <summary>
    /// Verifies: should compare terms with relational operators.
    /// </summary>
    [Test]
    public async Task Should_compare_terms_with_relational_operators()
    {
        var lower = new PolynomialTerm<double, int>(2, 1);
        var higher = new PolynomialTerm<double, int>(2, 3);

        await Assert.That(lower < higher).IsTrue();
        await Assert.That(lower <= higher).IsTrue();
        await Assert.That(higher > lower).IsTrue();
        await Assert.That(higher >= lower).IsTrue();
    }
}