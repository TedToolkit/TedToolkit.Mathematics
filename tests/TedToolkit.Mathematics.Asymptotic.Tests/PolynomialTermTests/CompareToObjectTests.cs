// -----------------------------------------------------------------------
// <copyright file="CompareToObjectTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Mathematics.Asymptotic.Tests.PolynomialTermTests;

internal sealed class CompareToObjectTests
{
    /// <summary>
    /// Verifies: should return positive value when object is null.
    /// </summary>
    [Test]
    public async Task Should_return_positive_value_when_object_is_null()
    {
        var term = new PolynomialTerm<double, int>(1, 0);

        await Assert.That(term.CompareTo(null)).IsEqualTo(1);
    }

    /// <summary>
    /// Verifies: should delegate to typed compare when object is same type.
    /// </summary>
    [Test]
    public async Task Should_delegate_to_typed_compare_when_object_is_same_type()
    {
        var left = new PolynomialTerm<double, int>(2, 0);
        var right = new PolynomialTerm<double, int>(1, 0);

        await Assert.That(left.CompareTo((object)right)).IsGreaterThan(0);
    }

    /// <summary>
    /// Verifies: should throw when object has unsupported type.
    /// </summary>
    [Test]
    public async Task Should_throw_when_object_has_unsupported_type()
    {
        var term = new PolynomialTerm<double, int>(1, 0);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
        {
            _ = term.CompareTo("nope");
            return Task.CompletedTask;
        });
    }
}