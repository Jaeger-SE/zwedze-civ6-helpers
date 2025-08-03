using Microsoft.AspNetCore.Components;

namespace Zwedze.Framework.Blazor.Reactive.SubscriptionManager;

public interface ISubscriptionManagerFactory
{
    ISubscriptionManager Create(IComponent component, Func<Func<Task>, Task> invokeAsync);
}

internal class SubscriptionManagerFactory : ISubscriptionManagerFactory
{
    public ISubscriptionManager Create(IComponent component, Func<Func<Task>, Task> invokeAsync) => new SubscriptionManager(component, invokeAsync);
}
