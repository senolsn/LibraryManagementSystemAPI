using Business.Dtos.Request.Book;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class BookValidator:AbstractValidator<IBookRequest>
    {
        public BookValidator()
        {
            RuleFor(b => b.AuthorId).NotEmpty();
            RuleFor(b => b.LanguageId).NotEmpty();
            RuleFor(b => b.CategoryId).NotEmpty();
            RuleFor(b => b.PublisherId).NotEmpty();
            RuleFor(b => b.BookName).NotEmpty();
            RuleFor(b => b.PageCount).NotEmpty();
            RuleFor(b => b.ISBNNumber).NotEmpty();
            RuleFor(b => b.PublishedDate).NotEmpty();
            //RuleFor(b => b.PublishCount).NotEmpty();
            RuleFor(b => b.Stock).NotEmpty();
            RuleFor(b => b.Status).NotEmpty();
            //RuleFor(b => b.Interpreter).NotEmpty();
            //RuleFor(b => b.FixtureNumber).NotEmpty();
        }
    }
}
