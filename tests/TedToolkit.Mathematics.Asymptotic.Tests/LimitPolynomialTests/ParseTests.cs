// -----------------------------------------------------------------------
// <copyright file="ParseTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class ParseTests
{
    [Test]
    public async Task Should_throw_not_supported_exception_when_parse_overloads_are_called()
    {
        await Assert.That(() => LimitPolynomial<double, int>.Parse("1", provider: null)).Throws<NotSupportedException>();
        await Assert.That(() => LimitPolynomial<double, int>.Parse("1".AsSpan(), provider: null)).Throws<NotSupportedException>();
        await Assert.That(() => LimitPolynomial<double, int>.Parse("1", NumberStyles.Number, provider: null))
            .Throws<NotSupportedException>();
        await Assert.That(() => LimitPolynomial<double, int>.Parse("1".AsSpan(), NumberStyles.Number, provider: null))
            .Throws<NotSupportedException>();
    }

    [Test]
    public async Task Should_return_false_or_throw_consistently_when_try_parse_overloads_are_called()
    {
        await Assert.That(() => LimitPolynomial<double, int>.TryParse("1", provider: null, out _)).Throws<NotSupportedException>();
        await Assert.That(() => LimitPolynomial<double, int>.TryParse("1".AsSpan(), provider: null, out _)).Throws<NotSupportedException>();
        await Assert.That(LimitPolynomial<double, int>.TryParse("1", NumberStyles.Number, provider: null, out _)).IsFalse();
        await Assert.That(LimitPolynomial<double, int>.TryParse("1".AsSpan(), NumberStyles.Number, provider: null, out _)).IsFalse();
    }
}