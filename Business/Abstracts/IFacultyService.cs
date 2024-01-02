using Business.Dtos.Request.Author;
using Business.Dtos.Request.Faculty;
using Business.Dtos.Response.Author;
using Business.Dtos.Response.Faculty;
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
    public interface IFacultyService
    {
        Task<IResult> Add(CreateFacultyRequest request);

        Task<IResult> Update(UpdateFacultyRequest request);

        Task<IResult> Delete(DeleteFacultyRequest request);

        Task<IDataResult<Faculty>> GetAsync(Guid facultyId);

        Task<IDataResult<IPaginate<GetListFacultyResponse>>> GetListAsync(PageRequest pageRequest);
    }
}
