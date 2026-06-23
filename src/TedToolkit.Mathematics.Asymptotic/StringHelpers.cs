using System.Buffers;
using System.Numerics;

using Cysharp.Text;

namespace TedToolkit.Mathematics.Asymptotic;

internal static class StringHelpers
{
    public const string LIMIT_PREFIX = "lim[x→∞] ";

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

    public static bool TryCopyTo(ReadOnlySpan<char> source, Span<char> destination, ref int charsWritten)
    {
        if (source.Length > destination.Length - charsWritten)
        {
            return false;
        }

        source.CopyTo(destination[charsWritten..]);
        charsWritten += source.Length;
        return true;
    }

    public static bool TryCopyTo(char source, Span<char> destination, ref int charsWritten)
    {
        if (charsWritten >= destination.Length)
        {
            return false;
        }

        destination[charsWritten] = source;
        charsWritten++;
        return true;
    }

    public static bool TryFormatX<TExponent>(TExponent exponent, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? formatProvider)
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
