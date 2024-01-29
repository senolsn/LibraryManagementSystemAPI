using Business.Dtos.Request.InterpreterRequests;
using Business.Dtos.Response.InterpreterResponses;
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
    public interface IInterpreterService
    {
        Task<IResult> Add(CreateInterpreterRequest request);

        Task<IResult> Update(UpdateInterpreterRequest request);

        Task<IResult> Delete(DeleteInterpreterRequest request);

        Task<IDataResult<GetListInterpreterResponse>> GetAsync(Guid interpreterId);

        Task<IDataResult<IPaginate<GetListInterpreterResponse>>> GetListAsync(PageRequest pageRequest);

    }
}
