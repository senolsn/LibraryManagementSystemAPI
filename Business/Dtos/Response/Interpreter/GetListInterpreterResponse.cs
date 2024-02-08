using System;

namespace Business.Dtos.Response.InterpreterResponses
{
    public class GetListInterpreterResponse
    {
        public Guid InterpreterId { get; set; }
        public string InterpreterFirstName { get; set; }
        public string InterpreterLastName { get; set; }
    
    }
}
