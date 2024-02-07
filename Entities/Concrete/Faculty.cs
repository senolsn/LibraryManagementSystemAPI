using Core.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Faculty:BaseEntity
    {
        public Guid FacultyId { get; set; }
        public string FacultyName { get; set; }
        public ICollection<User> FacultyUsers { get; set; }
    }
}
