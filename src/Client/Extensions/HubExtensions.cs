using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using PaperStop.Shared.Constants.Application;

namespace PaperStop.Client.Extensions;

public static class HubExtensions
{
    public static HubConnection TryInitialize(this HubConnection hubConnection, NavigationManager navigationManager, string apiAddress)
    {
        if (hubConnection == null)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl($"{apiAddress.TrimEnd('/')}/{ApplicationConstants.SignalR.HubUrl.TrimStart('/')}")
                .Build();
        }
        return hubConnection;
    }
}
