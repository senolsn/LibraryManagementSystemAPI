using Core.Entities.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities.Concrete
{
    public class Publisher:BaseEntity
    {
        public Guid PublisherId { get; set; }
        public string PublisherName { get; set; }
        [JsonIgnore]
        public ICollection<Book> Books { get; set; }
    }
}
