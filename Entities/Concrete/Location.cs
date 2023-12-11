using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Location:BaseEntity
    {
        public Guid LocationId { get; set; }
        public string Shelf { get; set; }
        public string CategoryId { get; set; }
    }
}
