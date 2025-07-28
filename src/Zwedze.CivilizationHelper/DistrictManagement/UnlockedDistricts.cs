namespace Zwedze.CivilizationHelper.DistrictManagement;

internal interface IUnlockedDistricts
{
    int Count { get; }

    void AddDistrict(DistrictKey district);
    void RemoveDistrict(DistrictKey district);
    bool IsUnlocked(DistrictKey district);
}

internal class UnlockedDistricts : IUnlockedDistricts
{
    private readonly HashSet<DistrictKey> _districts = [];

    public int Count => _districts.Count;

    public void AddDistrict(DistrictKey district)
    {
        _districts.Add(district);
    }

    public void RemoveDistrict(DistrictKey district)
    {
        _districts.Remove(district);
    }

    public bool IsUnlocked(DistrictKey district)
    {
        return _districts.Any(x => x == district);
    }

    public override string ToString() => string.Join(',', _districts);
}
