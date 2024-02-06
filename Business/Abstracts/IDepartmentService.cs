using Business.Dtos.Request.DepartmentRequests;
using Business.Dtos.Response.DepartmentResponses;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IDepartmentService
    {
        Task<IResult> Add(CreateDepartmentRequest request);

        Task<IResult> Update(UpdateDepartmentRequest request);

        Task<IResult> Delete(DeleteDepartmentRequest request);
        Task<IDataResult<List<GetListDepartmentResponse>>> GetListAsync();
        Task<IDataResult<List<GetListDepartmentResponse>>> GetListAsyncSortedByName();
        Task<IDataResult<List<GetListDepartmentResponse>>> GetListAsyncSortedByCreatedDate();

        Task<IDataResult<IPaginate<GetListDepartmentResponse>>> GetPaginatedListAsync(PageRequest pageRequest);

        Task<IDataResult<Department>> GetAsync(Guid departmentId);
    }
}
