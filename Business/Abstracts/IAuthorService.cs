using Business.Dtos.Request.Create;
using Business.Dtos.Request.Delete;
using Business.Dtos.Request.Update;
using Business.Dtos.Response.Create;
using Business.Dtos.Response.Delete;
using Business.Dtos.Response.Read;
using Business.Dtos.Response.Update;
using Core.DataAccess.Paging;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IAuthorService
    {
        Task<CreatedAuthorResponse> Add(CreateAuthorRequest request);

        Task<UpdatedAuthorResponse> Update(UpdateAuthorRequest request);

        Task<DeletedAuthorResponse> Delete(DeleteAuthorRequest request);

        Task<Author> GetAsync(Guid authorId);

        Task<IPaginate<GetListAuthorResponse>> GetListAsync(PageRequest pageRequest);

    }
}
