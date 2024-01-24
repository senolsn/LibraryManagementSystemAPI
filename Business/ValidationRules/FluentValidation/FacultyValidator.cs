using Business.Constants;
using Business.Dtos.Request.Faculty;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
