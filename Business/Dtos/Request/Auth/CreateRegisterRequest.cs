using Core.Entities.Concrete.enums;
using System;
using System.Collections.Generic;


namespace Business.Dtos.Request.Auth
{
    public class CreateRegisterRequest
    {
        public Guid FacultyId { get; set; }
        public List<Guid> DepartmentIds { get; set; }
        public string IdentyNumber { get; set; } //Personel no veya Okul Numarası
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public RoleType RoleType { get; set; }
    }
}
