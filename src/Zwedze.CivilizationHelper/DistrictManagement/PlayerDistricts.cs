namespace Zwedze.CivilizationHelper.DistrictManagement;

internal interface IPlayerDistricts
{
    int GetTotalCompletedDistrictsCount();

    void PlaceDistrict(DistrictKey district);
    void UnplaceDistrict(DistrictKey district);
    void CompleteDistrict(DistrictKey district);
    void RemoveDistrict(DistrictKey district);
    int GetPlacedCountFor(DistrictKey district);
    int GetCompletedCountFor(DistrictKey district);
}

internal sealed class PlayerDistricts(IUnlockedDistricts unlockedDistricts) : IPlayerDistricts
{
    private readonly List<DistrictPlacement> _districtsPlacements = new();

    public int GetTotalCompletedDistrictsCount()
    {
        return _districtsPlacements
            // In captured cities, districts that have not been unlocked do not count.
            .Where(x => unlockedDistricts.IsUnlocked(x.District))
            .Count(x => x.IsCompleted);
    }

    public void PlaceDistrict(DistrictKey district)
    {
        _districtsPlacements.Add(new DistrictPlacement(district));
    }

    public void UnplaceDistrict(DistrictKey district)
    {
        var firstFoundIncompletePlacement = _districtsPlacements
            .FirstOrDefault(x => x.District == district && !x.IsCompleted);
        if (firstFoundIncompletePlacement is null)
        {
            throw new InvalidOperationException($"There is no district {district} placement not completed to remove.");
        }
        _districtsPlacements.Remove(firstFoundIncompletePlacement);
    }

    public void CompleteDistrict(DistrictKey district)
    {
        var firstFoundIncompletePlacement = _districtsPlacements
            .FirstOrDefault(x => x.District == district && !x.IsCompleted);
        if (firstFoundIncompletePlacement is null)
        {
            throw new InvalidOperationException($"There is no '{district}' district placement to complete.");
        }
        firstFoundIncompletePlacement.MarkCompleted();
    }

    public void RemoveDistrict(DistrictKey district)
    {
        var firstFoundCompletePlacement = _districtsPlacements
            .FirstOrDefault(x => x.District == district && x.IsCompleted);
        if (firstFoundCompletePlacement is null)
        {
            throw new InvalidOperationException($"There is no '{district}' district placement completed.");
        }
        _districtsPlacements.Remove(firstFoundCompletePlacement);
    }

    public int GetPlacedCountFor(DistrictKey district)
    {
        return _districtsPlacements.Count(x => x.District == district);
    }

    public int GetCompletedCountFor(DistrictKey district)
    {
        return _districtsPlacements
            .Where(x => x.IsCompleted)
            .Count(x => x.District == district);
    }

    public override string ToString()
    {
        var debug = _districtsPlacements
            .GroupBy(x => x.District)
            .Select(x => $"{x.Key} ({x.Count(d => d.IsCompleted)}/{x.Count()})");

        return string.Join(',', debug);
    }
}
