using Business.Constants;
using Business.Dtos.Request.BookRequests;
using FluentValidation;
using System;
using System.Linq;

namespace Business.ValidationRules.FluentValidation.BookValidator
{
    public class CreateBookValidator : AbstractValidator<CreateBookRequest>
    {
        public CreateBookValidator()
        {

            //Publisher
            RuleFor(b => b.PublisherId).NotEmpty().WithMessage(ValidationMessages.PublisherIdNotEmpty);

            //Location
            RuleFor(b => b.LocationId).NotEmpty().WithMessage(ValidationMessages.LocationIdNotEmpty);

            //BookName
            RuleFor(b => b.BookName).NotEmpty().WithMessage(ValidationMessages.BookNameNotEmpty);

            //PageCount
            RuleFor(b => b.PageCount).NotEmpty().WithMessage(ValidationMessages.PageCountNotEmpty);
            RuleFor(b => b.PageCount).Must(IsNumberNegative).WithMessage(ValidationMessages.NumberNegative);

            //PublishedDate
            RuleFor(b => b.PublishedDate).NotEmpty().WithMessage(ValidationMessages.PublishedDateNotEmpty);
            RuleFor(b => b.PublishedDate).Must(IsDateCorrect).WithMessage(ValidationMessages.PublishDate);

            //PublishCount
            RuleFor(b => b.PublishCount).NotEmpty().WithMessage(ValidationMessages.PublishCountNotEmpty);
            RuleFor(b => b.PageCount).Must(IsNumberNegative);

            //Stock
            RuleFor(b => b.Stock).NotEmpty().WithMessage(ValidationMessages.StockNotEmpty);
            RuleFor(b => b.PageCount).Must(IsNumberNegative);

            //Status
            RuleFor(b => b.Status).NotEmpty().WithMessage(ValidationMessages.StatusNotEmpty);

            //FixtureNumber
            RuleFor(b => b.FixtureNumber)
                .NotEmpty().WithMessage(ValidationMessages.FixtureNumberNotEmpty)
                .Must(BeAValidNumber).WithMessage(ValidationMessages.FixtureNumberInvalid);

            //ISBN
            RuleFor(b => b.ISBNNumber)
                .NotEmpty().WithMessage(ValidationMessages.ISBNNumberNotEmpty)
                .Length(10, 13).WithMessage(ValidationMessages.ISBNNumberLengthInvalid)
                .Must(BeAValidNumber).WithMessage(ValidationMessages.ISBNNumberInvalid);

            //Authors
            RuleFor(b => b.AuthorIds).Must(a => a != null && a.Any()).WithMessage(ValidationMessages.AuthorsNotEmpty);

            //Categories
            RuleFor(b => b.CategoryIds).Must(c => c != null && c.Any()).WithMessage(ValidationMessages.CategoriesNotEmpty);

            //Languages
            RuleFor(b => b.LanguageIds).Must(l => l != null && l.Any()).WithMessage(ValidationMessages.LanguagesNotEmpty);
        }

        private bool BeAValidNumber(string value)
        {
            return value.All(char.IsDigit);
        }

        private bool IsNumberNegative(int value)
        {
            return (value >= 0);
        }

        private bool IsDateCorrect(DateTime date)
        {
            return (date > DateTime.Now);
        }
    }
}
