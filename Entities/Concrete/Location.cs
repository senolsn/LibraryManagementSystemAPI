using Core.Entities.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Concrete
{
    public class Location:BaseEntity
    {
        public Guid LocationId { get; set; }
        public string? Shelf { get; set; }

        [JsonIgnore]
        public ICollection<Book> LocationBooks { get; set; }

    }
}
