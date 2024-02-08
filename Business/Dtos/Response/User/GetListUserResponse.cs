using Business.Dtos.Response.DepartmentResponses;
using Business.Dtos.Response.DepositBook;
using Business.Dtos.Response.FacultyResponses;
using Core.Entities.Concrete.enums;
using System;
using System.Collections.Generic;

namespace Business.Dtos.Response.UserResponses
{
    public class GetListUserResponse
    {
        public Guid UserId { get; set; }
        public List<GetListDepartmentResponse> UserDepartments { get; set; }
        public List<GetListDepositBookResponse> UserDepositBooks { get; set; }
        public GetListFacultyResponse Faculty { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SchoolNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public RoleType RoleType { get; set; }
    }
}
