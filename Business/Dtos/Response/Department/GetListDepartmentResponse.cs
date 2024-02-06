using System;

namespace Business.Dtos.Response.DepartmentResponses
{
    public class GetListDepartmentResponse
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
