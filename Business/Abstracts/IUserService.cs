using Business.Dtos.Request.User;
using Business.Dtos.Response.User;
using Core.DataAccess.Paging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IUserService
    {
        Task<IResult> Add(User user);
        Task<IResult> Update(UpdateUserRequest request);
        Task<IResult> Delete(DeleteUserRequest request);
        Task<IDataResult<User>> GetAsync(Guid userId);
        //Task<IDataResult<User>> GetAsyncByFacultyId(Guid facultyId);
        Task<IDataResult<IPaginate<GetListUserResponse>>> GetPaginatedListAsync(PageRequest pageRequest);
        List<OperationClaim> GetClaims(User user);
        Task<User> GetByMail(string mail);
        Task<IResult> AddTransactionalTest(User user);
        //Task<IDataResult<User>> GetAsyncByDepartmentId(Guid departmentId);

    }
}
