namespace Zwedze.Framework.Blazor.Reactive.SubscriptionManager;

public interface ISubscriptionManagerBuilder<out TObservableValue>
{
    ISubscriptionManagerBuilder<TObservableValue> Do(Action<TObservableValue, CancellationToken> handler);
    ISubscriptionManagerBuilder<TObservableValue> Do(Func<TObservableValue, CancellationToken, Task> handler);
}

internal sealed class SubscriptionManagerBuilder<TObservableValue>(IObservable<TObservableValue> observable, ISubscriptionManager subscriptionManager) : ISubscriptionManagerBuilder<TObservableValue>
{
    public ISubscriptionManagerBuilder<TObservableValue> Do(Action<TObservableValue, CancellationToken> handler)
    {
        subscriptionManager.AddSubscriptionForUi(observable, handler);
        return this;
    }

    public ISubscriptionManagerBuilder<TObservableValue> Do(Func<TObservableValue, CancellationToken, Task> handler)
    {
        subscriptionManager.AddSubscriptionAsyncForUi(observable, handler);
        return this;
    }
}
