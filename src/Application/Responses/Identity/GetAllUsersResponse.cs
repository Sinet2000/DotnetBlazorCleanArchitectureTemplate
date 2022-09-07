namespace PaperStop.Application.Responses.Identity;

public class GetAllUsersResponse
{
    public IEnumerable<UserResponse> Users { get; set; }
}
