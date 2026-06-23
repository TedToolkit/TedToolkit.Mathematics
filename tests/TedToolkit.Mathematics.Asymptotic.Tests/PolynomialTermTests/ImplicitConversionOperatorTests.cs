// -----------------------------------------------------------------------
// <copyright file="ImplicitConversionOperatorTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.PolynomialTermTests;

internal sealed class ImplicitConversionOperatorTests
{
    [Test]
    public async Task Should_convert_coefficient_to_constant_term_when_value_is_assigned_directly()
    {
        PolynomialTerm<double, int> term = 4;

        await Assert.That(term).IsEqualTo(new PolynomialTerm<double, int>(4, 0));
    }

    [Test]
    public async Task Should_convert_term_to_limit_result_when_term_is_used_as_limit_result()
    {
        var term = new PolynomialTerm<double, int>(2, 3);

        LimitResult<double> result = term;

        await Assert.That(result).IsEqualTo(term.LimitValue);
    }
}