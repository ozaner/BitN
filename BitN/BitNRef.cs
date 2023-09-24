#nullable enable //Auto-generated code must enable this explicitly

using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace BitN;

// $startrefcomments
// Reference implementation of Bit5
// - Template variables:
//   - $N -> Replace 5 w/ N
//   - $type -> replace byte w/ backing type
//   - $makepublic -> replace all "internal" w/ "public"
//   - $max -> replace 31 w/ 2^N-1
//   - $1byte -> ? do nothing : delete line
//   - $1or2byte -> ? do nothing : delete line
//   - $2byte -> ? replace first two chars with "  " : delete line
//   - $4byte -> ? replace first two chars with "  " : delete line
//   - $addbitncasts -> insert the BitN specific cast operators
// - After variables processed:
//   - Remove everything that follows "//$"
//   - Replace all BitNRef w/ "Bit{N}"
// $endrefcomments
internal readonly struct BitNRef ://$makepublic
    IBitNByteSized<BitNRef>,//$1byte
//  IBitNUInt16Sized<BitNRef>,//$2byte
//  IBitNUInt32Sized<BitNRef>,//$4byte
    IBitHelper<BitNRef, byte>//$type
{
    //-------------------------------
    // Init
    //-------------------------------
    private readonly byte m_value; //stores the actual value as a byte//$type
    private BitNRef(byte value) => m_value = (byte)(value & MaxValueAsBacking);//$type

    //-------------------------------
    // object/ValueType
    //-------------------------------
    public override string ToString() => m_value.ToString();
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is BitNRef && m_value.Equals(obj);
    public override int GetHashCode() => m_value.GetHashCode();

    //-------------------------------
    // Casting operators
    //-------------------------------
    public static implicit operator sbyte(BitNRef b) => (sbyte)b.m_value; //must be within range//$1byte
    public static implicit operator byte(BitNRef b) => b.m_value;//$1byte
    public static implicit operator short(BitNRef b) => (short)b.m_value;//$1or2byte
    public static implicit operator ushort(BitNRef b) => b.m_value;//$1or2byte
    public static implicit operator int(BitNRef b) => (int)b.m_value;
    public static implicit operator uint(BitNRef b) => b.m_value;
    public static implicit operator long(BitNRef b) => b.m_value;
    public static implicit operator ulong(BitNRef b) => b.m_value;

//  public static implicit operator BitNRef(byte b) => new(b);//$2byte
//  public static implicit operator BitNRef(ushort b) => new(b);//$4byte
    public static explicit operator BitNRef(byte b) => new(b);//$type
    public static explicit operator checked BitNRef(byte b) => CheckedCast(b);//$type
//  public static explicit operator BitNRef(int i) => new((uint)i);//$4byte
//  public static explicit operator checked BitNRef(int i) => CheckedCast(i);//$4byte
//  public static explicit operator BitNRef(long l) => new((uint)l);//$4byte
//  public static explicit operator checked BitNRef(long l) => CheckedCast(l);//$4byte

    //$addbitncasts
    private static BitNRef CheckedCast(long l)
    {
        if (l > MaxValueAsBacking) throw new OverflowException();
        return new((byte)l);
    }

    //-------------------------------
    // Constants
    //-------------------------------
    private const byte MaxValueAsBacking = 31;//$type$max
    private const int BitCount = 5;//$N
    private static readonly BitNRef One = new(1);
    private static readonly BitNRef Zero = new(0);

    public static readonly BitNRef MaxValue = new(MaxValueAsBacking);
    public static readonly BitNRef MinValue = Zero;

    //-------------------------------
    // Interface constants
    //-------------------------------
    static int IBitN<BitNRef>.BitCount => 5;
    static int INumberBase<BitNRef>.Radix => 2;
    static BitNRef INumberBase<BitNRef>.One => One;
    static BitNRef INumberBase<BitNRef>.Zero => Zero;
    static BitNRef IMinMaxValue<BitNRef>.MaxValue => MaxValue;
    static BitNRef IMinMaxValue<BitNRef>.MinValue => MaxValue;
    static BitNRef IAdditiveIdentity<BitNRef, BitNRef>.AdditiveIdentity => Zero;
    static BitNRef IMultiplicativeIdentity<BitNRef, BitNRef>.MultiplicativeIdentity => One;

    //-------------------------------
    // Unary arithmetic operators
    //-------------------------------
    public static BitNRef operator +(BitNRef value) => value;
    public static BitNRef operator -(BitNRef value) => (BitNRef)(0 - value.m_value);
    public static BitNRef operator checked -(BitNRef value) => throw new OverflowException();

    public static BitNRef operator ++(BitNRef value) => (BitNRef)(value.m_value + 1);
    public static BitNRef operator checked ++(BitNRef value) => checked((BitNRef)(value.m_value + 1));
    public static BitNRef operator --(BitNRef value) => (BitNRef)(value.m_value - 1);
    public static BitNRef operator checked --(BitNRef value) => checked((BitNRef)(value.m_value - 1));

    //-------------------------------
    // Binary arithmetic operators
    //-------------------------------
    // Note that / and % don't need checked versions because the result is <= its operands.
    static BitNRef IAdditionOperators<BitNRef, BitNRef, BitNRef>.operator +(BitNRef left, BitNRef right) => (BitNRef)(left.m_value + right.m_value);
    static BitNRef IAdditionOperators<BitNRef, BitNRef, BitNRef>.operator checked +(BitNRef left, BitNRef right) => checked((BitNRef)(left.m_value + right.m_value));
    static BitNRef ISubtractionOperators<BitNRef, BitNRef, BitNRef>.operator -(BitNRef left, BitNRef right) => (BitNRef)(left.m_value - right.m_value);
    static BitNRef ISubtractionOperators<BitNRef, BitNRef, BitNRef>.operator checked -(BitNRef left, BitNRef right) => checked((BitNRef)(left.m_value - right.m_value));

    static BitNRef IMultiplyOperators<BitNRef, BitNRef, BitNRef>.operator *(BitNRef left, BitNRef right) => (BitNRef)(left.m_value * right.m_value);
    static BitNRef IMultiplyOperators<BitNRef, BitNRef, BitNRef>.operator checked *(BitNRef left, BitNRef right) => checked((BitNRef)(left.m_value * right.m_value));
    static BitNRef IDivisionOperators<BitNRef, BitNRef, BitNRef>.operator /(BitNRef left, BitNRef right) => (BitNRef)(left.m_value / right.m_value);
    static BitNRef IModulusOperators<BitNRef, BitNRef, BitNRef>.operator %(BitNRef left, BitNRef right) => (BitNRef)(left.m_value % right.m_value);

    //-------------------------------
    // Bitwise/Shift operators
    //-------------------------------
    static BitNRef IBitwiseOperators<BitNRef, BitNRef, BitNRef>.operator ~(BitNRef value) => (BitNRef)(~value.m_value & MaxValueAsBacking);
    static BitNRef IBitwiseOperators<BitNRef, BitNRef, BitNRef>.operator &(BitNRef left, BitNRef right) => (BitNRef)(left.m_value & right.m_value);
    static BitNRef IBitwiseOperators<BitNRef, BitNRef, BitNRef>.operator |(BitNRef left, BitNRef right) => (BitNRef)(left.m_value | right.m_value);
    static BitNRef IBitwiseOperators<BitNRef, BitNRef, BitNRef>.operator ^(BitNRef left, BitNRef right) => (BitNRef)(left.m_value ^ right.m_value);

    static BitNRef IShiftOperators<BitNRef, int, BitNRef>.operator <<(BitNRef value, int shiftAmount) => (BitNRef)(value.m_value << shiftAmount);
    static BitNRef IShiftOperators<BitNRef, int, BitNRef>.operator >>(BitNRef value, int shiftAmount) => (BitNRef)(value.m_value >> shiftAmount);
    static BitNRef IShiftOperators<BitNRef, int, BitNRef>.operator >>>(BitNRef value, int shiftAmount) => (BitNRef)(value.m_value >>> shiftAmount);

    //have default impls. but they assume the value takes up all 8 bits per byte.
    public static BitNRef RotateLeft(BitNRef value, int rotateAmount) => (BitNRef)((value << rotateAmount) | (value >> (5 - rotateAmount)));//$N
    public static BitNRef RotateRight(BitNRef value, int rotateAmount) => (BitNRef)((value >> rotateAmount) | (value << (5 - rotateAmount)));//$N

    //-------------------------------
    // Boolean operators
    //-------------------------------
    // Implementation forwarded from int (due to implicit conversion) (byte does the same anyway)
    public static bool operator ==(BitNRef left, BitNRef right) => left.m_value == right.m_value;
    public static bool operator !=(BitNRef left, BitNRef right) => left.m_value != right.m_value;
    public static bool operator <(BitNRef left, BitNRef right) => left.m_value < right.m_value;
    public static bool operator >(BitNRef left, BitNRef right) => left.m_value > right.m_value;
    public static bool operator <=(BitNRef left, BitNRef right) => left.m_value <= right.m_value;
    public static bool operator >=(BitNRef left, BitNRef right) => left.m_value >= right.m_value;

    //-------------------------------
    // Sign related
    //-------------------------------
    static BitNRef INumberBase<BitNRef>.Abs(BitNRef value) => value;
    static bool INumberBase<BitNRef>.IsZero(BitNRef value) => value == 0;
    static bool INumberBase<BitNRef>.IsPositive(BitNRef value) => true; //The built-in integral types define 0 as positive so
    static bool INumberBase<BitNRef>.IsNegative(BitNRef value) => false;

    //-------------------------------
    // Integer
    //-------------------------------
    public static bool IsEvenInteger(BitNRef value) => value % 2 == 0;
    public static bool IsOddInteger(BitNRef value) => value % 2 == 1;

    static bool INumberBase<BitNRef>.IsInteger(BitNRef value) => true;

    //-------------------------------
    // Complex
    //-------------------------------
    static bool INumberBase<BitNRef>.IsComplexNumber(BitNRef value) => false;
    static bool INumberBase<BitNRef>.IsRealNumber(BitNRef value) => true;
    static bool INumberBase<BitNRef>.IsImaginaryNumber(BitNRef value) => false;

    //-------------------------------
    // Floating point
    //-------------------------------
    static bool INumberBase<BitNRef>.IsNaN(BitNRef value) => false;
    static bool INumberBase<BitNRef>.IsFinite(BitNRef value) => true;
    static bool INumberBase<BitNRef>.IsInfinity(BitNRef value) => false;
    static bool INumberBase<BitNRef>.IsPositiveInfinity(BitNRef value) => true;
    static bool INumberBase<BitNRef>.IsNegativeInfinity(BitNRef value) => false;

    //Turns out 0 is neither normal nor subnormal....
    //and this fact carries on to integral types according to the built-ins.
    static bool INumberBase<BitNRef>.IsNormal(BitNRef value) => value != 0;
    static bool INumberBase<BitNRef>.IsSubnormal(BitNRef value) => false;

    //Nowhere else to put this...
    //Only one representation of a given BitN in memory
    static bool INumberBase<BitNRef>.IsCanonical(BitNRef value) => true;

    //-------------------------------
    // Powers
    //-------------------------------
    public static bool IsPow2(BitNRef value) => BitOperations.IsPow2((uint)value);
    public static BitNRef Log2(BitNRef value) => (BitNRef)BitOperations.Log2(value);

    //-------------------------------
    // Min/Max
    //-------------------------------
    //has default impl. but this is faster (or at least more direct)
    public static BitNRef Max(BitNRef x, BitNRef y) => (BitNRef)Math.Max(x.m_value, y.m_value);
    public static BitNRef Min(BitNRef x, BitNRef y) => (BitNRef)Math.Min(x.m_value, y.m_value);

    static BitNRef INumberBase<BitNRef>.MaxMagnitude(BitNRef x, BitNRef y) => Max(x, y);
    static BitNRef INumberBase<BitNRef>.MaxMagnitudeNumber(BitNRef x, BitNRef y) => Max(x, y);
    static BitNRef INumberBase<BitNRef>.MinMagnitude(BitNRef x, BitNRef y) => Min(x, y);
    static BitNRef INumberBase<BitNRef>.MinMagnitudeNumber(BitNRef x, BitNRef y) => Min(x, y);

    //-------------------------------
    // Parsing
    //-------------------------------
    public static BitNRef Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        => checked((BitNRef)byte.Parse(s, style, provider));//$type
    public static BitNRef Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        => checked((BitNRef)byte.Parse(s, provider));//$type
    public static BitNRef Parse(string s, NumberStyles style, IFormatProvider? provider)
        => checked((BitNRef)byte.Parse(s, style, provider));//$type
    public static BitNRef Parse(string s, IFormatProvider? provider)
        => checked((BitNRef)byte.Parse(s, provider));//$type
    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out BitNRef result)
    {
        var success = byte.TryParse(s, style, provider, out var backedResult);//$type
        success &= backedResult <= MaxValue;
        result = success ? (BitNRef)backedResult : default;
        return success;
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out BitNRef result)
    {
        var success = byte.TryParse(s, provider, out var backedResult);//$type
        success &= backedResult <= MaxValue;
        result = success ? (BitNRef)backedResult : default;
        return success;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out BitNRef result)
    {
        var success = byte.TryParse(s, style, provider, out var backedResult);//$type
        success &= backedResult <= MaxValue;
        result = success ? (BitNRef)backedResult : default;
        return success;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out BitNRef result)
    {
        var success = byte.TryParse(s, provider, out var backedResult);//$type
        success &= backedResult <= MaxValue;
        result = success ? (BitNRef)backedResult : default;
        return success;
    }

    //-------------------------------
    // IEquatable/IComparable
    //-------------------------------
    public bool Equals(BitNRef other) => this == other;
    public int CompareTo(BitNRef other)
        => this - other; //Only works for types that can fit inside an int (i.e. byte, ushort)//$1or2byte
//      => this < other ? -1 : (this > other ? 1 : 0);//$4byte
    public int CompareTo(object? obj) => obj switch
    {
        null => 1, //null comes after everything (ordinally equiv. to ꞷ)
        BitNRef b => CompareTo(b), //forward more specific overload
        _ => throw new ArgumentException($"{nameof(obj)} must be a {nameof(BitNRef)}")
    };

    //-------------------------------
    // IBinaryInteger
    //-------------------------------
    public static BitNRef PopCount(BitNRef value) => (BitNRef)BitOperations.PopCount(value);
    //has a default impl. but it's slow and, crucially, assumes the value takes up all 8 bits per byte.
    public static BitNRef LeadingZeroCount(BitNRef value) => (BitNRef)(BitOperations.LeadingZeroCount(value) - (32 - BitCount));
    public static BitNRef TrailingZeroCount(BitNRef value) => (BitNRef)(BitOperations.TrailingZeroCount(value.m_value << (32 - BitCount)) - (32 - BitCount));
    
    int IBinaryInteger<BitNRef>.GetByteCount() => sizeof(byte);//$type
    int IBinaryInteger<BitNRef>.GetShortestBitLength() => BitCount - LeadingZeroCount(this);//apparently 0 counts as 0 bits

    //-------------------------------
    // Span read/write
    //-------------------------------
    bool IBinaryInteger<BitNRef>.TryWriteBigEndian(Span<byte> destination, out int bWritten)
        => (m_value as IBinaryInteger<byte>).TryWriteBigEndian(destination, out bWritten);//$type
    bool IBinaryInteger<BitNRef>.TryWriteLittleEndian(Span<byte> destination, out int bWritten)
        => (m_value as IBinaryInteger<byte>).TryWriteLittleEndian(destination, out bWritten);//$type

    static bool IBinaryInteger<BitNRef>.TryReadBigEndian(ReadOnlySpan<byte> source, bool isUnsigned, out BitNRef value)
    {
        var success = IBitHelper<BitNRef, byte>.TryReadBigEndianHelper(source, isUnsigned, out byte backingValue);//$type
        value = (BitNRef)backingValue;
        return success;
    }
    static bool IBinaryInteger<BitNRef>.TryReadLittleEndian(ReadOnlySpan<byte> source, bool isUnsigned, out BitNRef value)
    {
        var success = IBitHelper<BitNRef, byte>.TryReadLittleEndianHelper(source, isUnsigned, out byte backingValue);//$type
        value = (BitNRef)backingValue;
        return success;
    }

    //-------------------------------
    // TryConvert
    //-------------------------------
    static bool INumberBase<BitNRef>.TryConvertFromChecked<TOther>(TOther value, out BitNRef result)
    {
        var success = IBitHelper<BitNRef, byte>.TryConvertFromCheckedHelper(value, out byte backingResult); //will fail if cant convert to backing//$type
        result = checked((BitNRef)backingResult); //will fail if backing cant convert to BitN
        return success;
    }

    static bool INumberBase<BitNRef>.TryConvertFromSaturating<TOther>(TOther value, out BitNRef result)
    {
        var success = IBitHelper<BitNRef, byte>.TryConvertFromSaturatingHelper(value, out byte backingResult); //will fail if cant convert to backing//$type

        if (backingResult >= MaxValue) result = MaxValue;
        else if (backingResult < MinValue) result = MinValue;
        else result = (BitNRef)backingResult;
        
        return success;
    }

    static bool INumberBase<BitNRef>.TryConvertFromTruncating<TOther>(TOther value, out BitNRef result)
    {
        var success = IBitHelper<BitNRef, byte>.TryConvertFromTruncatingHelper(value, out byte backingResult); //will fail if cant convert to backing//$type
        result = (BitNRef)backingResult;
        return success;
    }

    static bool INumberBase<BitNRef>.TryConvertToChecked<TOther>(BitNRef value, [MaybeNullWhen(false)] out TOther result)
        => IBitHelper<BitNRef, byte>.TryConvertToCheckedHelper(value.m_value, out result);//$type
    static bool INumberBase<BitNRef>.TryConvertToSaturating<TOther>(BitNRef value, [MaybeNullWhen(false)] out TOther result)
        => IBitHelper<BitNRef, byte>.TryConvertToSaturatingHelper(value.m_value, out result);//$type
    static bool INumberBase<BitNRef>.TryConvertToTruncating<TOther>(BitNRef value, [MaybeNullWhen(false)] out TOther result)
        => IBitHelper<BitNRef, byte>.TryConvertToTruncatingHelper(value.m_value, out result);//$type

    //-------------------------------
    // IFormattable
    //-------------------------------
    // Implementation forwarded from backing type
    public string ToString(string? format, IFormatProvider? formatProvider)
        => m_value.ToString(format, formatProvider);
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => m_value.TryFormat(destination, out charsWritten, format, provider);

    //-------------------------------
    // IConvertible
    //-------------------------------
    // Implementation forwarded from backing type
    public TypeCode GetTypeCode() => m_value.GetTypeCode();
    public string ToString(IFormatProvider? provider) => m_value.ToString(provider);

    bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)m_value).ToBoolean(provider);
    byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)m_value).ToByte(provider);
    char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)m_value).ToChar(provider);
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)m_value).ToDateTime(provider);
    decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)m_value).ToDecimal(provider);
    double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)m_value).ToDouble(provider);
    short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)m_value).ToInt16(provider);
    int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)m_value).ToInt32(provider);
    long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)m_value).ToInt64(provider);
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)m_value).ToSByte(provider);
    float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)m_value).ToSingle(provider);
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)m_value).ToType(conversionType, provider);
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)m_value).ToUInt16(provider);
    uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)m_value).ToUInt32(provider);
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)m_value).ToUInt64(provider);

    //-------------------------------
    // IBitNByteSized//$1byte
    // IBitNUInt16Sized//$1or2byte
    // IBitNUInt32Sized
    //-------------------------------
    static BitNRef IBitNByteSized<BitNRef>.ReadByteBacked(ReadOnlySpan<byte> source, int offset)//$1byte
    {//$1byte
        BitNUtil.ThrowIfOutOfBounds(source.Length, sizeof(byte), BitCount, offset);//$1byte
        var backingVal = source[0];//$1byte
        var mask = BitNUtil.CreateBitMask(BitCount, offset);//$1byte
        return (BitNRef)((backingVal & mask) >> offset);//$1byte
    }//$1byte
    static BitNRef IBitNUInt16Sized<BitNRef>.ReadUInt16BackedLittleEndian(ReadOnlySpan<byte> source, int offset)//$1or2byte
    {//$1or2byte
        BitNUtil.ThrowIfOutOfBounds(source.Length, sizeof(ushort), BitCount, offset);//$1or2byte
        var backingVal = BinaryPrimitives.ReadUInt16LittleEndian(source);//$1or2byte
        var mask = BitNUtil.CreateBitMask(BitCount, offset);//$1or2byte
        return (BitNRef)((backingVal & mask) >> offset);//$1or2byte
    }//$1or2byte
    static BitNRef IBitNUInt16Sized<BitNRef>.ReadUInt16BackedBigEndian(ReadOnlySpan<byte> source, int offset)//$1or2byte
    {//$1or2byte
        BitNUtil.ThrowIfOutOfBounds(source.Length, sizeof(ushort), BitCount, offset);//$1or2byte
        var backingVal = BinaryPrimitives.ReadUInt16BigEndian(source);//$1or2byte
        var mask = BitNUtil.CreateBitMask(BitCount, offset);//$1or2byte
        return (BitNRef)((backingVal & mask) >> offset);//$1or2byte
    }//$1or2byte
    static BitNRef IBitNUInt32Sized<BitNRef>.ReadUInt32BackedLittleEndian(ReadOnlySpan<byte> source, int offset)
    {
        BitNUtil.ThrowIfOutOfBounds(source.Length, sizeof(uint), BitCount, offset);
        var backingVal = BinaryPrimitives.ReadUInt32LittleEndian(source);
        var mask = BitNUtil.CreateBitMask(BitCount, offset);
        return (BitNRef)((backingVal & mask) >> offset);
    }
    static BitNRef IBitNUInt32Sized<BitNRef>.ReadUInt32BackedBigEndian(ReadOnlySpan<byte> source, int offset)
    {
        BitNUtil.ThrowIfOutOfBounds(source.Length, sizeof(uint), BitCount, offset);
        var backingVal = BinaryPrimitives.ReadUInt32BigEndian(source);
        var mask = BitNUtil.CreateBitMask(BitCount, offset);
        return (BitNRef)((backingVal & mask) >> offset);
    }

    void IBitNByteSized<BitNRef>.WriteByteBacked(Span<byte> destination, int offset)//$1byte
    {//$1byte
        BitNUtil.ThrowIfOutOfBounds(destination.Length, sizeof(byte), BitCount, offset);//$1byte
        var backingVal = destination[0];//$1byte
        var newVal = BitNUtil.SetValueAtOffset(backingVal, this, BitCount, offset);//$1byte
        destination[0] = (byte)newVal;//$1byte
    }//$1byte
    void IBitNUInt16Sized<BitNRef>.WriteUInt16BackedLittleEndian(Span<byte> destination, int offset)//$1or2byte
    {//$1or2byte
        BitNUtil.ThrowIfOutOfBounds(destination.Length, sizeof(ushort), BitCount, offset);//$1or2byte
        var backingVal = BinaryPrimitives.ReadUInt16LittleEndian(destination);//$1or2byte
        var newVal = BitNUtil.SetValueAtOffset(backingVal, this, BitCount, offset);//$1or2byte
        BinaryPrimitives.WriteUInt16LittleEndian(destination, (ushort)newVal);//$1or2byte
    }//$1or2byte
    void IBitNUInt16Sized<BitNRef>.WriteUInt16BackedBigEndian(Span<byte> destination, int offset)//$1or2byte
    {//$1or2byte
        BitNUtil.ThrowIfOutOfBounds(destination.Length, sizeof(ushort), BitCount, offset);//$1or2byte
        var backingVal = BinaryPrimitives.ReadUInt16BigEndian(destination);//$1or2byte
        var newVal = BitNUtil.SetValueAtOffset(backingVal, this, BitCount, offset);//$1or2byte
        BinaryPrimitives.WriteUInt16BigEndian(destination, (ushort)newVal);//$1or2byte
    }//$1or2byte
    void IBitNUInt32Sized<BitNRef>.WriteUInt32BackedLittleEndian(Span<byte> destination, int offset)
    {
        BitNUtil.ThrowIfOutOfBounds(destination.Length, sizeof(uint), BitCount, offset);
        var backingVal = BinaryPrimitives.ReadUInt32LittleEndian(destination);
        var newVal = BitNUtil.SetValueAtOffset(backingVal, this, BitCount, offset);
        BinaryPrimitives.WriteUInt32LittleEndian(destination, newVal);
    }
    void IBitNUInt32Sized<BitNRef>.WriteUInt32BackedBigEndian(Span<byte> destination, int offset)
    {
        BitNUtil.ThrowIfOutOfBounds(destination.Length, sizeof(uint), BitCount, offset);
        var backingVal = BinaryPrimitives.ReadUInt32BigEndian(destination);
        var newVal = BitNUtil.SetValueAtOffset(backingVal, this, BitCount, offset);
        BinaryPrimitives.WriteUInt32BigEndian(destination, newVal);
    }
}
