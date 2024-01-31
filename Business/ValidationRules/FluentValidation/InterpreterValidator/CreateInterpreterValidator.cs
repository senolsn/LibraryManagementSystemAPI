using Business.Dtos.Request.InterpreterRequests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.InterpreterValidator
{
    public class CreateInterpreterValidator:AbstractValidator<CreateInterpreterRequest>
    {
        public CreateInterpreterValidator()
        {
            RuleFor(i => i.InterpreterFirstName).NotEmpty();
            RuleFor(i => i.InterpreterFirstName).MinimumLength(2);

            RuleFor(i => i.InterpreterLastName).NotEmpty();
            RuleFor(i => i.InterpreterLastName).MinimumLength(2);
        }
    }
}
