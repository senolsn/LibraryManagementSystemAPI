using Business.Dtos.Request.DepositBook;
using Business.Dtos.Response.DepositBook;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IDepositBookService
    {
        Task<IResult> Add(CreateDepositBookRequest request);
        Task<IResult> Update(UpdateDepositBookRequest request);
        Task<IResult> Delete(DeleteDepositBookRequest request);
        Task<IResult> GetBookBack(Guid depositBookId);
        Task<IDataResult<DepositBook>> GetAsync(Guid depositBookId);
        Task<IDataResult<DepositBook>> GetAsyncByUserId(Guid userId);
        Task<IDataResult<DepositBook>> GetAsyncByBookAndUserId(Guid bookId, Guid userId);
        Task<IDataResult<List<GetListDepositBookResponse>>> GetListAsync();
        Task<IDataResult<ICollection<GetAllDepositBooksResponse>>> GetAllAsync();
        Task<IDataResult<List<GetListDepositBookResponse>>> GetListAsyncSortedByCreatedDate();
        Task<IDataResult<List<GetListDepositBookResponse>>> GetListAsyncUndeposited();
        Task<IDataResult<List<GetListDepositBookResponse>>> GetListAsyncDeposited();
        Task<IDataResult<List<GetListDepositBookResponse>>> GetListAsyncByUserId(Guid userId);
        Task<IDataResult<List<GetListDepositBookResponse>>> GetListAsyncByBookId(Guid bookId);
        Task<IDataResult<List<GetListDepositBookResponse>>> GetPaginatedListAsyncUndepositedByUserId(Guid userId);
        Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetPaginatedListAsync(PageRequest pageRequest);
        Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetPaginatedListAsyncUndeposited(PageRequest pageRequest);
        Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetPaginatedListAsyncDeposited(PageRequest pageRequest);
        Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetPaginatedListAsyncByUserId(PageRequest pageRequest, Guid userId);
        Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetPaginatedListAsyncByBookId(PageRequest pageRequest, Guid bookId);
        Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetPaginatedListAsyncUndepositedByUserId(PageRequest pageRequest, Guid userId);


    }
}
