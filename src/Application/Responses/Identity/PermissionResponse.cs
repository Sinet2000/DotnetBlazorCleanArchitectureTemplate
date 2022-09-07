using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAAgentura.Application.Responses.Identity;

public class PermissionResponse
{
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    public List<RoleClaimResponse> RoleClaims { get; set; }
}
