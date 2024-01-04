using Core.Entities.Concrete;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Department:BaseEntity
    {
        public Guid DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

    }
}
