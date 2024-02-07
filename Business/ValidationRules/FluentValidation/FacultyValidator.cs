using Business.Constants;
using Business.Dtos.Request.FacultyResponses;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class FacultyValidator:AbstractValidator<IFacultyRequest>
    {
        public FacultyValidator()
        {
            RuleFor(f => f.FacultyName).MinimumLength(3).WithMessage(ValidationMessages.FacultyMinLength);
            RuleFor(f => f.FacultyName).NotEmpty().WithMessage(ValidationMessages.FacultyNotEmpty);
        }
    }
}
