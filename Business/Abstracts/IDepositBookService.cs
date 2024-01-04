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

        Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetListAsync(PageRequest pageRequest);
    }
}
