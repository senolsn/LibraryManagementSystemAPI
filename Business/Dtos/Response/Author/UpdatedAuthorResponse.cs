using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Response.Author
{
    public class UpdatedAuthorResponse
    {
        public bool IsUpdated { get; set; }

        public UpdatedAuthorResponse()
        {
            IsUpdated = true;
        }
    }
}
