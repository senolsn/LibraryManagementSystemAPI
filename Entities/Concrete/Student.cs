using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    [Table("Students")]
    public class Student : BaseEntity, IUser, IEntity
    {
        public Guid StudentId { get; set; }
        public Guid FacultyId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid DepartmentId { get; set; }
        public string SchoolNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
