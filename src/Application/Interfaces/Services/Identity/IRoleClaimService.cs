using PaperStop.Application.Interfaces.Common;
using PaperStop.Application.Requests.Identity;
using PaperStop.Application.Responses.Identity;
using PaperStop.Shared.Wrapper;

namespace PaperStop.Application.Interfaces.Services.Identity;

public interface IRoleClaimService : IService
{
    Task<Result<List<RoleClaimResponse>>> GetAllAsync();

    Task<int> GetCountAsync();

    Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

    Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

    Task<Result<string>> SaveAsync(RoleClaimRequest request);

    Task<Result<string>> DeleteAsync(int id);
}
