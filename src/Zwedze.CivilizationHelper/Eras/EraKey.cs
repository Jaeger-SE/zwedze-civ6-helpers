namespace Zwedze.CivilizationHelper.Eras;

public readonly struct EraKey(int value) : IEquatable<EraKey>, IComparable<EraKey>
{
    private readonly int _value = value;

    public static explicit operator int(EraKey id) => id._value;

    public static explicit operator EraKey(int value) => new(value);

    public static bool operator ==(EraKey left, EraKey right) => Equals(left, right);

    public static bool operator !=(EraKey left, EraKey right) => !Equals(left, right);

    public bool Equals(EraKey other) => _value == other._value;

    public override bool Equals(object? obj) => obj is EraKey other && Equals(other);

    public override int GetHashCode() => _value.GetHashCode();

    public override string ToString() => _value.ToString();

    public int CompareTo(EraKey other) => _value - other._value;
}
