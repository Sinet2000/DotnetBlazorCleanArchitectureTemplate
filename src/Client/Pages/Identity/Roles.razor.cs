using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using PaperStop.Application.Requests.Identity;
using PaperStop.Application.Responses.Identity;
using PaperStop.Client.Extensions;
using PaperStop.Client.Infrastructure.Managers.Identity.Roles;
using PaperStop.Shared.Constants.Application;
using PaperStop.Shared.Constants.Permission;

namespace PaperStop.Client.Pages.Identity
{
    public partial class Roles
    {
        [Inject] private IRoleManager RoleManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private List<RoleResponse> _roleList = new();
        private RoleResponse _role = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateRoles;
        private bool _canEditRoles;
        private bool _canDeleteRoles;
        private bool _canSearchRoles;
        private bool _canViewRoleClaims;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await AuthenticationManager.CurrentUser();
            _canCreateRoles = (await AuthorizationService.AuthorizeAsync(_currentUser, Permissions.Roles.Create)).Succeeded;
            _canEditRoles = (await AuthorizationService.AuthorizeAsync(_currentUser, Permissions.Roles.Edit)).Succeeded;
            _canDeleteRoles = (await AuthorizationService.AuthorizeAsync(_currentUser, Permissions.Roles.Delete)).Succeeded;
            _canSearchRoles = (await AuthorizationService.AuthorizeAsync(_currentUser, Permissions.Roles.Search)).Succeeded;
            _canViewRoleClaims = (await AuthorizationService.AuthorizeAsync(_currentUser, Permissions.RoleClaims.View)).Succeeded;

            await GetRolesAsync();
            _loaded = true;

            var apiAddress = Configuration[$"{ClientAppConfiguration.ConfigKey}:{nameof(ClientAppConfiguration.ApiAddress)}"];
            HubConnection = HubConnection.TryInitialize(NavigationManager, apiAddress);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetRolesAsync()
        {
            var response = await RoleManager.GetRolesAsync();
            if (response.Succeeded)
            {
                _roleList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    SnackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task Delete(string id)
        {
            string deleteContent = Localizer["Delete Content"];
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = DialogService.Show<Shared.Dialogs.DeleteConfirmation>(Localizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await RoleManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    await Reset();
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    SnackBar.Add(response.Messages[0], Severity.Success);
                }
                else
                {
                    await Reset();
                    foreach (var message in response.Messages)
                    {
                        SnackBar.Add(message, Severity.Error);
                    }
                }
            }
        }

        private async Task InvokeModal(string id = null)
        {
            var parameters = new DialogParameters();
            if (id != null)
            {
                _role = _roleList.FirstOrDefault(c => c.Id == id);
                if (_role != null)
                {
                    parameters.Add(nameof(RoleModal.RoleModel), new RoleRequest
                    {
                        Id = _role.Id,
                        Name = _role.Name,
                        Description = _role.Description
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = DialogService.Show<RoleModal>(id == null ? Localizer["Create"] : Localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            _role = new RoleResponse();
            await GetRolesAsync();
        }

        private bool Search(RoleResponse role)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (role.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (role.Description?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }

        private void ManagePermissions(string roleId)
        {
            NavigationManager.NavigateTo($"/identity/role-permissions/{roleId}");
        }
    }
}