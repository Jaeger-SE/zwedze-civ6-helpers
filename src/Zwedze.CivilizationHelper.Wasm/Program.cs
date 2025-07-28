using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Zwedze.CivilizationHelper;
using Zwedze.CivilizationHelper.Wasm;
using Zwedze.Framework.Blazor.Reactive;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorReactive();
builder.Services
    .AddDomain()
    .AddAppUi()
    .AddMudServices();

var host = builder.Build();
await host.RunAsync();
