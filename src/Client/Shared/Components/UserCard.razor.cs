using Microsoft.AspNetCore.Components;
using PaperStop.Client.Extensions;

namespace PaperStop.Client.Shared.Components
{
    public partial class UserCard
    {
        [Parameter] public string Class { get; set; }
        private string FirstName { get; set; }
        private string SecondName { get; set; }
        private string Email { get; set; }
        private char FirstLetterOfName { get; set; }

        [Parameter]
        public string ImageDataUrl { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var state = await StateProvider.GetAuthenticationStateAsync();
            var user = state.User;

            this.Email = ClaimsPrincipalExtensions.GetEmail(user).Replace(".com", string.Empty);
            this.FirstName = ClaimsPrincipalExtensions.GetFirstName(user);
            this.SecondName = ClaimsPrincipalExtensions.GetLastName(user);
            if (this.FirstName.Length > 0)
            {
                FirstLetterOfName = FirstName[0];
            }
            var userId = ClaimsPrincipalExtensions.GetUserId(user);
            var imageResponse = await AccountManager.GetProfilePictureAsync(userId);
            if (imageResponse.Succeeded)
            {
                ImageDataUrl = imageResponse.Data;
            }
        }
    }
}