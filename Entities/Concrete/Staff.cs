using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("Staffs")]
    public class Staff : BaseEntity, IUser, IEntity
    {
        public Guid StaffId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid FacultyId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

    }
}
