using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Request.Book
{
    public class CreateBookRequest
    {
        public Guid LanguageId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PublisherId { get; set; }
        public Guid LocationId { get; set; }
        public string BookName { get; set; }
        public int PageCount { get; set; }
        public string ISBNNumber { get; set; }
        public DateTime PublishedDate { get; set; }
        public int PublishCount { get; set; }
        public int Stock { get; set; }
        public bool Status { get; set; }
        public string Interpreter { get; set; }
        public string FixtureNumber { get; set; }
    }
}
