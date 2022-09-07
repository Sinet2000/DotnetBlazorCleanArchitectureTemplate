using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAAgentura.Application.Responses.Identity;

public class GetAllRolesResponse
{
    public IEnumerable<RoleResponse> Roles { get; set; }
}
