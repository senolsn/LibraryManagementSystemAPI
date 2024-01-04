using Core.Entities.Concrete;
using System;

namespace Entities.Concrete
{
    public class Location:BaseEntity
    {
        public Guid LocationId { get; set; }
        public string? Shelf { get; set; }
        public Guid CategoryId { get; set; }
    }
}
