using Core.Entities.Abstract;
using Core.Entities.Concrete;
using Core.Entities.Concrete.enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        public string SchoolNumber { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public RoleType RoleType { get; set; }

        [JsonIgnore]
        public ICollection<Department> UserDepartments { get; set; }

        [JsonIgnore]
        public ICollection<DepositBook> UserDepositBooks { get; set; }
    }
}
