using Core.Entities.Concrete.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class User:BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid FacultyId { get; set; }
        public Guid DepartmentId { get; set; }
        public string SchoolNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public RoleType RoleType { get; set; }
    }
}
