<<<<<<< HEAD
﻿using Core.Entities.Concrete.enums;
=======
﻿using Entities.Concrete.enums;
>>>>>>> 5c43c7567816add2417b815efb5faed65d391e24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Request.Auth
{
    public class CreateRegisterRequest
    {
<<<<<<< HEAD
        public Guid FacultyId { get; set; }
        public Guid? DepartmentId { get; set; }
        public string? SchoolNumber { get; set; }
=======
>>>>>>> 5c43c7567816add2417b815efb5faed65d391e24
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
<<<<<<< HEAD
        public RoleType RoleType { get; set; }
=======
        public UserType UserType { get; set; }
>>>>>>> 5c43c7567816add2417b815efb5faed65d391e24
    }
}
