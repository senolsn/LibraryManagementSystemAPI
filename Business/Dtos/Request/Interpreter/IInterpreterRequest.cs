using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Request.Interpreter
{
    public interface IInterpreterRequest
    {
        public string InterpreterFirstName { get; set; }
        public string InterpreterLastName { get; set; }
    }
}
