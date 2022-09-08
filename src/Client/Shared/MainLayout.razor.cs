using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;
using PaperStop.Client.Extensions;
using PaperStop.Client.Infrastructure.Managers.Identity.Roles;
using PaperStop.Client.Infrastructure.Settings;
using PaperStop.Shared.Constants.Application;

namespace PaperStop.Client.Shared;

public partial class MainLayout : IDisposable
{
    [Inject] private IRoleManager RoleManager { get; set; }

    private string CurrentUserId { get; set; }
    private string ImageDataUrl { get; set; }
    private string FirstName { get; set; }
    private string SecondName { get; set; }
    private string Email { get; set; }
    private char FirstLetterOfName { get; set; }

    private async Task LoadDataAsync()
    {
        var state = await StateProvider.GetAuthenticationStateAsync();
        var user = state.User;
        if (user == null) return;
        if (user.Identity?.IsAuthenticated == true)
        {
            CurrentUserId = user.GetUserId();
            FirstName = user.GetFirstName();
            if (FirstName.Length > 0)
            {
                FirstLetterOfName = FirstName[0];
            }
            SecondName = user.GetLastName();
            Email = user.GetEmail();
            var imageResponse = await AccountManager.GetProfilePictureAsync(CurrentUserId);
            if (imageResponse.Succeeded)
            {
                ImageDataUrl = imageResponse.Data;
            }

            var currentUserResult = await UserManager.GetAsync(CurrentUserId);
            if (!currentUserResult.Succeeded || currentUserResult.Data == null)
            {
                SnackBar.Add(Localizer["You are logged out because the user with your Token has been deleted."], Severity.Error);
                await AuthenticationManager.Logout();
            }

            await _hubConnection.SendAsync(ApplicationConstants.SignalR.OnConnect, CurrentUserId);
        }
    }

    private MudTheme _currentTheme;
    private bool _drawerOpen = true;
    private bool _rightToLeft = false;
    private async Task RightToLeftToggle()
    {
        var isRtl = await ClientPreferenceManager.ToggleLayoutDirection();
        _rightToLeft = isRtl;
        _drawerOpen = false;
    }

    protected override async Task OnInitializedAsync()
    {
        _currentTheme = ClientAppTheme.DefaultTheme;
        _currentTheme = await ClientPreferenceManager.GetCurrentThemeAsync();
        _rightToLeft = await ClientPreferenceManager.IsRtl();
        Interceptor.RegisterEvent();

        var apiAddress = Configuration[$"{ClientAppConfiguration.ConfigKey}:{nameof(ClientAppConfiguration.ApiAddress)}"];

        _hubConnection = _hubConnection.TryInitialize(NavigationManager, apiAddress);

        await _hubConnection.StartAsync();
        _hubConnection.On<string, string, string>(ApplicationConstants.SignalR.ReceiveChatNotification, (message, receiverUserId, senderUserId) =>
        {
            if (CurrentUserId == receiverUserId)
            {
                JsRuntime.InvokeAsync<string>("PlayAudio", "notification");
                SnackBar.Add(message, Severity.Info, config =>
                {
                    config.VisibleStateDuration = 10000;
                    config.HideTransitionDuration = 500;
                    config.ShowTransitionDuration = 500;
                    config.Action = Localizer["Chat?"];
                    config.ActionColor = Color.Primary;
                    config.Onclick = snackbar =>
                    {
                        NavigationManager.NavigateTo($"chat/{senderUserId}");
                        return Task.CompletedTask;
                    };
                });
            }
        });
        _hubConnection.On(ApplicationConstants.SignalR.ReceiveRegenerateTokens, async () =>
        {
            try
            {
                var token = await AuthenticationManager.TryForceRefreshToken();
                if (!string.IsNullOrEmpty(token))
                {
                    SnackBar.Add(Localizer["Refreshed Token."], Severity.Success);
                    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                SnackBar.Add(Localizer["You are Logged Out."], Severity.Error);
                await AuthenticationManager.Logout();
                NavigationManager.NavigateTo("/");
            }
        });
        _hubConnection.On<string, string>(ApplicationConstants.SignalR.LogoutUsersByRole, async (userId, roleId) =>
        {
            if (CurrentUserId != userId)
            {
                var rolesResponse = await RoleManager.GetRolesAsync();
                if (rolesResponse.Succeeded)
                {
                    var role = rolesResponse.Data.FirstOrDefault(x => x.Id == roleId);
                    if (role != null)
                    {
                        var currentUserRolesResponse = await UserManager.GetRolesAsync(CurrentUserId);
                        if (currentUserRolesResponse.Succeeded && currentUserRolesResponse.Data.UserRoles.Any(x => x.RoleName == role.Name))
                        {
                            SnackBar.Add(Localizer["You are logged out because the Permissions of one of your Roles have been updated."], Severity.Error);
                            await _hubConnection.SendAsync(ApplicationConstants.SignalR.OnDisconnect, CurrentUserId);
                            await AuthenticationManager.Logout();
                            NavigationManager.NavigateTo("/login");
                        }
                    }
                }
            }
        });
    }

    private void Logout()
    {
        var parameters = new DialogParameters
            {
                {nameof(Dialogs.Logout.ContentText), $"{Localizer["Logout Confirmation"]}"},
                {nameof(Dialogs.Logout.ButtonText), $"{Localizer["Logout"]}"},
                {nameof(Dialogs.Logout.Color), Color.Error},
                {nameof(Dialogs.Logout.CurrentUserId), CurrentUserId},
                {nameof(Dialogs.Logout.HubConnection), _hubConnection}
            };

        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

        DialogService.Show<Dialogs.Logout>(Localizer["Logout"], parameters, options);
    }

    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    private async Task DarkMode()
    {
        bool isDarkMode = await ClientPreferenceManager.ToggleDarkModeAsync();
        _currentTheme = isDarkMode
            ? ClientAppTheme.DefaultTheme
            : ClientAppTheme.DarkTheme;
    }

    public void Dispose()
    {
        Interceptor.DisposeEvent();
        //_ = hubConnection.DisposeAsync();
    }

    private HubConnection _hubConnection;
    public bool IsConnected => _hubConnection.State == HubConnectionState.Connected;
}
