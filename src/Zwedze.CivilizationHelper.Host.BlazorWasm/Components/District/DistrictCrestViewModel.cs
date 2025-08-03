using System.Reactive.Linq;
using Zwedze.CivilizationHelper.DistrictManagement;
using Zwedze.CivilizationHelper.Host.BlazorWasm.States;
using Zwedze.CivilizationHelper.Host.BlazorWasm.States.Container;
using Zwedze.Framework.Blazor.Reactive.SubscriptionManager;

namespace Zwedze.CivilizationHelper.Host.BlazorWasm.Components.District;

public interface IDistrictCrestViewModelFactory
{
    DistrictCrestViewModel Create(ISubscriptionManager subscriptionManager, DistrictKey key);
}

internal sealed class DistrictCrestViewModelFactory(StateContainer<AppState> appStateContainer, IGameService gameService) : IDistrictCrestViewModelFactory
{
    public DistrictCrestViewModel Create(ISubscriptionManager subscriptionManager, DistrictKey key)
    {
        return new DistrictCrestViewModel(key, appStateContainer, gameService, subscriptionManager);
    }
}

public sealed class DistrictCrestViewModel
{
    private readonly StateContainer<AppState> _stateContainer;
    private readonly IGameService _gameService;
    private DistrictState _district = DistrictState.Init;

    public DistrictCrestViewModel(DistrictKey key, StateContainer<AppState> stateContainer, IGameService gameService, ISubscriptionManager subscriptionManager)
    {
        Key = key;
        _stateContainer = stateContainer;
        _gameService = gameService;

        subscriptionManager.AddSubscriptionForUi(DistrictSelector, (district, _) => _district = district);
    }

    private IObservable<DistrictState> DistrictSelector
    {
        get
        {
            return _stateContainer.State
                .Select(appState => appState.Game.Districts)
                .Where(districts=> districts.Any(d => d.Key == Key))
                .Select(districts => districts.Single(d => d.Key == Key)) ;
        }
    }

    public DistrictKey Key { get; }
    public string ImagePath => $"/img/districts/{Key}.png";
    public int PlacedCount => _district.PlacedCount;
    public int CompletedCount => _district.CompletedCount;
    public bool IsDiscounted => _district.IsDiscounted;
    public bool IsUnlocked => _district.IsUnlocked;

    public void OpenDetail()
    {
        if(IsUnlocked)
        {
            _gameService.SelectDistrictDetail(Key);
        }
    }

    public void CloseDetail()
    {
        _gameService.ResetDistrictDetail();
    }
}
