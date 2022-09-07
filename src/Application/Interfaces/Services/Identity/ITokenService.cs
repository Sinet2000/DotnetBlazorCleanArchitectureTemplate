﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPAAgentura.Application.Interfaces.Common;
using FPAAgentura.Application.Requests.Identity;
using FPAAgentura.Application.Responses.Identity;
using FPAAgentura.Shared.Wrapper;

namespace FPAAgentura.Application.Interfaces.Services.Identity;

public interface ITokenService : IService
{
    Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

    Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
}
