using Zwedze.CivilizationHelper.DistrictManagement;
using Zwedze.CivilizationHelper.DistrictManagement.Discount;
using Zwedze.CivilizationHelper.DistrictManagement.Factories;

namespace Zwedze.CivilizationHelper.Game;

public interface IGameFactory
{
    public IGame Create();
}

internal sealed class GameFactory : IGameFactory
{
    public IGame Create()
    {
        var unlockedDistricts = new UnlockedDistricts();
        var playerDistricts = new PlayerDistricts(unlockedDistricts);
        var districtDiscount = new DistrictDiscount(playerDistricts, unlockedDistricts);
        var districtFactory = new DistrictFactory(playerDistricts, unlockedDistricts, districtDiscount);

        return new Game(playerDistricts, unlockedDistricts, districtFactory);
    }
}
