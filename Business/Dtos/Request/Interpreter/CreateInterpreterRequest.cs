﻿using Business.Dtos.Request.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Request.InterpreterRequests
{
    public class CreateInterpreterRequest:IInterpreterRequest
    {
        public string InterpreterFirstName { get; set; }
        public string InterpreterLastName { get; set; }
    }
}
