using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Interpreter : BaseEntity
    {
        public Guid InterpreterId { get; set; }
        public string InterpreterFirstName { get; set; }
        public string InterpreterLastName { get; set; }
        
        [JsonIgnore]
        public ICollection<Book>? InterpreterBooks { get; set; }
    }
}
