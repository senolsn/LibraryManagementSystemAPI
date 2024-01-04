using Core.Entities.Concrete;
using System;

namespace Entities.Concrete
{
    public class Department:BaseEntity
    {
        public Guid DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

    }
}
