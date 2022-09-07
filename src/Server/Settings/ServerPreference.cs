using FPAAgentura.Shared.Constants.Localization;
using FPAAgentura.Shared.Settings;

namespace Server.Settings;

public record ServerPreference : IPreference
{
    public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

    //TODO - add server preferences
}
