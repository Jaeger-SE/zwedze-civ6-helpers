using System.Reactive.Linq;
using Zwedze.CivilizationHelper.DistrictManagement;
using Zwedze.CivilizationHelper.Wasm.States.Container;

namespace Zwedze.CivilizationHelper.Wasm.States.Selectors;

public sealed class DistrictSelector(StateContainer<AppState> stateContainer)
{
    public IObservable<DistrictState> Get(DistrictKey key)
    {
        return stateContainer.State
            .Select(appState => appState.Game.Districts)
            .Where(districts => districts.Any(d => d.Key == key))
            .Select(districts => districts.Single(d => d.Key == key));
    }
}
