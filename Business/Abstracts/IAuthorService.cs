using Business.Dtos.Request.Author;
using Business.Dtos.Response.Author;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IAuthorService
    {
        Task<IResult> Add(CreateAuthorRequest request);

        Task<IResult> Update(UpdateAuthorRequest request);

        Task<IResult> Delete(DeleteAuthorRequest request);

        Task<IDataResult<Author>> GetAsync(Guid authorId);

        Task<IDataResult<IPaginate<GetListAuthorResponse>>> GetListAsync(PageRequest pageRequest);

    }
}
