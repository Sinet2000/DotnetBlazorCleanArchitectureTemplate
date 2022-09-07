using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FPAAgentura.Application.Requests.Identity;
using Microsoft.Extensions.Localization;

namespace FPAAgentura.Application.Validators.Requests.Identity;

public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileRequestValidator(IStringLocalizer<UpdateProfileRequestValidator> localizer)
    {
        RuleFor(request => request.FirstName)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["First Name is required"]);
        RuleFor(request => request.LastName)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Last Name is required"]);
    }
}
