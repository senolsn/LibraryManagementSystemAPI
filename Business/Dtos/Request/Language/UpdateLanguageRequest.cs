using System;

namespace Business.Dtos.Request.LanguageRequests
{
    public class UpdateLanguageRequest : ILanguageRequest
    {
        public Guid LanguageId { get; set; }
        public string LanguageName { get; set; }

    }
}
