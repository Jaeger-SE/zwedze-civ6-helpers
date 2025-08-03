using Microsoft.Extensions.DependencyInjection;
using Zwedze.CivilizationHelper.Game;

namespace Zwedze.CivilizationHelper;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IGameFactory, GameFactory>();
        return services;
    }
}
