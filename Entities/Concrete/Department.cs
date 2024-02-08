using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Concrete
{
    public class Department:BaseEntity
    {
        public Guid DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        [JsonIgnore]
        public ICollection<User> DepartmentUsers { get; set; }
    }
}
