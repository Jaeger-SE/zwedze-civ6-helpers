using Microsoft.AspNetCore.Components;
using Zwedze.CivilizationHelper.DistrictManagement;
using Zwedze.Framework.Blazor.Reactive.SubscriptionManager;

namespace Zwedze.CivilizationHelper.Host.BlazorWasm.Components.District;

public partial class District : ComponentBase
{
    private DistrictViewModel _vm = null!;

    [Parameter] [EditorRequired] public required DistrictKey DistrictKey { get; init; }
    [CascadingParameter] public required ISubscriptionManager SubscriptionManager { get; set; }

    protected override void OnInitialized()
    {
        _vm = DistrictViewModelFactory.Create(SubscriptionManager, DistrictKey);
    }
}
