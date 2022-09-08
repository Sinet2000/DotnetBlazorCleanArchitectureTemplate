using Microsoft.AspNetCore.Components;
using MudBlazor;
using PaperStop.Application.Requests.Identity;

namespace PaperStop.Client.Pages.Identity
{
    public partial class UserProfile
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string Description { get; set; }

        private bool _active;
        private char _firstLetterOfName;
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private string _email;

        private bool _loaded;

        private async Task ToggleUserStatus()
        {
            var request = new ToggleUserStatusRequest { ActivateUser = _active, UserId = Id };
            var result = await UserManager.ToggleUserStatusAsync(request);
            if (result.Succeeded)
            {
                SnackBar.Add(Localizer["Updated User Status."], Severity.Success);
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

        [Parameter] public string ImageDataUrl { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var userId = Id;
            var result = await UserManager.GetAsync(userId);
            if (result.Succeeded)
            {
                var user = result.Data;
                if (user != null)
                {
                    _firstName = user.FirstName;
                    _lastName = user.LastName;
                    _email = user.Email;
                    _phoneNumber = user.PhoneNumber;
                    _active = user.IsActive;
                    var data = await AccountManager.GetProfilePictureAsync(userId);
                    if (data.Succeeded)
                    {
                        ImageDataUrl = data.Data;
                    }
                }
                Title = $"{_firstName} {_lastName}'s {Localizer["Profile"]}";
                Description = _email;
                if (_firstName.Length > 0)
                {
                    _firstLetterOfName = _firstName[0];
                }
            }

            _loaded = true;
        }
    }
}