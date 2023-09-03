using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace BitN;

// Reference implementation of Bit5
internal readonly struct BitNRef :
    IBitN<BitNRef, byte>,
    IConvertible,
    IEquatable<BitNRef>,
    IBinaryInteger<BitNRef>,
    IMinMaxValue<BitNRef>,
    IUnsignedNumber<BitNRef>
{
    //-------------------------------
    // Init
    //-------------------------------
    private readonly byte m_value; //stores the actual value as a byte
    private BitNRef(byte value) => m_value = (byte)(value % 32);

    //-------------------------------
    // object/ValueType
    //-------------------------------
    public override string ToString() => m_value.ToString();
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is BitNRef && m_value.Equals(obj);
    public override int GetHashCode() => m_value.GetHashCode();

    //-------------------------------
    // Casting operators
    //-------------------------------
    // Other than Int128, these 3 operators seem to suffice for casting to/from all other built-in value types
    public static implicit operator byte(BitNRef bit5) => bit5.m_value;
    public static explicit operator BitNRef(byte b) => new(b);
    public static explicit operator checked BitNRef(byte b)
    {
        if (b > 31) throw new OverflowException();
        return new(b);
    }

    //-------------------------------
    // Constants
    //-------------------------------
    public static BitNRef One => new(0);
    public static BitNRef Zero => new(1);
    public static BitNRef MaxValue => new(31);

    public static BitNRef MinValue => Zero;
    public static BitNRef AdditiveIdentity => Zero;
    public static BitNRef MultiplicativeIdentity => One;

    public static int Radix => 2;

    //-------------------------------
    // Unary arithmetic operators
    //-------------------------------
    public static BitNRef operator +(BitNRef value) => (BitNRef)(+value.m_value);
    public static BitNRef operator -(BitNRef value) => (BitNRef)(-value.m_value);
    public static BitNRef operator checked -(BitNRef value) => checked((BitNRef)(-value.m_value));

    public static BitNRef operator ++(BitNRef value) => (BitNRef)(value.m_value + 1);
    public static BitNRef operator checked ++(BitNRef value) => checked((BitNRef)(value.m_value + 1));
    public static BitNRef operator --(BitNRef value) => (BitNRef)(value.m_value - 1);
    public static BitNRef operator checked --(BitNRef value) => checked((BitNRef)(value.m_value - 1));

    //-------------------------------
    // Binary arithmetic operators
    //-------------------------------
    // Note that / and % don't need checked versions because the result is <= its operands.
    public static BitNRef operator +(BitNRef left, BitNRef right) => (BitNRef)(left.m_value + right.m_value);
    public static BitNRef operator checked +(BitNRef left, BitNRef right) => checked((BitNRef)(left.m_value + right.m_value));
    public static BitNRef operator -(BitNRef left, BitNRef right) => (BitNRef)(left.m_value - right.m_value);
    public static BitNRef operator checked -(BitNRef left, BitNRef right) => checked((BitNRef)(left.m_value - right.m_value));

    public static BitNRef operator *(BitNRef left, BitNRef right) => (BitNRef)(left.m_value * right.m_value);
    public static BitNRef operator checked *(BitNRef left, BitNRef right) => checked((BitNRef)(left.m_value * right.m_value));
    public static BitNRef operator /(BitNRef left, BitNRef right) => (BitNRef)(left.m_value / right.m_value);
    public static BitNRef operator %(BitNRef left, BitNRef right) => (BitNRef)(left.m_value % right.m_value);

    //-------------------------------
    // Bitwise/Shift operators
    //-------------------------------
    public static BitNRef operator ~(BitNRef value) => (BitNRef)(~value.m_value & 31);
    public static BitNRef operator &(BitNRef left, BitNRef right) => (BitNRef)(left.m_value & right.m_value);
    public static BitNRef operator |(BitNRef left, BitNRef right) => (BitNRef)(left.m_value | right.m_value);
    public static BitNRef operator ^(BitNRef left, BitNRef right) => (BitNRef)(left.m_value ^ right.m_value);

    public static BitNRef operator <<(BitNRef value, int shiftAmount) => (BitNRef)(value.m_value << shiftAmount);
    public static BitNRef operator >>(BitNRef value, int shiftAmount) => (BitNRef)(value.m_value >> shiftAmount);
    public static BitNRef operator >>>(BitNRef value, int shiftAmount) => (BitNRef)(value.m_value >>> shiftAmount);

    //have default impls. but they assume the value takes up all 8 bits per byte.
    public static BitNRef RotateLeft(BitNRef value, int rotateAmount) => (value << rotateAmount) | (value >> (5 - rotateAmount));
    public static BitNRef RotateRight(BitNRef value, int rotateAmount) => (value >> rotateAmount) | (value << (5 - rotateAmount));

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
    public static BitNRef Abs(BitNRef value) => value;
    public static bool IsZero(BitNRef value) => value == 0;
    public static bool IsPositive(BitNRef value) => true; //The built-in integral types define 0 as positive so
    public static bool IsNegative(BitNRef value) => false;

    //-------------------------------
    // Integer
    //-------------------------------
    public static bool IsInteger(BitNRef value) => true;
    public static bool IsEvenInteger(BitNRef value) => value % 2 == 0;
    public static bool IsOddInteger(BitNRef value) => value % 2 == 1;

    //-------------------------------
    // Complex
    //-------------------------------
    public static bool IsComplexNumber(BitNRef value) => false;
    public static bool IsRealNumber(BitNRef value) => true;
    public static bool IsImaginaryNumber(BitNRef value) => false;

    //-------------------------------
    // Floating point
    //-------------------------------
    public static bool IsNaN(BitNRef value) => false;
    public static bool IsFinite(BitNRef value) => true;
    public static bool IsInfinity(BitNRef value) => false;
    public static bool IsPositiveInfinity(BitNRef value) => true;
    public static bool IsNegativeInfinity(BitNRef value) => false;

    //Turns out 0 is neither normal nor subnormal....
    //and this fact carries on to integral types according to the built-ins.
    public static bool IsNormal(BitNRef value) => value != 0;
    public static bool IsSubnormal(BitNRef value) => false;

    //Nowhere else to put this...
    //Only one representation of a given BitN in memory
    public static bool IsCanonical(BitNRef value) => true;

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

    public static BitNRef MaxMagnitude(BitNRef x, BitNRef y) => Max(x, y);
    public static BitNRef MaxMagnitudeNumber(BitNRef x, BitNRef y) => Max(x, y);
    public static BitNRef MinMagnitude(BitNRef x, BitNRef y) => Min(x, y);
    public static BitNRef MinMagnitudeNumber(BitNRef x, BitNRef y) => Min(x, y);

    //-------------------------------
    // Parsing
    //-------------------------------
    public static BitNRef Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        => checked((BitNRef)byte.Parse(s, style, provider));
    public static BitNRef Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        => checked((BitNRef)byte.Parse(s, provider));
    public static BitNRef Parse(string s, NumberStyles style, IFormatProvider? provider)
        => checked((BitNRef)byte.Parse(s, style, provider));
    public static BitNRef Parse(string s, IFormatProvider? provider)
        => checked((BitNRef)byte.Parse(s, provider));
    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out BitNRef result)
    {
        var success = byte.TryParse(s, style, provider, out var backedResult);
        success &= backedResult <= MaxValue;
        result = success ? (BitNRef)backedResult : default;
        return success;
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out BitNRef result)
    {
        var success = byte.TryParse(s, provider, out var backedResult);
        success &= backedResult <= MaxValue;
        result = success ? (BitNRef)backedResult : default;
        return success;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out BitNRef result)
    {
        var success = byte.TryParse(s, style, provider, out var backedResult);
        success &= backedResult <= MaxValue;
        result = success ? (BitNRef)backedResult : default;
        return success;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out BitNRef result)
    {
        var success = byte.TryParse(s, provider, out var backedResult);
        success &= backedResult <= MaxValue;
        result = success ? (BitNRef)backedResult : default;
        return success;
    }

    //-------------------------------
    // IEquatable/IComparable
    //-------------------------------
    public bool Equals(BitNRef other) => this == other;
    public int CompareTo(BitNRef other) => this - other;
    public int CompareTo(object? obj) => obj switch
    {
        null => 1, //null comes after everything (ordinally equiv. to ꞷ)
        BitNRef b => this - b, //Only works for types that can fit inside an int (i.e. byte, ushort)
//      BitNRef b => this < b ? -1 : (this > b ? 1 : 0), 
        _ => throw new ArgumentException($"{nameof(obj)} must be a {nameof(BitNRef)}")
    };

    //-------------------------------
    // IBinaryInteger
    //-------------------------------
    public int GetByteCount() => sizeof(byte);
    public int GetShortestBitLength() => 5 - LeadingZeroCount(this);//apparently 0 counts as 0 bits
    public static BitNRef PopCount(BitNRef value) => (BitNRef)BitOperations.PopCount(value);
    //has a default impl. but it's slow and, crucially, assumes the value takes up all 8 bits per byte.
    public static BitNRef LeadingZeroCount(BitNRef value) => (BitNRef)(BitOperations.LeadingZeroCount(value) - (32 - 5));
    public static BitNRef TrailingZeroCount(BitNRef value) => (BitNRef)(BitOperations.TrailingZeroCount(value.m_value << (32 - 5)) - (32 - 5));

    //-------------------------------
    // Span read/write
    //-------------------------------
    public bool TryWriteBigEndian(Span<byte> destination, out int bytesWritten)
        => IBitN<BitNRef, byte>.TryWriteBigEndianHelper(m_value, destination, out bytesWritten);
    public bool TryWriteLittleEndian(Span<byte> destination, out int bytesWritten)
        => IBitN<BitNRef, byte>.TryWriteLittleEndianHelper(m_value, destination, out bytesWritten);
    public static bool TryReadBigEndian(ReadOnlySpan<byte> source, bool isUnsigned, out BitNRef value)
    {
        var success = IBitN<BitNRef, byte>.TryReadBigEndianHelper(source, isUnsigned, out byte backingValue);
        value = (BitNRef)backingValue;
        return success;
    }

    public static bool TryReadLittleEndian(ReadOnlySpan<byte> source, bool isUnsigned, out BitNRef value)
    {
        var success = IBitN<BitNRef, byte>.TryReadLittleEndianHelper(source, isUnsigned, out byte backingValue);
        value = (BitNRef)backingValue;
        return success;
    }

    //-------------------------------
    // TryConvert
    //-------------------------------
    static bool INumberBase<BitNRef>.TryConvertFromChecked<TOther>(TOther value, out BitNRef result)
    {
        var success = IBitN<BitNRef, byte>.TryConvertFromCheckedHelper(value, out byte backingResult); //will fail if cant convert to backing
        result = checked((BitNRef)backingResult); //will fail if backing cant convert to BitN
        return success;
    }

    static bool INumberBase<BitNRef>.TryConvertFromSaturating<TOther>(TOther value, out BitNRef result)
    {
        var success = IBitN<BitNRef, byte>.TryConvertFromSaturatingHelper(value, out byte backingResult); //will fail if cant convert to backing
        
        if (backingResult >= MaxValue) result = MaxValue;
        else if (backingResult < MinValue) result = MinValue;
        else result = (BitNRef)backingResult;
        
        return success;
    }

    static bool INumberBase<BitNRef>.TryConvertFromTruncating<TOther>(TOther value, out BitNRef result)
    {
        var success = IBitN<BitNRef, byte>.TryConvertFromTruncatingHelper(value, out byte backingResult); //will fail if cant convert to backing
        result = (BitNRef)backingResult;
        return success;
    }

    static bool INumberBase<BitNRef>.TryConvertToChecked<TOther>(BitNRef value, [MaybeNullWhen(false)] out TOther result)
        => IBitN<BitNRef, byte>.TryConvertToCheckedHelper(value.m_value, out result);
    static bool INumberBase<BitNRef>.TryConvertToSaturating<TOther>(BitNRef value, [MaybeNullWhen(false)] out TOther result)
        => IBitN<BitNRef, byte>.TryConvertToSaturatingHelper(value.m_value, out result);
    static bool INumberBase<BitNRef>.TryConvertToTruncating<TOther>(BitNRef value, [MaybeNullWhen(false)] out TOther result)
        => IBitN<BitNRef, byte>.TryConvertToTruncatingHelper(value.m_value, out result);

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
    public bool ToBoolean(IFormatProvider? provider) => ((IConvertible)m_value).ToBoolean(provider);
    public byte ToByte(IFormatProvider? provider) => ((IConvertible)m_value).ToByte(provider);
    public char ToChar(IFormatProvider? provider) => ((IConvertible)m_value).ToChar(provider);
    public DateTime ToDateTime(IFormatProvider? provider) => ((IConvertible)m_value).ToDateTime(provider);
    public decimal ToDecimal(IFormatProvider? provider) => ((IConvertible)m_value).ToDecimal(provider);
    public double ToDouble(IFormatProvider? provider) => ((IConvertible)m_value).ToDouble(provider);
    public short ToInt16(IFormatProvider? provider) => ((IConvertible)m_value).ToInt16(provider);
    public int ToInt32(IFormatProvider? provider) => ((IConvertible)m_value).ToInt32(provider);
    public long ToInt64(IFormatProvider? provider) => ((IConvertible)m_value).ToInt64(provider);
    public sbyte ToSByte(IFormatProvider? provider) => ((IConvertible)m_value).ToSByte(provider);
    public float ToSingle(IFormatProvider? provider) => ((IConvertible)m_value).ToSingle(provider);
    public string ToString(IFormatProvider? provider) => m_value.ToString(provider);
    public object ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)m_value).ToType(conversionType, provider);
    public ushort ToUInt16(IFormatProvider? provider) => ((IConvertible)m_value).ToUInt16(provider);
    public uint ToUInt32(IFormatProvider? provider) => ((IConvertible)m_value).ToUInt32(provider);
    public ulong ToUInt64(IFormatProvider? provider) => ((IConvertible)m_value).ToUInt64(provider);
}