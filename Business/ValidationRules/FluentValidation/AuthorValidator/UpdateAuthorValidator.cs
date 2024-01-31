using Business.Constants;
using Business.Dtos.Request.AuthorRequests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.AuthorValidator
{
    public class UpdateAuthorValidator:AbstractValidator<UpdateAuthorRequest>
    {
        public UpdateAuthorValidator()
        {

            RuleFor(c => c.AuthorFirstName).NotEmpty().WithMessage(ValidationMessages.AuthorFirstNameNotEmpty);
            RuleFor(c => c.AuthorFirstName).MinimumLength(2).WithMessage(ValidationMessages.AuthorMinLength);

            RuleFor(c => c.AuthorLastName).NotEmpty().WithMessage(ValidationMessages.AuthorLastNameNotEmpty);
            RuleFor(c => c.AuthorLastName).MinimumLength(2).WithMessage(ValidationMessages.AuthorMinLength);
        }
    }
}
