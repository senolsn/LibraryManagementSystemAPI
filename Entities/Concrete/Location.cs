using Core.Entities.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Location:BaseEntity
    {
        public Guid LocationId { get; set; }
        public string? Shelf { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
