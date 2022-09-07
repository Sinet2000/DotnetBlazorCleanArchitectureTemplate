using Microsoft.AspNetCore.Identity;
using PaperStop.Domain.Contracts;

namespace PaperStop.Infrastructure.Models.Identity;

public class ApplicationUserRole : IdentityRole, IAuditableEntity<string>
{
    public string Description { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public virtual ICollection<ApplicationUserRoleClaim> RoleClaims { get; set; }

    public ApplicationUserRole() : base()
    {
        RoleClaims = new HashSet<ApplicationUserRoleClaim>();
    }

    public ApplicationUserRole(string roleName, string roleDescription = null) : base(roleName)
    {
        RoleClaims = new HashSet<ApplicationUserRoleClaim>();
        Description = roleDescription;
    }
}
