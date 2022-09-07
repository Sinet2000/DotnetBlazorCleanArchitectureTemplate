using PaperStop.Application.Interfaces.Common;
using PaperStop.Application.Requests.Identity;
using PaperStop.Application.Responses.Identity;
using PaperStop.Shared.Wrapper;

namespace PaperStop.Application.Interfaces.Services.Identity;

public interface ITokenService : IService
{
    Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

    Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
}
