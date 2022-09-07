using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FPAAgentura.Application.Requests.Identity;
using Microsoft.Extensions.Localization;

namespace FPAAgentura.Application.Validators.Requests.Identity;

public class TokenRequestValidator : AbstractValidator<TokenRequest>
{
    public TokenRequestValidator(IStringLocalizer<TokenRequestValidator> localizer)
    {
        RuleFor(request => request.Email)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Email is required"])
            .EmailAddress().WithMessage(x => localizer["Email is not correct"]);
        RuleFor(request => request.Password)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Password is required!"]);
    }
}
