using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Response.Create
{
    public class CreatedAuthorResponse
    {
        public Guid AuthorId { get; set; }
        public string AuthorFirstName { get; set ; }
        public string AuthorLastName { get; set ; }
    }
}
