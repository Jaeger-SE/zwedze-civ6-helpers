namespace Zwedze.Framework.Blazor.Reactive.SubscriptionManager;

public interface IReactiveAwareComponent
{
    void TriggerStateHasChanged();
}
