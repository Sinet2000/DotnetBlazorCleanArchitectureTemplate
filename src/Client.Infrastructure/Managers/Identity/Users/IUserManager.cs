﻿using PaperStop.Application.Requests.Identity;
using PaperStop.Application.Responses.Identity;
using PaperStop.Shared.Wrapper;

namespace PaperStop.Client.Infrastructure.Managers.Identity.Users;

public interface IUserManager : IManager
{
    Task<IResult<List<UserResponse>>> GetAllAsync();

    Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request);

    Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);

    Task<IResult<UserResponse>> GetAsync(string userId);

    Task<IResult<UserRolesResponse>> GetRolesAsync(string userId);

    Task<IResult> RegisterUserAsync(RegisterRequest request);

    Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request);

    Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request);

    Task<string> ExportToExcelAsync(string searchString = "");
}