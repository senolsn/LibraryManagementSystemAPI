using Core.Entities.Concrete;
using System;

namespace Entities.Concrete
{
    public class Language : BaseEntity
    {
        public Guid LanguageId { get; set; }
        public string? LanguageName { get; set; }

    }
}
