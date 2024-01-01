using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Request.Author
{
    public class CreateAuthorRequest
    {
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
    }
}
