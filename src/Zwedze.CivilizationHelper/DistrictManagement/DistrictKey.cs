namespace Zwedze.CivilizationHelper.DistrictManagement;

public readonly struct DistrictKey(string value) : IEquatable<DistrictKey>, IComparable<DistrictKey>
{
    private readonly string _value = value;

    public static explicit operator string(DistrictKey id) => id._value;

    public static explicit operator DistrictKey(string value) => new(value);

    public static bool operator ==(DistrictKey left, DistrictKey right) => Equals(left, right);

    public static bool operator !=(DistrictKey left, DistrictKey right) => !Equals(left, right);

    public bool Equals(DistrictKey other) => _value == other._value;

    public override bool Equals(object? obj) => obj is DistrictKey other && Equals(other);

    public override int GetHashCode() => _value.GetHashCode();

    public override string ToString() => _value;

    public int CompareTo(DistrictKey other) => string.Compare(_value, other._value, StringComparison.Ordinal);
}
