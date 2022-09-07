using PaperStop.Application.Specifications.Base;
using PaperStop.Infrastructure.Models.Audit;

namespace PaperStop.Infrastructure.Specifications;

public class AuditFilterSpecification : ApplicationSpecification<Audit>
{
    public AuditFilterSpecification(string userId, string searchString, bool searchInOldValues, bool searchInNewValues)
    {
        if (!string.IsNullOrEmpty(searchString))
        {
            Criteria = p => (p.TableName.Contains(searchString) || searchInOldValues && p.OldValues.Contains(searchString) || searchInNewValues && p.NewValues.Contains(searchString)) && p.UserId == userId;
        }
        else
        {
            Criteria = p => p.UserId == userId;
        }
    }
}
