using Business.Dtos.Request.Location;
using Business.Dtos.Response.Location;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface ILocationService
    {
        Task<IResult> Add(CreateLocationRequest request);

        Task<IResult> Update(UpdateLocationRequest request);

        Task<IResult> Delete(DeleteLocationRequest request);

        Task<IDataResult<IPaginate<GetListLocationResponse>>> GetPaginatedListAsync(PageRequest pageRequest);

        Task<IDataResult<Location>> GetAsync(Guid locationId);
        Task<IDataResult<List<GetListLocationResponse>>> GetListAsync();
        Task<IDataResult<List<GetListLocationResponse>>> GetListAsyncSortedByName();
        Task<IDataResult<List<GetListLocationResponse>>> GetListAsyncSortedByCreatedDate();
    }
}
