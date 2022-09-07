using PaperStop.Application.Interfaces.Services;

namespace PaperStop.Shared.Infrastructure.Services;

public class SystemDateTimeService : IDateTimeService
{
    public DateTime NowUtc => DateTime.UtcNow;
}
