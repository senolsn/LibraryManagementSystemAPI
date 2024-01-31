using Business.Dtos.Request.Location;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.LocationValidator
{
    public class CreateLocationValidator:AbstractValidator<CreateLocationRequest>
    {
        public CreateLocationValidator()
        {
            RuleFor(l => l.Shelf).NotEmpty();
            RuleFor(l => l.Shelf).MinimumLength(2);
        }
    }
}
