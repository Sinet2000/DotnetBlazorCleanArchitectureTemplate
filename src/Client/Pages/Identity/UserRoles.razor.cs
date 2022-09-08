﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using PaperStop.Application.Requests.Identity;
using PaperStop.Application.Responses.Identity;
using PaperStop.Shared.Constants.Permission;

namespace PaperStop.Client.Pages.Identity
{
    public partial class UserRoles
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string Description { get; set; }
        public List<UserRoleModel> UserRolesList { get; set; } = new();

        private UserRoleModel _userRole = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canEditUsers;
        private bool _canSearchRoles;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await AuthenticationManager.CurrentUser();
            _canEditUsers = (await AuthorizationService.AuthorizeAsync(_currentUser, Permissions.Users.Edit)).Succeeded;
            _canSearchRoles = (await AuthorizationService.AuthorizeAsync(_currentUser, Permissions.Roles.Search)).Succeeded;

            var userId = Id;
            var result = await UserManager.GetAsync(userId);
            if (result.Succeeded)
            {
                var user = result.Data;
                if (user != null)
                {
                    Title = $"{user.FirstName} {user.LastName}";
                    Description = string.Format(Localizer["Manage {0} {1}'s Roles"], user.FirstName, user.LastName);
                    var response = await UserManager.GetRolesAsync(user.Id);
                    UserRolesList = response.Data.UserRoles;
                }
            }

            _loaded = true;
        }

        private async Task SaveAsync()
        {
            var request = new UpdateUserRolesRequest()
            {
                UserId = Id,
                UserRoles = UserRolesList
            };
            var result = await UserManager.UpdateRolesAsync(request);
            if (result.Succeeded)
            {
                SnackBar.Add(result.Messages[0], Severity.Success);
                NavigationManager.NavigateTo("/identity/users");
            }
            else
            {
                foreach (var error in result.Messages)
                {
                    SnackBar.Add(error, Severity.Error);
                }
            }
        }

        private bool Search(UserRoleModel userRole)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (userRole.RoleName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (userRole.RoleDescription?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
    }
}