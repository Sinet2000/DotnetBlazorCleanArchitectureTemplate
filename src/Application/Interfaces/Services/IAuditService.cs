using PaperStop.Application.Responses.Audit;
using PaperStop.Shared.Wrapper;

namespace PaperStop.Application.Interfaces.Services;

public interface IAuditService
{
    Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync(string userId);

    Task<IResult<string>> ExportToExcelAsync(string userId, string searchString = "", bool searchInOldValues = false, bool searchInNewValues = false);
}
