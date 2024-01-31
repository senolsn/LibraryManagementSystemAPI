using Business.Constants;
using Business.Dtos.Request.Category;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.CategoryValidator
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryValidator()
        {
            RuleFor(c => c.CategoryName).MinimumLength(2).WithMessage(ValidationMessages.CategoryMinLength);
            RuleFor(c => c.CategoryName).NotEmpty().WithMessage(ValidationMessages.CategoryNotEmpty);
        }
    }
}
