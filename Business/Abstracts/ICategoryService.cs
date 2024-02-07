using Business.Dtos.Request.Category;
using Business.Dtos.Response.Book;
using Business.Dtos.Response.Category;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface ICategoryService
    {
        Task<IResult> Add(CreateCategoryRequest request);

        Task<IResult> Update(UpdateCategoryRequest request);

        Task<IResult> Delete(DeleteCategoryRequest request);

        Task<IDataResult<Category>> GetAsync(Guid categoryId);

        Task<IDataResult<IPaginate<GetListCategoryResponse>>> GetPaginatedListAsync(PageRequest pageRequest);
        Task<IDataResult<List<GetListCategoryResponse>>> GetListAsync();

        Task<IDataResult<List<GetListCategoryResponse>>> GetListAsyncSortedByName();
        Task<IDataResult<List<GetListCategoryResponse>>> GetListAsyncSortedByCreatedDate();
    }
}
