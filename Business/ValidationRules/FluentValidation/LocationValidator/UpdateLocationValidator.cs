using Business.Dtos.Request.Location;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.LocationValidator
{
    public class UpdateLocationValidator:AbstractValidator<UpdateLocationRequest>
    {
        public UpdateLocationValidator()
        {
            RuleFor(l => l.Shelf).NotEmpty();
            RuleFor(l => l.Shelf).MinimumLength(2);
        }
    }
}
