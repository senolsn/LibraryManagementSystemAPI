using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Response.Location
{
    public class GetListLocationResponse
    {
        public Guid LocationId { get; set; }
        public string? Shelf { get; set; }
        public Guid CategoryId { get; set; }
    }
}
