using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Book:BaseEntity
    {
        public Guid BookId { get; set; }
        public string LanguageId { get; set; }
        public string CategoryId { get; set; }
        public string PublisherId { get; set; }
        public string LocationId { get; set; }
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
