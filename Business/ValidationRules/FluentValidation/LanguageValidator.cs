using Business.Constants;
using Business.Dtos.Request.Language;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class LanguageValidator:AbstractValidator<CreateLanguageRequest>
    {
        public LanguageValidator()
        {
            RuleFor(l => l.LanguageName).MinimumLength(3).WithMessage(ValidationMessages.LanguageMinLength);
            RuleFor(l => l.LanguageName).NotEmpty().WithMessage(ValidationMessages.LanguageNotEmpty);
        }
    }
}
