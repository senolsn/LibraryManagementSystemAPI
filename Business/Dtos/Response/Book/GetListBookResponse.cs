using Business.Dtos.Response.Author;
using Business.Dtos.Response.Category;
using Business.Dtos.Response.InterpreterResponses;
<<<<<<< HEAD
using Business.Dtos.Response.LanguageResponses;
=======
using Business.Dtos.Response.Language;
>>>>>>> 5c43c7567816add2417b815efb5faed65d391e24
using Business.Dtos.Response.Location;
using Business.Dtos.Response.Publisher;
using Entities.Concrete.enums;
using System;
<<<<<<< HEAD
=======
using System.Collections.Generic;
using System.Text.Json.Serialization;
>>>>>>> 5c43c7567816add2417b815efb5faed65d391e24

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
        public string ImageUrl { get; set; }
        public List<GetListAuthorResponse> BookAuthors { get; set; }
        public List<GetListCategoryResponse> BookCategories { get; set; }
        public List<GetListLanguageResponse> BookLanguages { get; set; }
        public List<GetListInterpreterResponse> BookInterpreters { get; set; }
    }
}
