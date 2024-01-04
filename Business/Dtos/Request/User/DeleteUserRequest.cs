using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Request.User
{
    public class DeleteUserRequest
    {
        public Guid UserId { get; set; }
    }
}
