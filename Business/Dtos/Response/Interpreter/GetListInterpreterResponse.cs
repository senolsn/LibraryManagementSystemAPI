using Business.Dtos.Response.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Response.InterpreterResponses
{
    public class GetListInterpreterResponse
    {
        public Guid InterpreterId { get; set; }
        public string InterpreterFirstName { get; set; }
        public string InterpreterLastName { get; set; }
        public List<GetListBookResponse> InterpreterBooks { get; set; }
    }
}
