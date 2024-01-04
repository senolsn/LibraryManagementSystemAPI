using Core.Entities.Concrete;
using System;

namespace Entities.Concrete
{
    public class Author:BaseEntity
    {
        public Guid AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }

    }
}
