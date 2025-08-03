using Microsoft.AspNetCore.Components;
using Zwedze.Framework.Blazor.Reactive.SubscriptionManager;

namespace Zwedze.CivilizationHelper.Host.BlazorWasm.Components.DistrictList;

public partial class DistrictList : ComponentBase
{
    private DistrictListViewModel _vm = null!;
    [CascadingParameter] public required ISubscriptionManager SubscriptionManager { get; set; }

    protected override void OnInitialized()
    {
        _vm = DistrictListViewModelFactory.Create(SubscriptionManager);
    }
}
