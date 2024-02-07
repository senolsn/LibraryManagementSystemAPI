using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class User : BaseEntity, IUser
    {
        public Guid UserId { get; set; }
        public Faculty Faculty { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public ICollection<Department> UserDepartments { get; set; }
        public ICollection<DepositBook> UserDepositBooks { get; set; }
    }
}
