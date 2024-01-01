using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Response.Language
{
    public class UpdatedLanguageResponse
    {
        public bool IsUpdated { get; set; }

        public UpdatedLanguageResponse()
        {
            IsUpdated = true;
        }
    }
}
