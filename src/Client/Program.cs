using System.Globalization;
using Client.Extensions;
using Client.Infrastructure.Managers.Preferences;
using Client.Infrastructure.Settings;
using FPAAgentura.Shared.Constants.Localization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Client;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder
            .CreateDefault(args)
            .AddRootComponents()
            .AddClientServices();
        var host = builder.Build();
        var storageService = host.Services.GetRequiredService<ClientPreferenceManager>();
        if (storageService != null)
        {
            CultureInfo culture;
            var preference = await storageService.GetPreference() as ClientPreference;
            if (preference != null)
                culture = new CultureInfo(preference.LanguageCode);
            else
                culture = new CultureInfo(LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
        await builder.Build().RunAsync();
    }
}