using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Request.Department
{
    public class UpdateDepartmentRequest
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
