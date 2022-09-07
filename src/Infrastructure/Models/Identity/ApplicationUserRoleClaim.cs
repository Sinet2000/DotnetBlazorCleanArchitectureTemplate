using FPAAgentura.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Identity;

public class ApplicationUserRoleClaim : IdentityRoleClaim<string>, IAuditableEntity<int>
{
    public string Description { get; set; }
    public string Group { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public virtual ApplicationUserRole Role { get; set; }

    public ApplicationUserRoleClaim() : base()
    {
    }

    public ApplicationUserRoleClaim(string roleClaimDescription = null, string roleClaimGroup = null) : base()
    {
        Description = roleClaimDescription;
        Group = roleClaimGroup;
    }
}
