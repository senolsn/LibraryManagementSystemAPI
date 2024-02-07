using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Request.Location
{
    public interface ILocationRequest
    {
        public string? Shelf { get; set; }
    }
}
