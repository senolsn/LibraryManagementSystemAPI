using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Request.Update
{
    public class UpdateLanguageRequest
    {
        public Guid LanguageId { get; set; }
        public string LanguageName { get; set; }

    }
}
