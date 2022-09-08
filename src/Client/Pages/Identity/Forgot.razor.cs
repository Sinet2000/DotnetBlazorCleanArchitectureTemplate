using Blazored.FluentValidation;
using MudBlazor;
using PaperStop.Application.Requests.Identity;

namespace PaperStop.Client.Pages.Identity
{
    public partial class Forgot
    {
        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private readonly ForgotPasswordRequest _emailModel = new();

        private async Task SubmitAsync()
        {
            var result = await UserManager.ForgotPasswordAsync(_emailModel);
            if (result.Succeeded)
            {
                SnackBar.Add(Localizer["Done!"], Severity.Success);
                NavigationManager.NavigateTo("/");
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    SnackBar.Add(message, Severity.Error);
                }
            }
        }
    }
}