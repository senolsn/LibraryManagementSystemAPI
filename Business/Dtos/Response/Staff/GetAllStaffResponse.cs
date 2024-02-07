using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Response.StaffResponses
{
    public class GetAllStaffResponse
    {
        public Guid StaffId { get; set; }
        public Guid UserId { get; set; }
        public Faculty Faculty { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
