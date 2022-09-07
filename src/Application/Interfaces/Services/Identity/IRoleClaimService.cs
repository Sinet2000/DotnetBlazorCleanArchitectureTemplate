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

public interface IRoleClaimService : IService
{
    Task<Result<List<RoleClaimResponse>>> GetAllAsync();

    Task<int> GetCountAsync();

    Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

    Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

    Task<Result<string>> SaveAsync(RoleClaimRequest request);

    Task<Result<string>> DeleteAsync(int id);
}
