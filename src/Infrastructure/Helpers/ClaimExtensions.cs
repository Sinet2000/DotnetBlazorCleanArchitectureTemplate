﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FPAAgentura.Application.Responses.Identity;
using FPAAgentura.Shared.Constants.Permission;
using Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Helpers;

public static class ClaimsHelper
{
    public static void GetAllPermissions(this List<RoleClaimResponse> allPermissions)
    {
        var modules = typeof(Permissions).GetNestedTypes();

        foreach (var module in modules)
        {
            var fields = module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            foreach (FieldInfo fi in fields)
            {
                var propertyValue = fi.GetValue(null);

                if (propertyValue is not null)
                    allPermissions.Add(new RoleClaimResponse { Value = propertyValue.ToString(), Type = ApplicationClaimTypes.Permission, Group = module.Name });
                //TODO - take descriptions from description attribute
            }
        }

    }

    public static async Task<IdentityResult> AddPermissionClaim(this RoleManager<ApplicationUserRole> roleManager, ApplicationUserRole role, string permission)
    {
        var allClaims = await roleManager.GetClaimsAsync(role);
        if (!allClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && a.Value == permission))
        {
            return await roleManager.AddClaimAsync(role, new Claim(ApplicationClaimTypes.Permission, permission));
        }

        return IdentityResult.Failed();
    }
}
