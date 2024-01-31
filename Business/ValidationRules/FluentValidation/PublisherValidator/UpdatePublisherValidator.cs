using Business.Dtos.Request.Publisher;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.PublisherValidator
{
    public class UpdatePublisherValidator:AbstractValidator<UpdatePublisherRequest>
    {
        public UpdatePublisherValidator()
        {
            RuleFor(p => p.PublisherName).NotEmpty();
            RuleFor(p => p.PublisherName).MinimumLength(2);
        }
    }
}
