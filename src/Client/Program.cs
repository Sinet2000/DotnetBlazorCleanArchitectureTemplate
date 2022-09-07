using System.Globalization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PaperStop.Client.Extensions;
using PaperStop.Client.Infrastructure.Managers.Preferences;
using PaperStop.Client.Infrastructure.Settings;
using PaperStop.Shared.Constants.Localization;

namespace PaperStop.Client;

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