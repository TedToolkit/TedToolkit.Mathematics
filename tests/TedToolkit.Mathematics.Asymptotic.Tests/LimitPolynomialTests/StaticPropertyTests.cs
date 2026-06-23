// -----------------------------------------------------------------------
// <copyright file="StaticPropertyTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class StaticPropertyTests
{
    [Test]
    public async Task Should_expose_expected_identity_values_when_static_properties_are_read()
    {
        await Assert.That(LimitPolynomial<double, int>.One)
            .IsEqualTo(new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(1, 0)));
        await Assert.That(LimitPolynomial<double, int>.Zero)
            .IsEqualTo(new LimitPolynomial<double, int>(new PolynomialTerm<double, int>(0, 0)));
        await Assert.That(LimitPolynomial<double, int>.AdditiveIdentity).IsEqualTo(LimitPolynomial<double, int>.Zero);
        await Assert.That(LimitPolynomial<double, int>.MultiplicativeIdentity).IsEqualTo(LimitPolynomial<double, int>.One);
        await Assert.That(LimitPolynomial<double, int>.Radix).IsEqualTo(2);
    }
}