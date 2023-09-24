using System.Numerics;

namespace BitN;

public interface IBitN<TSelf> :
    IConvertible,
    IEquatable<TSelf>,
    IBinaryInteger<TSelf>,
    IMinMaxValue<TSelf>,
    IUnsignedNumber<TSelf>
    where TSelf : IBitN<TSelf>
{
    static abstract int BitCount { get; }
}

public interface IBitNUInt32Sized<TSelf> : IBitN<TSelf>
    where TSelf : IBitNUInt32Sized<TSelf>
{
    static abstract TSelf ReadUInt32BackedLittleEndian(ReadOnlySpan<byte> source, int offset);
    static abstract TSelf ReadUInt32BackedBigEndian(ReadOnlySpan<byte> source, int offset);
    void WriteUInt32BackedLittleEndian(Span<byte> destination, int offset);
    void WriteUInt32BackedBigEndian(Span<byte> destination, int offset);

    static bool TryReadUInt32BackedLittleEndian(ReadOnlySpan<byte> source, int offset, out TSelf value)
    {
        value = TSelf.Zero;
        if (source.Length < sizeof(uint)) return false;
        value = TSelf.ReadUInt32BackedLittleEndian(source, offset);
        return true;
    }
    static bool TryReadUInt32BackedBigEndian(ReadOnlySpan<byte> source, int offset, out TSelf value)
    {
        value = TSelf.Zero;
        if (source.Length < sizeof(uint)) return false;
        value = TSelf.ReadUInt32BackedBigEndian(source, offset);
        return true;
    }
    bool TryWriteUInt32BackedLittleEndian(Span<byte> destination, int offset)
    {
        if (destination.Length < sizeof(uint)) return false;
        WriteUInt32BackedLittleEndian(destination, offset);
        return true;
    }
    bool TryWriteUInt32BackedBigEndian(Span<byte> destination, int offset)
    {
        if (destination.Length < sizeof(uint)) return false;
        WriteUInt32BackedBigEndian(destination, offset);
        return true;
    }
}

public interface IBitNUInt16Sized<TSelf> : IBitNUInt32Sized<TSelf>
    where TSelf : IBitNUInt16Sized<TSelf>
{
    static abstract TSelf ReadUInt16BackedLittleEndian(ReadOnlySpan<byte> source, int offset);
    static abstract TSelf ReadUInt16BackedBigEndian(ReadOnlySpan<byte> source, int offset);
    void WriteUInt16BackedLittleEndian(Span<byte> destination, int offset);
    void WriteUInt16BackedBigEndian(Span<byte> destination, int offset);

    static bool TryReadUInt16BackedLittleEndian(ReadOnlySpan<byte> source, int offset, out TSelf value)
    {
        value = TSelf.Zero;
        if (source.Length < sizeof(ushort)) return false;
        value = TSelf.ReadUInt16BackedLittleEndian(source, offset);
        return true;
    }
    static bool TryReadUInt16BackedBigEndian(ReadOnlySpan<byte> source, int offset, out TSelf value)
    {
        value = TSelf.Zero;
        if (source.Length < sizeof(ushort)) return false;
        value = TSelf.ReadUInt16BackedBigEndian(source, offset);
        return true;
    }
    bool TryWriteUInt16BackedLittleEndian(Span<byte> destination, int offset)
    {
        if (destination.Length < sizeof(ushort)) return false;
        WriteUInt16BackedLittleEndian(destination, offset);
        return true;
    }
    bool TryWriteUInt16BackedBigEndian(Span<byte> destination, int offset)
    {
        if (destination.Length < sizeof(ushort)) return false;
        WriteUInt16BackedBigEndian(destination, offset);
        return true;
    }
}

public interface IBitNByteSized<TSelf> : IBitNUInt16Sized<TSelf>
    where TSelf : IBitNByteSized<TSelf>
{
    static abstract TSelf ReadByteBacked(ReadOnlySpan<byte> source, int offset);
    void WriteByteBacked(Span<byte> destination, int offset);

    static bool TryReadByteBacked(ReadOnlySpan<byte> source, int offset, out TSelf value)
    {
        value = TSelf.Zero;
        if (source.Length < sizeof(byte)) return false;
        value = TSelf.ReadByteBacked(source, offset);
        return true;
    }
    bool TryWriteByteBacked(Span<byte> destination, int offset)
    {
        if (destination.Length < sizeof(byte)) return false;
        WriteByteBacked(destination, offset);
        return true;
    }
}
