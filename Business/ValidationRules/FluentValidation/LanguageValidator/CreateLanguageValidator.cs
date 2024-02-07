using Business.Constants;
using Business.Dtos.Request.LanguageRequests;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.LanguageValidator
{
    public class CreateLanguageValidator : AbstractValidator<ILanguageRequest>
    {
        public CreateLanguageValidator()
        {
            RuleFor(l => l.LanguageName).MinimumLength(2).WithMessage(ValidationMessages.LanguageMinLength);
            RuleFor(l => l.LanguageName).NotEmpty().WithMessage(ValidationMessages.LanguageNotEmpty);
        }
    }
}
