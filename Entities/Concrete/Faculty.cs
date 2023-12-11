using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Faculty:BaseEntity
    {
        public Guid FacultyId { get; set; }
        public string FacultyName { get; set; }
    }
}
