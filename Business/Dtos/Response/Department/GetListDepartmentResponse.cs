using System;

namespace Business.Dtos.Response.Department
{
    public class GetListDepartmentResponse
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
