using Core.Entities.Concrete;
using Entities.Concrete.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Concrete
{
    public class Book:BaseEntity
    {
        public Guid BookId { get; set; }
        public Publisher Publisher { get; set; }
        public Location Location { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string ISBNNumber { get; set; }
        public DateTime PublishedDate { get; set; }
        public int PublishCount { get; set; }
        public int Stock { get; set; }
        public BookStatus Status { get; set; }
        public string FixtureNumber { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Author> BookAuthors { get; set; }

        public ICollection<Category> BookCategories { get; set; }
        
        public ICollection<Language> BookLanguages { get; set; }
        
        public ICollection<Interpreter> BookInterpreters { get; set; }
    }
}
