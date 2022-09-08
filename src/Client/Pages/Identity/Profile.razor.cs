using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using PaperStop.Application.Enums;
using PaperStop.Application.Requests.Identity;
using PaperStop.Client.Extensions;
using PaperStop.Shared.Constants.Storage;

namespace PaperStop.Client.Pages.Identity
{
    public partial class Profile
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private char _firstLetterOfName;
        private readonly UpdateProfileRequest _profileModel = new();

        public string UserId { get; set; }

        private async Task UpdateProfileAsync()
        {
            var response = await AccountManager.UpdateProfileAsync(_profileModel);
            if (response.Succeeded)
            {
                await AuthenticationManager.Logout();
                SnackBar.Add(Localizer["Your Profile has been updated. Please Login to Continue."], Severity.Success);
                NavigationManager.NavigateTo("/");
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    SnackBar.Add(message, Severity.Error);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var state = await StateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            _profileModel.Email = user.GetEmail();
            _profileModel.FirstName = user.GetFirstName();
            _profileModel.LastName = user.GetLastName();
            _profileModel.PhoneNumber = user.GetPhoneNumber();
            UserId = user.GetUserId();
            var data = await AccountManager.GetProfilePictureAsync(UserId);
            if (data.Succeeded)
            {
                ImageDataUrl = data.Data;
            }
            if (_profileModel.FirstName.Length > 0)
            {
                _firstLetterOfName = _profileModel.FirstName[0];
            }
        }

        private IBrowserFile _file;

        [Parameter]
        public string ImageDataUrl { get; set; }

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                var extension = Path.GetExtension(_file.Name);
                var fileName = $"{UserId}-{Guid.NewGuid()}{extension}";
                var format = "image/png";
                var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
                var buffer = new byte[imageFile.Size];
                await imageFile.OpenReadStream().ReadAsync(buffer);
                var request = new UpdateProfilePictureRequest { Data = buffer, FileName = fileName, Extension = extension, UploadType = UploadType.ProfilePicture };
                var result = await AccountManager.UpdateProfilePictureAsync(request, UserId);
                if (result.Succeeded)
                {
                    await LocalStorage.SetItemAsync(StorageConstants.Local.UserImageUrl, result.Data);
                    SnackBar.Add(Localizer["Profile picture added."], Severity.Success);
                    NavigationManager.NavigateTo("/account", true);
                }
                else
                {
                    foreach (var error in result.Messages)
                    {
                        SnackBar.Add(error, Severity.Error);
                    }
                }
            }
        }

        private async Task DeleteAsync()
        {
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), $"{string.Format(Localizer["Do you want to delete the profile picture of {0}"], _profileModel.Email)}?"}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = DialogService.Show<Shared.Dialogs.DeleteConfirmation>(Localizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var request = new UpdateProfilePictureRequest { Data = null, FileName = string.Empty, UploadType = UploadType.ProfilePicture };
                var data = await AccountManager.UpdateProfilePictureAsync(request, UserId);
                if (data.Succeeded)
                {
                    await LocalStorage.RemoveItemAsync(StorageConstants.Local.UserImageUrl);
                    ImageDataUrl = string.Empty;
                    SnackBar.Add(Localizer["Profile picture deleted."], Severity.Success);
                    NavigationManager.NavigateTo("/account", true);
                }
                else
                {
                    foreach (var error in data.Messages)
                    {
                        SnackBar.Add(error, Severity.Error);
                    }
                }
            }
        }
    }
}