using System;

namespace Business.Dtos.Request.Department
{
    public class UpdateDepartmentRequest
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
