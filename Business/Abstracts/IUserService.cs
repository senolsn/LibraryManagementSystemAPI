using Business.Dtos.Request.User;
using Business.Dtos.Response.User;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IUserService
    {
        Task<IResult> Add(CreateUserRequest request);

        Task<IResult> Update(UpdateUserRequest request);

        Task<IResult> Delete(DeleteUserRequest request);

        Task<IDataResult<User>> GetAsync(Guid userId);

        Task<IDataResult<IPaginate<GetListUserResponse>>> GetListAsync(PageRequest pageRequest);
    }
}
