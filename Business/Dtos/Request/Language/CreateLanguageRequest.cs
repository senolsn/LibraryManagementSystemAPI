namespace Business.Dtos.Request.Language
{
    public class CreateLanguageRequest : ILanguageRequest
    {
        public string? LanguageName { get; set; }
    }
}
