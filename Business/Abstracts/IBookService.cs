using Business.Dtos.Request.Author;
using Business.Dtos.Request.Book;
using Business.Dtos.Response.Author;
using Business.Dtos.Response.Book;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IBookService
    {
        Task<IResult> Add(CreateBookRequest request);

        Task<IResult> Update(UpdateBookRequest request);

        Task<IResult> Delete(DeleteBookRequest request);

        Task<IDataResult<Book>> GetAsync(Guid bookId);

        Task<IDataResult<IPaginate<GetListBookResponse>>> GetListAsync(PageRequest pageRequest);
        Task<IDataResult<IPaginate<GetListBookResponse>>> GetListAsyncByCategory(PageRequest pageRequest,Guid categoryId);

        Task<IDataResult<IPaginate<GetListBookResponse>>> GetListAsyncSortedByName(PageRequest pageRequest);
    }
}
