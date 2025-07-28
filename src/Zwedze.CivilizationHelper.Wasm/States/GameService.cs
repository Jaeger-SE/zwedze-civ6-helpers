using System.Collections.Immutable;
using Zwedze.CivilizationHelper.DistrictManagement;
using Zwedze.CivilizationHelper.Game;
using Zwedze.CivilizationHelper.Wasm.States.Container;

namespace Zwedze.CivilizationHelper.Wasm.States;

public interface IGameService
{
    void Restart();

    void ToggleUnlock(DistrictKey districtKey);

    void Place(DistrictKey districtKey);
    void Unplace(DistrictKey key);

    void Complete(DistrictKey districtKey);
    void Remove(DistrictKey key);

    void SelectDistrictDetail(DistrictKey districtKey);
    void ResetDistrictDetail();
}

internal sealed class GameService(StateContainer<AppState> container, IGameFactory gameFactory) : IGameService
{
    private IGame _game = gameFactory.Create();

    public void Restart()
    {
        _game = gameFactory.Create();
        container.Update(_ => new AppState
        {
            Game = new GameState
            {
                Districts = MapDistricts(),
                OpenedDistrictKey = null
            }
        });
    }

    public void ToggleUnlock(DistrictKey districtKey)
    {
        if (!_game.IsUnlocked(districtKey))
        {
            _game.Unlock(districtKey);
        }
        else
        {
            _game.Lock(districtKey);
        }
        container.Update(x => new AppState
        {
            Game = x.Game with
            {
                Districts = MapDistricts()
            }
        });
    }

    public void Place(DistrictKey districtKey)
    {
        _game.Place(districtKey);
        container.Update(x => new AppState
        {
            Game = x.Game with
            {
                Districts = MapDistricts()
            }
        });
    }

    public void Unplace(DistrictKey key)
    {
        _game.Unplace(key);
        container.Update(x => new AppState
        {
            Game = x.Game with
            {
                Districts = MapDistricts()
            }
        });
    }

    public void Complete(DistrictKey districtKey)
    {
        _game.Complete(districtKey);
        container.Update(x => new AppState
        {
            Game = x.Game with
            {
                Districts = MapDistricts()
            }
        });
    }

    public void Remove(DistrictKey key)
    {
        _game.Remove(key);
        container.Update(x => new AppState
        {
            Game = x.Game with
            {
                Districts = MapDistricts()
            }
        });
    }

    public void SelectDistrictDetail(DistrictKey districtKey)
    {
        container.Update(x => new AppState
        {
            Game = x.Game with
            {
                OpenedDistrictKey = x.Game.OpenedDistrictKey == districtKey ? null : districtKey
            }
        });
    }

    public void ResetDistrictDetail()
    {
        container.Update(x => new AppState
        {
            Game = x.Game with
            {
                OpenedDistrictKey = null
            }
        });
    }

    private ImmutableArray<DistrictState> MapDistricts()
    {
        return
        [
            .._game.Districts.Select(d => new DistrictState
            {
                Key = d.Key,
                PlacedCount = d.PlacedCount,
                CompletedCount = d.CompletedCount,
                IsDiscounted = d.IsDiscounted,
                IsUnlocked = d.IsUnlocked,
                CanRemove = d.CompletedCount > 0,
                CanUnplace = d.PlacedCount > 0 && d.PlacedCount != d.CompletedCount
            })
        ];
    }
}
