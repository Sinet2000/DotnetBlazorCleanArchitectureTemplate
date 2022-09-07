using PaperStop.Application.Interfaces.Common;

namespace PaperStop.Application.Interfaces.Services;

public interface ICurrentUserService : IService
{
    string UserId { get; }
}
