﻿namespace PaperStop.Application.Responses.Identity;

public class GetAllRolesResponse
{
    public IEnumerable<RoleResponse> Roles { get; set; }
}
