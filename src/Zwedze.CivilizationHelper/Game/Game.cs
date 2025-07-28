using System.Collections.Immutable;
using System.Text;
using Zwedze.CivilizationHelper.DistrictManagement;
using Zwedze.CivilizationHelper.DistrictManagement.Discount;
using Zwedze.CivilizationHelper.Eras;

namespace Zwedze.CivilizationHelper.Game;

public interface IGame
{
    ImmutableArray<IDistrict> Districts { get; }

    void Unlock(DistrictKey district);
    void Lock(DistrictKey district);
    bool IsUnlocked(DistrictKey district);

    void Place(DistrictKey district);
    void Unplace(DistrictKey district);

    void Complete(DistrictKey district);
    void Remove(DistrictKey district);
}

internal sealed class Game(IPlayerDistricts playerDistricts, IUnlockedDistricts unlockedDistricts, IDistrictDiscount districtDiscount, IDistrictFactory districtFactory) : IGame
{
    private readonly List<IDistrict> _districts =
    [
        ..DistrictEra.List
            .SelectMany(x => x.Value.Select(d => new { DistrictKey = d, Era = x.Key }))
            .Select(x => districtFactory.Create(x.DistrictKey, (int)x.Era))
    ];

    public ImmutableArray<IDistrict> Districts
    {
        get
        {
            return
            [
                ..
                _districts
                    .OrderByDescending(x => x.IsUnlocked)
                    .ThenByDescending(x => x.IsDiscounted)
                    .ThenBy(x => x.Era)
            ];
        }
    }

    public void Unlock(DistrictKey district)
    {
        unlockedDistricts.AddDistrict(district);
    }

    public void Lock(DistrictKey district)
    {
        unlockedDistricts.RemoveDistrict(district);
    }

    public bool IsUnlocked(DistrictKey district) => unlockedDistricts.IsUnlocked(district);

    public void Place(DistrictKey district)
    {
        playerDistricts.PlaceDistrict(district);
    }

    public void Complete(DistrictKey district)
    {
        playerDistricts.CompleteDistrict(district);
    }

    public void Unplace(DistrictKey district)
    {
        playerDistricts.UnplaceDistrict(district);
    }

    public void Remove(DistrictKey district)
    {
        playerDistricts.RemoveDistrict(district);
    }

    public int GetPlacedCount(DistrictKey district) => playerDistricts.GetPlacedCountFor(district);

    public int GetCompletedCount(DistrictKey district) => playerDistricts.GetCompletedCountFor(district);

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Unlocked Districts: {unlockedDistricts}");
        sb.AppendLine($"Player Districts: {playerDistricts}");
        return sb.ToString();
    }
}
