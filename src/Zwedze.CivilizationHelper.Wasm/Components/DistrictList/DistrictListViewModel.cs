using System.Collections.Immutable;
using System.Reactive.Linq;
using MudBlazor;
using Zwedze.CivilizationHelper.DistrictManagement;
using Zwedze.CivilizationHelper.Wasm.States;
using Zwedze.CivilizationHelper.Wasm.States.Container;
using Zwedze.Framework.Blazor.Reactive.SubscriptionManager;

namespace Zwedze.CivilizationHelper.Wasm.Components.DistrictList;

public interface IDistrictListViewModelFactory
{
    DistrictListViewModel Create(ISubscriptionManager subscriptionManager);
}

internal sealed class DistrictListViewModelFactory(StateContainer<AppState> appStateContainer, IGameService gameService, IDialogService dialogService) : IDistrictListViewModelFactory
{
    public DistrictListViewModel Create(ISubscriptionManager subscriptionManager) => new(appStateContainer, gameService, subscriptionManager, dialogService);
}

public sealed class DistrictListViewModel
{
    private readonly StateContainer<AppState> _appStateContainer;
    private readonly IDialogService _dialogService;
    private readonly IGameService _gameService;

    public ImmutableArray<DistrictKey> Districts = ImmutableArray<DistrictKey>.Empty;

    public DistrictListViewModel(StateContainer<AppState> appStateContainer, IGameService gameService, ISubscriptionManager subscriptionManager, IDialogService dialogService)
    {
        _appStateContainer = appStateContainer;
        _gameService = gameService;
        _dialogService = dialogService;

        _gameService.Restart();
        subscriptionManager.AddSubscriptionForUi(DistrictsSelector, (districts, _) => Districts = districts);
    }

    private IObservable<ImmutableArray<DistrictKey>> DistrictsSelector
    {
        get
        {
            return _appStateContainer.State
                .Select(appState => appState.Game.Districts)
                .Select(districts => districts
                    .Select(x => x.Key)
                    .ToImmutableArray()
                );
        }
    }

    public void Reset()
    {
        _gameService.Restart();
    }

    public Task OpenHelp()
    {
        return _dialogService.ShowAsync<HowTo.HowTo>(
            "How to",
            new DialogOptions
            {
                CloseButton = true,
                FullWidth = true,
                Position = DialogPosition.Center
            });
    }
}
