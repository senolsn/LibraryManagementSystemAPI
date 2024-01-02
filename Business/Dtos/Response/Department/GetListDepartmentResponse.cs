using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Response.Department
{
    public class GetListDepartmentResponse
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
