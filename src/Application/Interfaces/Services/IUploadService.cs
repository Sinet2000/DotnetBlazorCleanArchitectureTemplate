using PaperStop.Application.Requests;

namespace PaperStop.Application.Interfaces.Services;

public interface IUploadService
{
    string UploadAsync(UploadRequest request);
}
