using Business.Constants;
using Business.Dtos.Request.LanguageRequests;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class LanguageValidator:AbstractValidator<ILanguageRequest>
    {
        public LanguageValidator()
        {
            RuleFor(l => l.LanguageName).MinimumLength(3).WithMessage(ValidationMessages.LanguageMinLength);
            RuleFor(l => l.LanguageName).NotEmpty().WithMessage(ValidationMessages.LanguageNotEmpty);
        }
    }
}
