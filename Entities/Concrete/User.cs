using Core.Entities.Concrete;
using System;

namespace Entities.Concrete
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid FacultyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string Card { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}
