﻿using System;

namespace Business.Dtos.Request.DepartmentRequests
{
    public class UpdateDepartmentRequest:IDepartmentRequest
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
