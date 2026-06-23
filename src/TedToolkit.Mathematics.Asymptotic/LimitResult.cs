using System.Numerics;

namespace TedToolkit.Mathematics.Asymptotic;

public readonly record struct LimitResult<TValue>(LimitState State, TValue Value) :
    IFormattable
    where TValue : INumber<TValue>
{
    public override string ToString()
    {
        return ToString(null, null);
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        switch (State)
        {
            case LimitState.FINITE:
                return Value.ToString(format, formatProvider);
            case LimitState.POSITIVE_INFINITY:
                return "+∞";
            case LimitState.NEGATIVE_INFINITY:
                return "-∞";
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}