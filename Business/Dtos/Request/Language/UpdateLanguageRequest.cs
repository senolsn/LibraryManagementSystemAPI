using System;

namespace Business.Dtos.Request.Language
{
    public class UpdateLanguageRequest
    {
        public Guid LanguageId { get; set; }
        public string LanguageName { get; set; }

    }
}
