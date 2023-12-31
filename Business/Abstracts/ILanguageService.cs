using Business.Dtos.Request.Create;
using Business.Dtos.Request.Delete;
using Business.Dtos.Request.Update;
using Business.Dtos.Response.Create;
using Business.Dtos.Response.Delete;
using Business.Dtos.Response.Update;
using Core.DataAccess.Paging;
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
        Task<CreatedLanguageResponse> Add(CreateLanguageRequest request);

        Task<UpdatedLanguageResponse> Update(UpdateLanguageRequest request);

        Task<DeletedLanguageResponse> Delete (DeleteLanguageRequest request);

        Task<IPaginate<Language>> GetListAsync(PageRequest pageRequest);

        Task<Language> GetAsync(Guid id);
    }
}
