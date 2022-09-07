using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPAAgentura.Application.Interfaces.Common;

namespace FPAAgentura.Application.Interfaces.Services;

public interface ICurrentUserService : IService
{
    string UserId { get; }
}
