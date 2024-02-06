using Business.Dtos.Request.StaffRequests;
using Business.Dtos.Response.StaffResponses;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IStaffService
    {
        Task<IResult> Add(CreateStaffRequest request);

        Task<IDataResult<Staff>> GetAsync(Guid staffId);

        Task<IDataResult<List<GetAllStaffResponse>>> GetListAsync();
    }
}
