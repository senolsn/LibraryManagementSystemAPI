using Business.Dtos.Request.Language;
using Business.Dtos.Response.Language;
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
    public interface ILanguageService
    {
        Task<IResult> Add(CreateLanguageRequest request);

        Task<IResult> Update(UpdateLanguageRequest request);

        Task<IResult> Delete (DeleteLanguageRequest request);

        Task<IDataResult<IPaginate<GetListLanguageResponse>>> GetListAsync(PageRequest pageRequest);

        Task<IDataResult<Language>> GetAsync(Guid languageId);
    }
}
