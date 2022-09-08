using PaperStop.Shared.Constants.Localization;
using PaperStop.Shared.Settings;

namespace PaperStop.Server.Settings;

public record ServerPreference : IPreference
{
    public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

    //TODO - add server preferences
}
