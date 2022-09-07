using System.Security.Claims;
using PaperStop.Application.Requests.Identity;
using PaperStop.Shared.Wrapper;

namespace PaperStop.Client.Infrastructure.Managers.Identity.Authentication;

public interface IAuthenticationManager : IManager
{
    Task<IResult> Login(TokenRequest model);

    Task<IResult> Logout();

    Task<string> RefreshToken();

    Task<string> TryRefreshToken();

    Task<string> TryForceRefreshToken();

    Task<ClaimsPrincipal> CurrentUser();
}