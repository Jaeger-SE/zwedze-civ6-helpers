using System.Reactive.Linq;
using Zwedze.CivilizationHelper.DistrictManagement;
using Zwedze.CivilizationHelper.Host.BlazorWasm.States.Container;

namespace Zwedze.CivilizationHelper.Host.BlazorWasm.States.Selectors;

public sealed class OpenedDistrictDetailSelector(StateContainer<AppState> stateContainer)
{
    public IObservable<DistrictKey?> Get()
    {
        return stateContainer.State
            .Select(appState => appState.Game.OpenedDistrictKey) ;
    }
}
