using Business.Dtos.Request.StudentRequests;
using Business.Dtos.Response.StudentResponses;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IStudentService  
    {
        Task<IResult> Add(CreateStudentRequest request);

        Task<IDataResult<Student>> GetAsync(Guid studentId);

        Task<IDataResult<List<GetAllStudentsResponse>>> GetListAsync();
    }
}
