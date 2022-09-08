using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using PaperStop.Application.Requests.Identity;
using PaperStop.Client.Extensions;
using PaperStop.Client.Infrastructure.Managers.Identity.Roles;
using PaperStop.Shared.Constants.Application;

namespace PaperStop.Client.Pages.Identity
{
    public partial class RoleModal
    {
        [Inject] private IRoleManager RoleManager { get; set; }

        [Parameter] public RoleRequest RoleModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        protected override async Task OnInitializedAsync()
        {
            var apiAddress = Configuration[$"{ClientAppConfiguration.ConfigKey}:{nameof(ClientAppConfiguration.ApiAddress)}"];
            HubConnection = HubConnection.TryInitialize(NavigationManager, apiAddress);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task SaveAsync()
        {
            var response = await RoleManager.SaveAsync(RoleModel);
            if (response.Succeeded)
            {
                SnackBar.Add(response.Messages[0], Severity.Success);
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    SnackBar.Add(message, Severity.Error);
                }
            }
        }
    }
}