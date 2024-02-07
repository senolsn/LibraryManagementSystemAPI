using Business.Constants;
using Business.Dtos.Request.FacultyResponses;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.FacultyValidator
{
    public class CreateFacultyValidator : AbstractValidator<CreateFacultyRequest>
    {
        public CreateFacultyValidator()
        {
            RuleFor(f => f.FacultyName).MinimumLength(2).WithMessage(ValidationMessages.FacultyMinLength);
            RuleFor(f => f.FacultyName).NotEmpty().WithMessage(ValidationMessages.FacultyNotEmpty);
        }
    }
}
