using Business.Dtos.Request.Category;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CategoryValidator:AbstractValidator<CreateCategoryRequest>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName).MinimumLength(3);
        }
    }
}
