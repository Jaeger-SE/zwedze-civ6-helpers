using Zwedze.CivilizationHelper.Wasm.States.Container;

namespace Zwedze.CivilizationHelper.Wasm.States.Selectors;

public interface IGameSelectors
{
    DistrictSelector DistrictSelector { get; }
    OpenedDistrictDetailSelector OpenedDistrictDetailSelector { get; }
}

internal sealed class GameSelectors(StateContainer<AppState> appStateContainer) : IGameSelectors
{
    public DistrictSelector DistrictSelector => new(appStateContainer);
    public OpenedDistrictDetailSelector OpenedDistrictDetailSelector => new(appStateContainer);
}
