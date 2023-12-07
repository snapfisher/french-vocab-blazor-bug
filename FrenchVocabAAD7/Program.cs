using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using FrenchVocabAAD7;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
});


builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
        options.ProductToken = builder.Configuration["BlazorizeProductToken"];
    })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons()
    .AddSpeechSynthesis();

builder.Services.AddSingleton<WordList>();
builder.Services.AddSingleton<StatusTracker>();

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fr-FR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("fr-FR");

await builder.Build().RunAsync();
