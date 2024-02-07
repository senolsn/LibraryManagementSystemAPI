using Business.Dtos.Request.Auth;
using FluentValidation;
using System.Linq;
using System.Text.RegularExpressions;


namespace Business.ValidationRules.FluentValidation.AuthValidator
{
    public class CreateRegisterValidator:AbstractValidator<CreateRegisterRequest>
    {
        public CreateRegisterValidator()
        {
            RuleFor(r => r.Email).NotEmpty();
            RuleFor(r => r.Email).Must(IsValidEmail);

            RuleFor(r => r.Password).NotEmpty();
            RuleFor(r => r.Password).MinimumLength(3);


            RuleFor(r => r.FirstName).NotEmpty();
            RuleFor(r => r.FirstName).MinimumLength(2);
            RuleFor(r => r.LastName).MinimumLength(2);
            RuleFor(r => r.LastName).NotEmpty();


            RuleFor(r => r.IdentyNumber).NotEmpty();
            RuleFor(r => r.IdentyNumber).Must(BeASchoolNumber);
            RuleFor(r => r.IdentyNumber).Must(BeAValidNumber);

            RuleFor(r => r.PhoneNumber).NotEmpty();
            RuleFor(r => r.PhoneNumber).Must(IsValidPhoneNumber);
            RuleFor(r => r.PhoneNumber).Must(BeAValidNumber);

            RuleFor(r => r.FacultyId).NotEmpty();
            RuleFor(r => r.DepartmentIds).NotEmpty();
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^\S+@uludag\.edu\.tr$";
            return Regex.IsMatch(email,pattern,RegexOptions.IgnoreCase) ? true : false;
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length == 10;
        }
        private bool BeAValidNumber(string value)
        {
            return value.All(char.IsDigit);
        }
        private bool BeASchoolNumber(string number)
        {
            return number.Length == 9;
        }
    }
}
