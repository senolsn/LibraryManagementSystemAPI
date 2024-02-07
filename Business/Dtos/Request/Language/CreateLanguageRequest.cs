namespace Business.Dtos.Request.LanguageRequests
{
    public class CreateLanguageRequest : ILanguageRequest
    {
        public string? LanguageName { get; set; }
    }
}
