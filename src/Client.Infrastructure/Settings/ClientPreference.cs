using PaperStop.Shared.Constants.Localization;
using PaperStop.Shared.Settings;

namespace PaperStop.Client.Infrastructure.Settings;

public record ClientPreference : IPreference
{
    public bool IsDarkMode { get; set; }
    public bool IsRtl { get; set; }
    public bool IsDrawerOpen { get; set; }
    public string PrimaryColor { get; set; }
    public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";
}