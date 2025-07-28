using Zwedze.CivilizationHelper.Wasm.Components.District;
using Zwedze.CivilizationHelper.Wasm.Components.DistrictList;
using Zwedze.CivilizationHelper.Wasm.States;
using Zwedze.CivilizationHelper.Wasm.States.Container;
using Zwedze.CivilizationHelper.Wasm.States.Selectors;

namespace Zwedze.CivilizationHelper.Wasm;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppUi(this IServiceCollection services)
    {
        return services
            .AddScoped<IDistrictViewModelFactory, DistrictViewModelFactory>()
            .AddScoped<IDistrictListViewModelFactory, DistrictListViewModelFactory>()
            .AddScoped<IDistrictCrestViewModelFactory, DistrictCrestViewModelFactory>()
            .AddScoped<IGameSelectors, GameSelectors>()
            .AddScoped<IGameService, GameService>()
            .AddSingleton(_ => new StateContainer<AppState>(AppState.Init));
    }
}
