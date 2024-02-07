using Business.Constants;
using Business.Dtos.Request.Book;
using Business.Dtos.Request.BookRequests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.Book
{
    public class CreateBookValidator : AbstractValidator<CreateBookRequest>
    {
        public CreateBookValidator()
        {
            //Publisher
            RuleFor(b => b.PublisherId).NotEmpty().WithMessage(ValidationMessages.PublisherIdNotEmpty);

            //Location
            RuleFor(b => b.LocationId).NotEmpty().WithMessage(ValidationMessages.LocationIdNotEmpty);

            //Name
            RuleFor(b => b.BookName).NotEmpty().WithMessage(ValidationMessages.BookNameNotEmpty);

            //PageCount
            RuleFor(b => b.PageCount).NotEmpty().WithMessage(ValidationMessages.PageCountNotEmpty);

            //PublishedDate
            RuleFor(b => b.PublishedDate).NotEmpty().WithMessage(ValidationMessages.PublishedDateNotEmpty);

            //PublishCount
            RuleFor(b => b.PublishCount).NotEmpty().WithMessage(ValidationMessages.PublishCountNotEmpty);

            //Stock
            RuleFor(b => b.Stock).NotEmpty().WithMessage(ValidationMessages.StockNotEmpty);

            //Status
            RuleFor(b => b.Status).NotEmpty().WithMessage(ValidationMessages.StatusNotEmpty);

            //Fixture
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
            // Bu metotun içeriği sizin belirleyeceğiniz şekilde olmalıdır.
            // Örneğin, value'nun sadece rakamlardan oluşup oluşmadığını kontrol edebilirsiniz.
            return value.All(char.IsDigit);
        }
    }
}
