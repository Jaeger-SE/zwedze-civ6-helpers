using System.Reactive.Linq;
using Zwedze.CivilizationHelper.DistrictManagement;
using Zwedze.CivilizationHelper.Wasm.States.Container;

namespace Zwedze.CivilizationHelper.Wasm.States.Selectors;

public sealed class OpenedDistrictDetailSelector(StateContainer<AppState> stateContainer)
{
    public IObservable<DistrictKey?> Get()
    {
        return stateContainer.State
            .Select(appState => appState.Game.OpenedDistrictKey) ;
    }
}
