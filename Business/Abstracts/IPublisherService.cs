using Business.Dtos.Request.Publisher;
using Business.Dtos.Response.Publisher;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IPublisherService
    {
        Task<IResult> Add(CreatePublisherRequest request);

        Task<IResult> Update(UpdatePublisherRequest request);

        Task<IResult> Delete(DeletePublisherRequest request);

        Task<IDataResult<Publisher>> GetAsync(Guid publisherId);

        Task<IDataResult<List<GetListPublisherResponse>>> GetListAsync();
        Task<IDataResult<List<GetListPublisherResponse>>> GetListAsyncSortedByName();
        Task<IDataResult<List<GetListPublisherResponse>>> GetListAsyncSortedByCreatedDate();

        Task<IDataResult<IPaginate<GetListPublisherResponse>>> GetPaginatedListAsync(PageRequest pageRequest);
    }
}
