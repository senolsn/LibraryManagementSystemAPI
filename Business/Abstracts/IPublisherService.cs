﻿using Business.Dtos.Request.Publisher;
using Business.Dtos.Response.Publisher;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IPublisherService
    {
        Task<IResult> Add(CreatePublisherRequest request);

        Task<IResult> Update(UpdatePublisherRequest request);

        Task<IResult> Delete(DeletePublisherRequest request);

        Task<IDataResult<Publisher>> GetAsync(Guid publisherId);

        Task<IDataResult<IPaginate<GetListPublisherResponse>>> GetPaginatedListAsync(PageRequest pageRequest);
    }
}
