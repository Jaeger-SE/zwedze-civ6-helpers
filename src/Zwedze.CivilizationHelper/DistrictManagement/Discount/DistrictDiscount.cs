using System.Collections.Immutable;

namespace Zwedze.CivilizationHelper.DistrictManagement.Discount;

internal interface IDistrictDiscount
{
    bool HasDiscountFor(DistrictKey district);
}

internal sealed class DistrictDiscount(IPlayerDistricts playerDistricts, IUnlockedDistricts unlockedDistricts) : IDistrictDiscount
{
    private static readonly ImmutableArray<DistrictKey> SingletonDistricts;

    static DistrictDiscount()
    {
        SingletonDistricts =
        [
            DistrictKeys.GovernmentPlaza,
            DistrictKeys.DiplomaticQuarter
        ];
    }

    public bool HasDiscountFor(DistrictKey district)
    {
        if (unlockedDistricts.Count <= 1)
        {
            return false;
        }

        if (SingletonDistricts.Contains(district) && playerDistricts.GetPlacedCountFor(district) > 0)
        {
            return false;
        }

        float a = unlockedDistricts.Count;
        float b = playerDistricts.GetTotalCompletedDistrictsCount();
        float c = playerDistricts.GetPlacedCountFor(district);

        return b >= a && c < b / a;
    }
}
