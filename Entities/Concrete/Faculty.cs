using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities.Concrete
{
    public class Faculty:BaseEntity
    {
        public Guid FacultyId { get; set; }
        public string FacultyName { get; set; }

        [JsonIgnore]
        public ICollection<User> FacultyUsers { get; set; }
    }
}
