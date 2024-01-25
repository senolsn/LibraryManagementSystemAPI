using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).MinimumLength(3);
			RuleFor(request => request.UserId).NotEmpty().WithMessage("UserId cannot be empty."); // TODO: DeleteUserValidator yazmak daha best pract. olabilir 
			RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name is required.")
								   .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.");
			RuleFor(u => u.Email).NotEmpty().WithMessage("Email address is required.")
								.EmailAddress().WithMessage("Email address is not in a correct format.");
			RuleFor(u => u.SchoolNumber)
			.Length(9).When(u => !string.IsNullOrEmpty(u.SchoolNumber))
			.WithMessage("School number must be exactly 9 digits long."); //132030018

			RuleFor(u => u.PhoneNumber).MinimumLength(10).MaximumLength(11).WithMessage("Phone number must be exactly 10 digits long."); // 05434915343
		}
	}
}
