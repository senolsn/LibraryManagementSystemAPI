using Business.Dtos.Request.Department;
using Business.Dtos.Response.Department;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IDepartmentService
    {
        Task<IResult> Add(CreateDepartmentRequest request);

        Task<IResult> Update(UpdateDepartmentRequest request);

        Task<IResult> Delete(DeleteDepartmentRequest request);

        Task<IDataResult<IPaginate<GetListDepartmentResponse>>> GetListAsync(PageRequest pageRequest);

        Task<IDataResult<Department>> GetAsync(Guid departmentId);
    }
}
