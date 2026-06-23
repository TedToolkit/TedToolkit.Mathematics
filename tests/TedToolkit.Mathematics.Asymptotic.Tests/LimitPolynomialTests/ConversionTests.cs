// -----------------------------------------------------------------------
// <copyright file="ConversionTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class ConversionTests
{
    [Test]
    public async Task Should_create_constant_polynomial_when_value_is_converted_from_other_number_type()
    {
        var checkedConverted = LimitPolynomial<double, int>.TryConvertFromChecked(3, out var checkedResult);
        var saturatingConverted = LimitPolynomial<double, int>.TryConvertFromSaturating(3, out var saturatingResult);
        var truncatingConverted = LimitPolynomial<double, int>.TryConvertFromTruncating(3, out var truncatingResult);

        await Assert.That(checkedConverted).IsTrue();
        await Assert.That(saturatingConverted).IsTrue();
        await Assert.That(truncatingConverted).IsTrue();
        await Assert.That(checkedResult).IsEqualTo(new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(3, 0)));
        await Assert.That(saturatingResult).IsEqualTo(checkedResult);
        await Assert.That(truncatingResult).IsEqualTo(checkedResult);
    }

    [Test]
    public async Task Should_convert_only_constant_polynomials_to_other_number_types()
    {
        var constant = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(3, 0));
        var nonConstant = new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(3, 1));

        var toChecked = LimitPolynomial<double, int>.TryConvertToChecked<double>(constant, out var checkedResult);
        var toSaturating = LimitPolynomial<double, int>.TryConvertToSaturating<double>(constant, out var saturatingResult);
        var toTruncating = LimitPolynomial<double, int>.TryConvertToTruncating<double>(constant, out var truncatingResult);
        var nonConstantConverted = LimitPolynomial<double, int>.TryConvertToChecked<double>(nonConstant, out var nonConstantResult);

        await Assert.That(toChecked).IsTrue();
        await Assert.That(toSaturating).IsTrue();
        await Assert.That(toTruncating).IsTrue();
        await Assert.That(checkedResult).IsEqualTo(3);
        await Assert.That(saturatingResult).IsEqualTo(3);
        await Assert.That(truncatingResult).IsEqualTo(3);
        await Assert.That(nonConstantConverted).IsFalse();
        await Assert.That(nonConstantResult).IsEqualTo(default(double));
    }
}