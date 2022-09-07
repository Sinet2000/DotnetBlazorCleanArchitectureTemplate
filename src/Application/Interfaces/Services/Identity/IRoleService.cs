using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPAAgentura.Application.Interfaces.Common;
using FPAAgentura.Application.Requests.Identity;
using FPAAgentura.Application.Responses.Identity;
using FPAAgentura.Shared.Wrapper;

namespace FPAAgentura.Application.Interfaces.Services.Identity;

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
