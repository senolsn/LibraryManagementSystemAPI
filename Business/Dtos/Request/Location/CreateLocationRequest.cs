using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Request.Location
{
    public class CreateLocationRequest
    {
        public string? Shelf { get; set; }
        public Guid CategoryId { get; set; }
    }
}
