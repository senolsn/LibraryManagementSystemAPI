using Core.Entities.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Language : BaseEntity
    {
        public Guid LanguageId { get; set; }
        public string? LanguageName { get; set; }
        public ICollection<Book> LanguageBooks { get; set; }

    }
}
