using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPAAgentura.Application.Requests;

namespace FPAAgentura.Application.Interfaces.Services;

public interface IUploadService
{
    string UploadAsync(UploadRequest request);
}
