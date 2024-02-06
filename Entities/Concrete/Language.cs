using Core.Entities.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Concrete
{
    public class Language : BaseEntity
    {
        public Guid LanguageId { get; set; }
        public string? LanguageName { get; set; }

        [JsonIgnore]
        public ICollection<Book> LanguageBooks { get; set; }

    }
}
