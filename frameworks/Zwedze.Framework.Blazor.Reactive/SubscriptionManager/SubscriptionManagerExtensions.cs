namespace Zwedze.Framework.Blazor.Reactive.SubscriptionManager;

public static class SubscriptionManagerExtensions
{
    public static ISubscriptionManagerBuilder<T> For<T>(this ISubscriptionManager subscriptionManager, IObservable<T> observable)
    {
        return new SubscriptionManagerBuilder<T>(observable, subscriptionManager);
    }
}
