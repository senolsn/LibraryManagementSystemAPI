using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Department:BaseEntity
    {
        public Guid DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

    }
}
