using PaperStop.Application.Interfaces.Common;
using PaperStop.Application.Requests.Identity;
using PaperStop.Application.Responses.Identity;
using PaperStop.Shared.Wrapper;

namespace PaperStop.Application.Interfaces.Services.Identity;

public interface IRoleService : IService
{
    Task<Result<List<RoleResponse>>> GetAllAsync();

    Task<int> GetCountAsync();

    Task<Result<RoleResponse>> GetByIdAsync(string id);

    Task<Result<string>> SaveAsync(RoleRequest request);

    Task<Result<string>> DeleteAsync(string id);

    Task<Result<PermissionResponse>> GetAllPermissionsAsync(string roleId);

    Task<Result<string>> UpdatePermissionsAsync(PermissionRequest request);
}
