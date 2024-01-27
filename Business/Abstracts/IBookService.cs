using Business.Dtos.Request.BookRequests;
using Business.Dtos.Response.Book;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
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
        Task<IDataResult<Book>> GetAsyncByCategoryId(Guid categoryId);
        Task<IDataResult<Book>> GetAsyncByLanguageId(Guid languageId);
        Task<IDataResult<IPaginate<GetListBookResponse>>> GetListAsync(PageRequest pageRequest);
        Task<IDataResult<IPaginate<Book>>> GetListWithAuthors(Expression<Func<Book, bool>> predicate, PageRequest pageRequest);
        Task<IDataResult<IPaginate<GetListBookResponse>>> GetListAsyncByCategory(PageRequest pageRequest,Guid categoryId);
        Task<IDataResult<IPaginate<GetListBookResponse>>> GetListAsyncSortedByName(PageRequest pageRequest);
        Task<IDataResult<IPaginate<GetListBookResponse>>> GetListAsyncSortedByCreatedDate(PageRequest pageRequest);
    }
}
