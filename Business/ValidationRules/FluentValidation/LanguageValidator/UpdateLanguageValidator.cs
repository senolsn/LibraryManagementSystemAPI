using Business.Constants;
using Business.Dtos.Request.Language;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.LanguageValidator.LanguageValidator
{
    public class UpdateLanguageValidator:AbstractValidator<UpdateLanguageRequest>
    {
        public UpdateLanguageValidator()
        {
            RuleFor(l => l.LanguageName).MinimumLength(2).WithMessage(ValidationMessages.LanguageMinLength);
            RuleFor(l => l.LanguageName).NotEmpty().WithMessage(ValidationMessages.LanguageNotEmpty);
        }
    }
}
