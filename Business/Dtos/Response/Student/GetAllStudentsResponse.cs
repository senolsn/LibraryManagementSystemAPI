using Entities.Concrete;
using System;

namespace Business.Dtos.Response.StudentResponses
{
    public class GetAllStudentsResponse
    {
        public Guid StudentId { get; set; }
        public Faculty Faculty { get; set; }
        public Guid UserId { get; set; }
        public Department Department { get; set; }
        public string SchoolNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
