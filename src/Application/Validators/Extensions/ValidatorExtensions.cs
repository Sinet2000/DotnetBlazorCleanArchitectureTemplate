using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;

namespace FPAAgentura.Application.Validators.Extensions;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> MustBeJson<T>(this IRuleBuilder<T, string> ruleBuilder, IPropertyValidator<T, string> validator) where T : class
    {
        return ruleBuilder.SetValidator(validator);
    }
}
