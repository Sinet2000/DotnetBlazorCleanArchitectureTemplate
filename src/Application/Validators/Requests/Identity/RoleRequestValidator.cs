using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FPAAgentura.Application.Requests.Identity;
using Microsoft.Extensions.Localization;

namespace FPAAgentura.Application.Validators.Requests.Identity;

public class RoleRequestValidator : AbstractValidator<RoleRequest>
{
    public RoleRequestValidator(IStringLocalizer<RoleRequestValidator> localizer)
    {
        RuleFor(request => request.Name)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required"]);
    }
}
