using Toolbelt.Blazor;

namespace PaperStop.Client.Infrastructure.Managers.Interceptors;

public interface IHttpInterceptorManager : IManager
{
    void RegisterEvent();

    Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e);

    void DisposeEvent();
}