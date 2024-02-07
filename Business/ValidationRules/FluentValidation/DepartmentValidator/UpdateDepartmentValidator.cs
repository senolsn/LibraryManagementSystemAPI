using Business.Dtos.Request.DepartmentRequests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.DepartmentValidator
{
    public class UpdateDepartmentValidator:AbstractValidator<UpdateDepartmentRequest>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(d => d.DepartmentName).NotEmpty();
            RuleFor(d => d.DepartmentName).MinimumLength(2);
        }
    }
}
