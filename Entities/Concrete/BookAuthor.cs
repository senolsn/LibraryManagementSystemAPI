using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class BookAuthor:BaseEntity
    {
        public Guid BookAuthorId { get; set; }
        public string BookId { get; set; }
        public string AuthorId { get; set; }
    }
}
