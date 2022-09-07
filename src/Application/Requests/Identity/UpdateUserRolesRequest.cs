using PaperStop.Application.Responses.Identity;

namespace PaperStop.Application.Requests.Identity;

public class UpdateUserRolesRequest
{
    public string UserId { get; set; }
    public IList<UserRoleModel> UserRoles { get; set; }
}
