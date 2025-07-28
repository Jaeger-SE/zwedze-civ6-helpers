using Zwedze.CivilizationHelper.DistrictManagement.Discount;

namespace Zwedze.CivilizationHelper.DistrictManagement;

internal interface IDistrictFactory
{
    IDistrict Create(DistrictKey key, int era);
}

internal sealed class DistrictFactory(IPlayerDistricts playerDistricts, IUnlockedDistricts unlockedDistricts, IDistrictDiscount districtDiscount) : IDistrictFactory
{
    public IDistrict Create(DistrictKey key, int era) => new District(key, era, playerDistricts, unlockedDistricts, districtDiscount);
}

public interface IDistrict
{
    public int Era { get; }
    DistrictKey Key { get; }
    int CompletedCount { get; }
    bool IsDiscounted { get; }
    bool IsUnlocked { get; }
    int PlacedCount { get; }
}

internal sealed class District(DistrictKey key, int age, IPlayerDistricts playerDistricts, IUnlockedDistricts unlockedDistricts, IDistrictDiscount districtDiscount) : IDistrict
{
    public int Era { get; } = age;
    public DistrictKey Key => key;
    public int CompletedCount => playerDistricts.GetCompletedCountFor(Key);
    public bool IsDiscounted => IsUnlocked && districtDiscount.HasDiscountFor(Key);
    public bool IsUnlocked => unlockedDistricts.IsUnlocked(Key);
    public int PlacedCount => playerDistricts.GetPlacedCountFor(Key);
}
