using Zwedze.CivilizationHelper.Host.BlazorWasm.Components.District;
using Zwedze.CivilizationHelper.Host.BlazorWasm.Components.DistrictList;
using Zwedze.CivilizationHelper.Host.BlazorWasm.States;
using Zwedze.CivilizationHelper.Host.BlazorWasm.States.Container;
using Zwedze.CivilizationHelper.Host.BlazorWasm.States.Selectors;

namespace Zwedze.CivilizationHelper.Host.BlazorWasm;

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
