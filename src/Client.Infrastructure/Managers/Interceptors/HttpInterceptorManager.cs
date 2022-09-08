using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using PaperStop.Client.Infrastructure.Managers.Identity.Authentication;
using Toolbelt.Blazor;

namespace PaperStop.Client.Infrastructure.Managers.Interceptors;

public class HttpInterceptorManager : IHttpInterceptorManager
{
    private readonly HttpClientInterceptor _interceptor;
    private readonly IAuthenticationManager _authenticationManager;
    private readonly NavigationManager _navigationManager;
    private readonly IStringLocalizer<HttpInterceptorManager> _localizer;

    public HttpInterceptorManager(
        HttpClientInterceptor interceptor,
        IAuthenticationManager authenticationManager,
        NavigationManager navigationManager,
        IStringLocalizer<HttpInterceptorManager> localizer)
    {
        _interceptor = interceptor;
        _authenticationManager = authenticationManager;
        _navigationManager = navigationManager;
        _localizer = localizer;
    }

    public void RegisterEvent() => _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;

    public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
    {
        var absPath = e.Request.RequestUri.AbsolutePath;
        if (!absPath.Contains("token") && !absPath.Contains("accounts"))
        {
            try
            {
                var token = await _authenticationManager.TryRefreshToken();
                if (!string.IsNullOrEmpty(token))
                {
                    // _snackBar.Add(_localizer["Refreshed Token."], Severity.Success);
                    e.Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await _authenticationManager.Logout();
                _navigationManager.NavigateTo("/");
            }
        }
    }

    public void DisposeEvent() => _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
}