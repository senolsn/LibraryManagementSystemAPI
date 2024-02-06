﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Request.StudentRequests
{
    public class CreateStudentRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
        public Guid FacultyId { get; set; }
        public Guid DepartmentId { get; set; }
        public string SchoolNumber { get; set; }
    }
}