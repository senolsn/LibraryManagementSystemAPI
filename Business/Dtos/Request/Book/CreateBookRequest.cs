using Business.Dtos.Request.AuthorRequests;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using Entities.Concrete.enums;

namespace Business.Dtos.Request.BookRequests
{
    public class CreateBookRequest
    {
        public List<Guid> Authors { get; set; }
        public List<Guid> Categories { get; set; }
        public List<Guid> Languages { get; set; }
        public Guid PublisherId { get; set; }
        public Guid LocationId { get; set; }
        public string BookName { get; set; }
        public int PageCount { get; set; }
        public string ISBNNumber { get; set; }
        public DateTime PublishedDate { get; set; }
        public int PublishCount { get; set; }
        public int Stock { get; set; }
        public BookStatus Status { get; set; }
        public string Interpreter { get; set; }
        public string FixtureNumber { get; set; }
    }
}
