using Core.Entities.Concrete;
using System;

namespace Entities.Concrete
{
    public class Faculty:BaseEntity
    {
        public Guid FacultyId { get; set; }
        public string FacultyName { get; set; }
    }
}
