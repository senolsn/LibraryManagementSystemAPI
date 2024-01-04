using Business.Dtos.Request.DepositBook;
using Business.Dtos.Response.DepositBook;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
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
        Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetListAsync(PageRequest pageRequest);
        Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetListAsyncUndeposited(PageRequest pageRequest);
        Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetListAsyncDeposited(PageRequest pageRequest);
        Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetListAsyncByUserId(PageRequest pageRequest, Guid userId);
        Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetListAsyncByBookId(PageRequest pageRequest, Guid bookId);
        Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetListAsyncUndepositedByUserId(PageRequest pageRequest, Guid userId);


    }
}
