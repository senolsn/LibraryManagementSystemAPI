using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Response.Language
{
    public class CreatedLanguageResponse
    {
        public Guid LanguageId { get; set; }
        public string? LanguageName { get; set; }
    }
}
