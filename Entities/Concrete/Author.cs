using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities.Concrete
{
    public class Author:BaseEntity
    {
        public Guid AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Book>? AuthorBooks { get; set; }
    }
}
