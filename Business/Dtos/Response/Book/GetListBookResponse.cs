using Business.Dtos.Response.Author;
using Business.Dtos.Response.Category;
using Business.Dtos.Response.InterpreterResponses;
using Business.Dtos.Response.Language;
using Business.Dtos.Response.Location;
using Business.Dtos.Response.Publisher;
using Entities.Concrete.enums;
using System;
using System.Collections.Generic;

namespace Business.Dtos.Response.Book
{
    public class GetListBookResponse
    {
        public Guid BookId { get; set; }
        public GetListPublisherResponse Publisher { get; set; }
        public GetListLocationResponse Location { get; set; }
        public string BookName { get; set; }
        public int PageCount { get; set; }
        public string ISBNNumber { get; set; }
        public DateTime PublishedDate { get; set; }
        public int PublishCount { get; set; }
        public int Stock { get; set; }
        public BookStatus Status { get; set; }
        public string FixtureNumber { get; set; }
        public List<GetListAuthorResponse> BookAuthors { get; set; }
        public List<GetListCategoryResponse> BookCategories { get; set; }
        public List<GetListLanguageResponse> BookLanguages { get; set; }
        public List<GetListInterpreterResponse> BookInterpreters { get; set; }
    }
}
