using Zwedze.CivilizationHelper.DistrictManagement.Discount;

namespace Zwedze.CivilizationHelper.DistrictManagement.Factories;

internal interface IDistrictFactory
{
    IDistrict Create(DistrictKey key, int era);
}

internal sealed class DistrictFactory(IPlayerDistricts playerDistricts, IUnlockedDistricts unlockedDistricts, IDistrictDiscount districtDiscount) : IDistrictFactory
{
    public IDistrict Create(DistrictKey key, int era) => new District(key, era, playerDistricts, unlockedDistricts, districtDiscount);
}
