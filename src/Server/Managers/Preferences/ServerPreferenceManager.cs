using Microsoft.Extensions.Localization;
using PaperStop.Application.Interfaces.Services.Storage;
using PaperStop.Shared.Constants.Storage;
using PaperStop.Shared.Settings;
using PaperStop.Shared.Wrapper;
using Server.Settings;
using IResult = PaperStop.Shared.Wrapper.IResult;

namespace Server.Managers.Preferences;

public class ServerPreferenceManager : IServerPreferenceManager
{
    private readonly IServerStorageService _serverStorageService;
    private readonly IStringLocalizer<ServerPreferenceManager> _localizer;

    public ServerPreferenceManager(
        IServerStorageService serverStorageService,
        IStringLocalizer<ServerPreferenceManager> localizer)
    {
        _serverStorageService = serverStorageService;
        _localizer = localizer;
    }

    public async Task<IResult> ChangeLanguageAsync(string languageCode)
    {
        if (await GetPreference() is ServerPreference preference)
        {
            preference.LanguageCode = languageCode;
            await SetPreference(preference);
            return new Result
            {
                Succeeded = true,
                Messages = new List<string> { _localizer["Server Language has been changed"] }
            };
        }

        return new Result
        {
            Succeeded = false,
            Messages = new List<string> { _localizer["Failed to get server preferences"] }
        };
    }

    public async Task<IPreference> GetPreference()
    {
        return await _serverStorageService.GetItemAsync<ServerPreference>(StorageConstants.Server.Preference) ?? new ServerPreference();
    }

    public async Task SetPreference(IPreference preference)
    {
        await _serverStorageService.SetItemAsync(StorageConstants.Server.Preference, preference as ServerPreference);
    }
}
