using Business.Dtos.Request.Category;
using Business.Dtos.Response.Category;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface ICategoryService
    {
        Task<IResult> Add(CreateCategoryRequest request);

        Task<IResult> Update(UpdateCategoryRequest request);

        Task<IResult> Delete(DeleteCategoryRequest request);

        Task<IDataResult<Category>> GetAsync(Guid categoryId);

        Task<IDataResult<IPaginate<GetListCategoryResponse>>> GetListAsync(PageRequest pageRequest);
    }
}
