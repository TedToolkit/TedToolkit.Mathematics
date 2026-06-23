// -----------------------------------------------------------------------
// <copyright file="StringHelpers.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Buffers;
using System.Numerics;

using Cysharp.Text;

namespace TedToolkit.Mathematics.Asymptotic;

/// <summary>
/// Provides formatting helpers for asymptotic expressions.
/// </summary>
internal static class StringHelpers
{
    /// <summary>
    /// The textual prefix used when formatting limits as <c>x</c> approaches infinity.
    /// </summary>
    public const string LIMIT_PREFIX = "lim[x→∞] ";

    /// <summary>
    /// Formats an exponent as an <c>x</c>-term using superscript digits when possible.
    /// </summary>
    /// <typeparam name="TExponent">The numeric type used for the exponent.</typeparam>
    /// <param name="exponent">The exponent to format.</param>
    /// <param name="format">The optional exponent format string.</param>
    /// <param name="formatProvider">The optional format provider.</param>
    /// <returns>A formatted <c>x</c>-term.</returns>
    public static string ToX<TExponent>(TExponent exponent, string? format, IFormatProvider? formatProvider)
        where TExponent : INumber<TExponent>
    {
        using var sb = ZString.CreateStringBuilder();
        sb.Append('x');
        if (exponent == TExponent.One)
        {
            return sb.ToString();
        }

        if (ToSuper(exponent.ToString(format, formatProvider)) is not { } super)
        {
            return sb.ToString();
        }

        sb.Append(super);
        return sb.ToString();
    }

    /// <summary>
    /// Copies a character span into the destination buffer.
    /// </summary>
    /// <param name="source">The source characters to copy.</param>
    /// <param name="destination">The destination buffer.</param>
    /// <param name="charsWritten">The number of characters already written and updated after the copy.</param>
    /// <returns><see langword="true"/> if the copy succeeded; otherwise, <see langword="false"/>.</returns>
    public static bool TryCopyTo(in ReadOnlySpan<char> source, in Span<char> destination, ref int charsWritten)
    {
        if (source.Length > destination.Length - charsWritten)
        {
            return false;
        }

        source.CopyTo(destination[charsWritten..]);
        charsWritten += source.Length;
        return true;
    }

    /// <summary>
    /// Copies a single character into the destination buffer.
    /// </summary>
    /// <param name="source">The character to copy.</param>
    /// <param name="destination">The destination buffer.</param>
    /// <param name="charsWritten">The number of characters already written and updated after the copy.</param>
    /// <returns><see langword="true"/> if the copy succeeded; otherwise, <see langword="false"/>.</returns>
    public static bool TryCopyTo(char source, in Span<char> destination, ref int charsWritten)
    {
        if (charsWritten >= destination.Length)
        {
            return false;
        }

        destination[charsWritten] = source;
        charsWritten++;
        return true;
    }

    /// <summary>
    /// Formats an exponent as an <c>x</c>-term into a destination buffer.
    /// </summary>
    /// <typeparam name="TExponent">The numeric type used for the exponent.</typeparam>
    /// <param name="exponent">The exponent to format.</param>
    /// <param name="destination">The destination buffer.</param>
    /// <param name="charsWritten">The number of characters written to <paramref name="destination"/>.</param>
    /// <param name="format">The optional exponent format string.</param>
    /// <param name="formatProvider">The optional format provider.</param>
    /// <returns><see langword="true"/> if formatting succeeded; otherwise, <see langword="false"/>.</returns>
    public static bool TryFormatX<TExponent>(
        TExponent exponent,
        in Span<char> destination,
        out int charsWritten,
        in ReadOnlySpan<char> format,
        IFormatProvider? formatProvider)
        where TExponent : INumber<TExponent>
    {
        charsWritten = 0;
        if (!TryCopyTo('x', destination, ref charsWritten))
        {
            return false;
        }

        if (exponent == TExponent.One)
        {
            return true;
        }

        Span<char> exponentBuffer = stackalloc char[32];
        char[]? rentedBuffer = null;

        try
        {
            var formatted = false;
            var exponentCharsWritten = 0;

            while (!formatted)
            {
                formatted = exponent.TryFormat(exponentBuffer, out exponentCharsWritten, format, formatProvider);
                if (formatted)
                {
                    break;
                }

                var nextBuffer = ArrayPool<char>.Shared.Rent(exponentBuffer.Length * 2);
                if (rentedBuffer is not null)
                {
                    ArrayPool<char>.Shared.Return(rentedBuffer);
                }

                rentedBuffer = nextBuffer;
                exponentBuffer = rentedBuffer;
            }

            if (exponentCharsWritten > destination.Length - charsWritten)
            {
                charsWritten = 0;
                return false;
            }

            foreach (var c in exponentBuffer[..exponentCharsWritten])
            {
                destination[charsWritten] = ToSuper(c);
                charsWritten++;
            }

            return true;
        }
        finally
        {
            if (rentedBuffer is not null)
            {
                ArrayPool<char>.Shared.Return(rentedBuffer);
            }
        }
    }

    private static string? ToSuper(string? str)
    {
        if (str is null)
        {
            return null;
        }

        using var sb = ZString.CreateStringBuilder();

        foreach (var c in str)
        {
            sb.Append(ToSuper(c));
        }

        return sb.ToString();
    }

    private static char ToSuper(char c)
    {
        return c switch
        {
            '-' => '⁻',
            '0' => '⁰',
            '1' => '¹',
            '2' => '²',
            '3' => '³',
            '4' => '⁴',
            '5' => '⁵',
            '6' => '⁶',
            '7' => '⁷',
            '8' => '⁸',
            '9' => '⁹',
            _ => c,
        };
    }
}