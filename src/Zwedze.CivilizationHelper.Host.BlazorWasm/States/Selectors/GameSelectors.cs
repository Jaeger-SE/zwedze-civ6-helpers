using Zwedze.CivilizationHelper.Host.BlazorWasm.States.Container;

namespace Zwedze.CivilizationHelper.Host.BlazorWasm.States.Selectors;

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
