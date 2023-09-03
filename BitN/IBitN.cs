using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace BitN;

internal interface IBitN<TSelf, TBacking> : INumberBase<TSelf>
    where TSelf : IBitN<TSelf, TBacking>
    where TBacking : struct, IBinaryInteger<TBacking>
{

    //-------------------------------
    // Span read/write helper methods
    //-------------------------------
    protected static bool TryWriteBigEndianHelper(TBacking value, Span<byte> destination, out int bytesWritten)
        => value.TryWriteBigEndian(destination, out bytesWritten);

    protected static bool TryWriteLittleEndianHelper(TBacking value, Span<byte> destination, out int bytesWritten)
        => value.TryWriteLittleEndian(destination, out bytesWritten);

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
