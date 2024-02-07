using Business.Dtos.Request.LanguageRequests;
using Business.Dtos.Response.Category;
using Business.Dtos.Response.LanguageResponses;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface ILanguageService
    {
        Task<IResult> Add(CreateLanguageRequest request);

        Task<IResult> Update(UpdateLanguageRequest request);

        Task<IResult> Delete (DeleteLanguageRequest request);

        Task<IDataResult<List<GetListLanguageResponse>>> GetListAsync();
        Task<IDataResult<List<GetListLanguageResponse>>> GetListAsyncSortedByName();
        Task<IDataResult<List<GetListLanguageResponse>>> GetListAsyncSortedByCreatedDate();

        Task<IDataResult<IPaginate<GetListLanguageResponse>>> GetPaginatedListAsync(PageRequest pageRequest);

        Task<IDataResult<Language>> GetAsync(Guid languageId);
    }
}
