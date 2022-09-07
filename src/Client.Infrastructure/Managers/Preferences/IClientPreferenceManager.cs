using PaperStop.Shared.Managers;

namespace PaperStop.Client.Infrastructure.Managers.Preferences;

public interface IClientPreferenceManager : IPreferenceManager
{
    Task<bool> ToggleDarkModeAsync();
}