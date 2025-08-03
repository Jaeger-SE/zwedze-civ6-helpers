using Microsoft.AspNetCore.Components;
using Zwedze.Framework.Blazor.Reactive.SubscriptionManager;

namespace Zwedze.CivilizationHelper.Host.BlazorWasm.Pages;

public partial class DistrictDiscount : ComponentBase, IDisposable, IReactiveAwareComponent
{
    private ISubscriptionManager _subscriptionManager = null!;

    public void Dispose()
    {
        _subscriptionManager.Dispose();
    }

    public void TriggerStateHasChanged()
    {
        StateHasChanged();
    }

    protected override void OnInitialized()
    {
        _subscriptionManager = SubscriptionManagerFactory.Create(this, InvokeAsync);
    }
}
