using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace BitN;

internal interface IBitN<TSelf, TBacking> :
    IBinaryInteger<TSelf>
    where TSelf : IBitN<TSelf, TBacking>, IMinMaxValue<TSelf>
    where TBacking : struct, IBinaryInteger<TBacking>
{
    public abstract static implicit operator TBacking(TSelf value);
    public abstract static explicit operator TSelf(TBacking b);
    public abstract static explicit operator checked TSelf(TBacking b);

    //-------------------------------
    // Span read/write helper methods
    //-------------------------------
    protected static bool TryWriteBigEndianHelper(TBacking value, Span<byte> destination, out int bytesWritten)
        => value.TryWriteBigEndian(destination, out bytesWritten);

    protected static bool TryWriteLittleEndianHelper(TBacking value, Span<byte> destination, out int bytesWritten)
        => value.TryWriteLittleEndian(destination, out bytesWritten);

    protected static bool TryReadBigEndianHelper(ReadOnlySpan<byte> source, bool isUnsigned, out TSelf value)
    {
        var success = TBacking.TryReadBigEndian(source, isUnsigned, out TBacking backingValue);
        value = (TSelf)backingValue;
        return success;
    }

    protected static bool TryReadLittleEndianHelper(ReadOnlySpan<byte> source, bool isUnsigned, out TSelf value)
    {
        var success = TBacking.TryReadLittleEndian(source, isUnsigned, out TBacking backingValue);
        value = (TSelf)backingValue;
        return success;
    }

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

    protected static bool TryConvertFromTruncatingHelper<TOther>(TOther value, out TSelf result)
        where TOther : INumberBase<TOther>
    {
        var success = TBacking.TryConvertFromTruncating(value, out TBacking backingResult); //will fail if cant convert to backing
        result = (TSelf)backingResult;
        return success;
    }

    protected static bool TryConvertFromCheckedHelper<TOther>(TOther value, out TSelf result)
        where TOther : INumberBase<TOther>
    {
        var success = TBacking.TryConvertFromChecked(value, out TBacking backingResult); //will fail if cant convert to backing
        result = checked((TSelf)backingResult); //will fail if backing cant convert to BitN
        return success;
    }

    protected static bool TryConvertFromSaturatingHelper<TOther>(TOther value, out TSelf result)
        where TOther : INumberBase<TOther>
    {
        var success = TBacking.TryConvertFromSaturating(value, out TBacking backingResult); //will fail if cant convert to backing
        backingResult = (backingResult >= TSelf.MaxValue) ? TSelf.MaxValue :
                        (backingResult < TSelf.MinValue) ? TSelf.MinValue : backingResult;
        result = (TSelf)backingResult;
        return success;
    }
}
