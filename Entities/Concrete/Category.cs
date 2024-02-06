using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Concrete
{
    public class Category:BaseEntity
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Book>? CategoryBooks { get; set; }
    }
}

