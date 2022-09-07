using PaperStop.Application.Specifications.Base;
using PaperStop.Infrastructure.Models.Identity;

namespace PaperStop.Infrastructure.Specifications;

public class UserFilterSpecification : ApplicationSpecification<ApplicationUser>
{
    public UserFilterSpecification(string searchString)
    {
        if (!string.IsNullOrEmpty(searchString))
        {
            Criteria = p => p.FirstName.Contains(searchString) || p.LastName.Contains(searchString) || p.Email.Contains(searchString) || p.PhoneNumber.Contains(searchString) || p.UserName.Contains(searchString);
        }
        else
        {
            Criteria = p => true;
        }
    }
}
