using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Request.User
{
    public class CreateUserRequest
    {
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
