using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPAAgentura.Application.Specifications.Base;
using Infrastructure.Models.Audit;

namespace Infrastructure.Specifications;

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
