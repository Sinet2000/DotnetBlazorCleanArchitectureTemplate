﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAAgentura.Application.Requests.Identity;

public class PermissionRequest
{
    public string RoleId { get; set; }
    public IList<RoleClaimRequest> RoleClaims { get; set; }
}
