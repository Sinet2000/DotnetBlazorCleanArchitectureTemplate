﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FPAAgentura.Application.Responses.Audit;
using FPAAgentura.Shared.Wrapper;

namespace Client.Infrastructure.Managers.Audit;

public interface IAuditManager : IManager
{
    Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync();

    Task<IResult<string>> DownloadFileAsync(string searchString = "", bool searchInOldValues = false, bool searchInNewValues = false);
}