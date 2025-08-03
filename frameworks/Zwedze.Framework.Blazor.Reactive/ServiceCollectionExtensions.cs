using Microsoft.Extensions.DependencyInjection;
using Zwedze.Framework.Blazor.Reactive.SubscriptionManager;

namespace Zwedze.Framework.Blazor.Reactive;

public static class ServiceCollectionExtensions
{
    public static void AddBlazorReactive(this IServiceCollection services)
    {
        services.AddScoped<ISubscriptionManagerFactory, SubscriptionManagerFactory>();
    }
}
