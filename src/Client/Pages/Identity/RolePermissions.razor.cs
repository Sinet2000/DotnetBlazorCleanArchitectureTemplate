using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using PaperStop.Application.Requests.Identity;
using PaperStop.Application.Responses.Identity;
using PaperStop.Client.Extensions;
using PaperStop.Client.Infrastructure.Managers.Identity.Roles;
using PaperStop.Client.Infrastructure.Mappings;
using PaperStop.Shared.Constants.Application;
using PaperStop.Shared.Constants.Permission;

namespace PaperStop.Client.Pages.Identity
{
    public partial class RolePermissions
    {
        [Inject] private IRoleManager RoleManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [Parameter] public string Id { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string Description { get; set; }

        private PermissionResponse _model;
        private Dictionary<string, List<RoleClaimResponse>> GroupedRoleClaims { get; } = new();
        private IMapper _mapper;
        private RoleClaimResponse _roleClaims = new();
        private RoleClaimResponse _selectedItem = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canEditRolePermissions;
        private bool _canSearchRolePermissions;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await AuthenticationManager.CurrentUser();
            _canEditRolePermissions = (await AuthorizationService.AuthorizeAsync(_currentUser, Permissions.RoleClaims.Edit)).Succeeded;
            _canSearchRolePermissions = (await AuthorizationService.AuthorizeAsync(_currentUser, Permissions.RoleClaims.Search)).Succeeded;

            await GetRolePermissionsAsync();
            _loaded = true;

            var apiAddress = Configuration[$"{ClientAppConfiguration.ConfigKey}:{nameof(ClientAppConfiguration.ApiAddress)}"];

            HubConnection = HubConnection.TryInitialize(NavigationManager, apiAddress);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetRolePermissionsAsync()
        {
            _mapper = new MapperConfiguration(c => { c.AddProfile<RoleProfile>(); }).CreateMapper();
            var roleId = Id;
            var result = await RoleManager.GetPermissionsAsync(roleId);
            if (result.Succeeded)
            {
                _model = result.Data;
                GroupedRoleClaims.Add(Localizer["All Permissions"], _model.RoleClaims);
                foreach (var claim in _model.RoleClaims)
                {
                    if (GroupedRoleClaims.ContainsKey(claim.Group))
                    {
                        GroupedRoleClaims[claim.Group].Add(claim);
                    }
                    else
                    {
                        GroupedRoleClaims.Add(claim.Group, new List<RoleClaimResponse> { claim });
                    }
                }
                if (_model != null)
                {
                    Description = string.Format(Localizer["Manage {0} {1}'s Permissions"], _model.RoleId, _model.RoleName);
                }
            }
            else
            {
                foreach (var error in result.Messages)
                {
                    SnackBar.Add(error, Severity.Error);
                }
                NavigationManager.NavigateTo("/identity/roles");
            }
        }

        private async Task SaveAsync()
        {
            var request = _mapper.Map<PermissionResponse, PermissionRequest>(_model);
            var result = await RoleManager.UpdatePermissionsAsync(request);
            if (result.Succeeded)
            {
                SnackBar.Add(result.Messages[0], Severity.Success);
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendRegenerateTokens);
                await HubConnection.SendAsync(ApplicationConstants.SignalR.OnChangeRolePermissions, _currentUser.GetUserId(), request.RoleId);
                NavigationManager.NavigateTo("/identity/roles");
            }
            else
            {
                foreach (var error in result.Messages)
                {
                    SnackBar.Add(error, Severity.Error);
                }
            }
        }

        private bool Search(RoleClaimResponse roleClaims)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (roleClaims.Value?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (roleClaims.Description?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }

        private Color GetGroupBadgeColor(int selected, int all)
        {
            if (selected == 0)
                return Color.Error;

            if (selected == all)
                return Color.Success;

            return Color.Info;
        }
    }
}