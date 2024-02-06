using Business.Dtos.Request.FacultyResponses;
using Business.Dtos.Response.FacultyResponses;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IFacultyService
    {
        Task<IResult> Add(CreateFacultyRequest request);

        Task<IResult> Update(UpdateFacultyRequest request);

        Task<IResult> Delete(DeleteFacultyRequest request);

        Task<IDataResult<Faculty>> GetAsync(Guid facultyId);

        Task<IDataResult<List<GetListFacultyResponse>>> GetListAsync();
        Task<IDataResult<List<GetListFacultyResponse>>> GetListAsyncSortedByName();
        Task<IDataResult<List<GetListFacultyResponse>>> GetListAsyncSortedByCreatedDate();

        Task<IDataResult<IPaginate<GetListFacultyResponse>>> GetPaginatedListAsync(PageRequest pageRequest);
    }
}
