using PaperStop.Application.Interfaces.Common;
using PaperStop.Application.Requests.Identity;
using PaperStop.Shared.Wrapper;

namespace PaperStop.Application.Interfaces.Services.Account;

public interface IAccountService : IService
{
    Task<IResult> UpdateProfileAsync(UpdateProfileRequest model, string userId);

    Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, string userId);

    Task<IResult<string>> GetProfilePictureAsync(string userId);

    Task<IResult<string>> UpdateProfilePictureAsync(UpdateProfilePictureRequest request, string userId);
}
