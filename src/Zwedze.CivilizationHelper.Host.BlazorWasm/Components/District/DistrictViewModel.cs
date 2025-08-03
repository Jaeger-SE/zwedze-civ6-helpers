using Zwedze.CivilizationHelper.DistrictManagement;
using Zwedze.CivilizationHelper.Host.BlazorWasm.States;
using Zwedze.CivilizationHelper.Host.BlazorWasm.States.Selectors;
using Zwedze.Framework.Blazor.Reactive.SubscriptionManager;

namespace Zwedze.CivilizationHelper.Host.BlazorWasm.Components.District;

public interface IDistrictViewModelFactory
{
    DistrictViewModel Create(ISubscriptionManager subscriptionManager, DistrictKey key);
}

internal sealed class DistrictViewModelFactory(IGameSelectors gameSelectors, IGameService gameService) : IDistrictViewModelFactory
{
    public DistrictViewModel Create(ISubscriptionManager subscriptionManager, DistrictKey key) =>
        new(key, gameSelectors, gameService, subscriptionManager);
}

public sealed class DistrictViewModel
{
    private readonly IGameService _gameService;
    private DistrictState _district = DistrictState.Init;

    public DistrictViewModel(DistrictKey key, IGameSelectors gameSelectors, IGameService gameService, ISubscriptionManager subscriptionManager)
    {
        Key = key;
        _gameService = gameService;

        subscriptionManager.AddSubscriptionForUi(gameSelectors.DistrictSelector.Get(Key), (district, _) => _district = district);
        subscriptionManager.AddSubscriptionForUi(gameSelectors.OpenedDistrictDetailSelector.Get(), (selectedDistrictKey, _) => IsDetailViewOpened = selectedDistrictKey == Key);
    }

    public DistrictKey Key { get; }
    public bool IsUnlocked => _district.IsUnlocked;
    public bool IsDetailViewOpened { get; private set; }
    public bool CanUnplace => _district.CanUnplace;
    public bool CanRemove => _district.CanRemove;

    public void Place()
    {
        _gameService.Place(Key);
    }

    public void Unplace()
    {
        _gameService.Unplace(Key);
    }

    public void Complete()
    {
        try
        {
            _gameService.Complete(Key);
        }
        catch (InvalidOperationException)
        {
            // Ignore
        }
    }

    public void ToggleUnlock()
    {
        _gameService.ToggleUnlock(Key);
    }

    public void OpenDetail()
    {
        if (IsUnlocked)
        {
            _gameService.SelectDistrictDetail(Key);
        }
    }

    public void Destroy()
    {
        _gameService.Remove(Key);
    }
}
