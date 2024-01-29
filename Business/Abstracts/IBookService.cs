using Business.Dtos.Request.BookRequests;
using Business.Dtos.Response.Book;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IBookService
    {
        Task<IResult> Add(CreateBookRequest request);
        Task<IResult> Update(UpdateBookRequest request);
        Task<IResult> Delete(DeleteBookRequest request);
        Task<IDataResult<Book>> GetAsync(Guid bookId);
        Task<IDataResult<IPaginate<GetListBookResponse>>> GetPaginatedListAsync(PageRequest pageRequest);
        Task<IDataResult<IPaginate<GetListBookResponse>>> GetPaginatedListWithAuthors(Expression<Func<Book, bool>> predicate, PageRequest pageRequest);
        Task<IDataResult<IPaginate<GetListBookResponse>>> GetPaginatedListAsyncByCategory(PageRequest pageRequest, List<Guid> categoryIds);
        Task<IDataResult<IPaginate<GetListBookResponse>>> GetPaginatedListAsyncByLanguage(PageRequest pageRequest, List<Guid> languageIds);
        Task<IDataResult<IPaginate<GetListBookResponse>>> GetPaginatedListAsyncSortedByName(PageRequest pageRequest);
        Task<IDataResult<IPaginate<GetListBookResponse>>> GetPaginatedListAsyncSortedByCreatedDate(PageRequest pageRequest);
    }
}
