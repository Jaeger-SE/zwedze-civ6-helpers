using System.Reactive;
using System.Reactive.Linq;
using Microsoft.AspNetCore.Components;

namespace Zwedze.Framework.Blazor.Reactive.SubscriptionManager;

public interface ISubscriptionManager : IDisposable
{
    void AddSubscriptionAsyncForUi<TSource>(
        IObservable<TSource> observable,
        Func<TSource, CancellationToken, Task> handler,
        Action? onCompleted = null,
        Action<Exception>? onError = null,
        CancellationToken cancellationToken = default);

    void AddSubscriptionForUi<TSource>(
        IObservable<TSource> observable,
        Action<TSource, CancellationToken> handler,
        Action? onCompleted = null,
        Action<Exception>? onError = null,
        CancellationToken cancellationToken = default);
}

internal sealed class SubscriptionManager(IComponent blazorComponent, Func<Func<Task>, Task> invokeAsync) : ISubscriptionManager
{
    private readonly IList<IDisposable> _subscriptions = new List<IDisposable>();

    public void Dispose()
    {
        foreach (var subscription in _subscriptions)
        {
            subscription.Dispose();
        }
    }

    public void AddSubscriptionAsyncForUi<TSource>(
        IObservable<TSource> observable,
        Func<TSource, CancellationToken, Task> handler,
        Action? onCompleted = null,
        Action<Exception>? onError = null,
        CancellationToken cancellationToken = default)
    {
        AddSubscriptionForUiInternal(observable, handler, onCompleted, onError, cancellationToken);
    }

    public void AddSubscriptionForUi<TSource>(
        IObservable<TSource> observable,
        Action<TSource, CancellationToken> handler,
        Action? onCompleted = null,
        Action<Exception>? onError = null,
        CancellationToken cancellationToken = default)
    {
        Func<TSource, CancellationToken, Task> handle = (source, token) =>
        {
            handler(source, token);
            return Task.CompletedTask;
        };
        AddSubscriptionForUiInternal(observable, handle, onCompleted, onError, cancellationToken);
    }

    private void AddSubscriptionForUiInternal<TSource>(
        IObservable<TSource> observable,
        Func<TSource, CancellationToken, Task> handler,
        Action? onCompleted,
        Action<Exception>? onError,
        CancellationToken cancellationToken)
    {
        var subscription = observable
            .SelectMany(source =>
                Observable.FromAsync(async () =>
                {
                    await handler(source, cancellationToken);

                    // This ensures we switch back to the Blazor dispatcher thread
                    // ReSharper disable once SuspiciousTypeConversion.Global
                    if (blazorComponent is IReactiveAwareComponent component)
                    {
                        await invokeAsync(() =>
                        {
                            component.TriggerStateHasChanged();
                            return Task.CompletedTask;
                        });
                    }

                    return Unit.Default;
                }))
            .Subscribe(
                _ => { },
                onError ?? (_ => { }),
                onCompleted ?? (() => { })
            );

        _subscriptions.Add(subscription);
    }
}
