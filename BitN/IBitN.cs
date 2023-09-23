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
