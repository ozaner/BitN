using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace BitN;

internal interface IBitHelper<TSelf, TBacking> : INumberBase<TSelf>
    where TSelf : IBitHelper<TSelf, TBacking>
    where TBacking : struct, IBinaryInteger<TBacking>
{

    //-------------------------------
    // Span read/write helper methods
    //-------------------------------
    protected static bool TryReadBigEndianHelper(ReadOnlySpan<byte> source, bool isUnsigned, out TBacking value)
        => TBacking.TryReadBigEndian(source, isUnsigned, out value);

    protected static bool TryReadLittleEndianHelper(ReadOnlySpan<byte> source, bool isUnsigned, out TBacking value)
        => TBacking.TryReadLittleEndian(source, isUnsigned, out value);


    //-------------------------------
    // TryConvert helper methods
    //-------------------------------
    // Implementation forwarded from backing type, with the 'from'
    // methods needing a bit of extra work since BitN ⊆ backing
    protected static bool TryConvertToTruncatingHelper<TOther>(TBacking value, [MaybeNullWhen(false)] out TOther result)
        where TOther : INumberBase<TOther> => TBacking.TryConvertToTruncating(value, out result);

    protected static bool TryConvertToCheckedHelper<TOther>(TBacking value, [MaybeNullWhen(false)] out TOther result)
        where TOther : INumberBase<TOther> => TBacking.TryConvertToChecked(value, out result);

    protected static bool TryConvertToSaturatingHelper<TOther>(TBacking value, [MaybeNullWhen(false)] out TOther result)
        where TOther : INumberBase<TOther> => TBacking.TryConvertToSaturating(value, out result);

    protected static bool TryConvertFromTruncatingHelper<TOther>(TOther value, out TBacking result)
        where TOther : INumberBase<TOther> => TBacking.TryConvertFromTruncating(value, out result);

    protected static bool TryConvertFromCheckedHelper<TOther>(TOther value, out TBacking result)
        where TOther : INumberBase<TOther> => TBacking.TryConvertFromChecked(value, out result);

    protected static bool TryConvertFromSaturatingHelper<TOther>(TOther value, out TBacking result)
        where TOther : INumberBase<TOther> => TBacking.TryConvertFromSaturating(value, out result);
}

internal static class BitNUtil
{
    //Creates a uint with the first {offset} bits being 0s,
    //the next {length} bits being 1s, and the rest being 0s.
    //1111111100000000
    //{length}{offset}
    //If {offset} is > 32, offset is % 32.
    public static uint CreateBitMask(int length, int offset) => ((1u << length) - 1) << offset;

    public static uint SetValueAtOffset(uint backingValue, uint value, int length, int offset)
    {
        var mask = ~CreateBitMask(length, offset);//11110001111
        var newVal = backingValue & mask;//xxxx000xxxx
        return newVal | (value << offset);//xxxxyyyxxxx
    }

    public static bool OutOfBounds(int spanLength, int byteCount, int bitCount, int offset)
        => spanLength < byteCount || offset + bitCount > byteCount * 8;

    //No need to check span length bc. the read/write impls in BitNRef use 
    public static void ThrowIfOutOfBounds(int spanLength, int byteCount, int bitCount, int offset)
    {
        if (OutOfBounds(spanLength, byteCount, bitCount, offset))
        {
            throw new ArgumentOutOfRangeException(nameof(offset),
                "The number of bits plus the offset must not exceed the number of bits in the backing type.");
        }
    }
}
