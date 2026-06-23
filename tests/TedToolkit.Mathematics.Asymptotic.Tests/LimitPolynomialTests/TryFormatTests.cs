// -----------------------------------------------------------------------
// <copyright file="TryFormatTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.LimitPolynomialTests;

internal sealed class TryFormatTests
{
    /// <summary>
    /// Verifies: should not expose internal span formatting helpers.
    /// </summary>
    [Test]
    public async Task Should_not_expose_internal_span_formatting_helpers()
    {
        var helperMethods = typeof(LimitPolynomial<double, int>)
            .GetMethods(System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.DeclaredOnly)
            .Where(static method => method.Name is "TryFormat" or "FormatToString")
            .Select(static method => method.Name)
            .ToArray();

        await Assert.That(helperMethods).IsEmpty();
    }

    /// <summary>
    /// Verifies: should match to string output when destination is large enough.
    /// </summary>
    [Test]
    public async Task Should_match_to_string_output_when_destination_is_large_enough()
    {
        var polynomial = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(12.5, 2),
            new PolynomialTerm<double, int>(-3.25, -1));
        var expected = polynomial.ToString("0.0", null);
        var buffer = new char[expected.Length];

        var formatted = polynomial.TryFormat(buffer, out var charsWritten, "0.0".AsSpan(), null);
        var actual = new string(buffer.AsSpan(0, charsWritten));

        await Assert.That(formatted).IsTrue();
        await Assert.That(charsWritten).IsEqualTo(expected.Length);
        await Assert.That(actual).IsEqualTo(expected);
    }

    /// <summary>
    /// Verifies: should return false when destination is too small.
    /// Prerequisite: Should_match_to_string_output_when_destination_is_large_enough.
    /// </summary>
    [Test]
    [DependsOn(nameof(Should_match_to_string_output_when_destination_is_large_enough))]
    public async Task Should_return_false_when_destination_is_too_small()
    {
        var polynomial = new LimitPolynomial<double, int>(
            new PolynomialTerm<double, int>(2, 3),
            new PolynomialTerm<double, int>(1, 0));
        var buffer = new char[8];

        var formatted = polynomial.TryFormat(buffer, out var charsWritten, default, null);

        await Assert.That(formatted).IsFalse();
        await Assert.That(charsWritten).IsEqualTo(0);
    }
}