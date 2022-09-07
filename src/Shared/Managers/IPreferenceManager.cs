using PaperStop.Shared.Settings;
using PaperStop.Shared.Wrapper;

namespace PaperStop.Shared.Managers;

public interface IPreferenceManager
{
    Task SetPreference(IPreference preference);

    Task<IPreference> GetPreference();

    Task<IResult> ChangeLanguageAsync(string languageCode);
}
