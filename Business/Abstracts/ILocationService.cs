using Business.Dtos.Request.Department;
using Business.Dtos.Request.Location;
using Business.Dtos.Response.Department;
using Business.Dtos.Response.Location;
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
    public interface ILocationService
    {
        Task<IResult> Add(CreateLocationRequest request);

        Task<IResult> Update(UpdateLocationRequest request);

        Task<IResult> Delete(DeleteLocationRequest request);

        Task<IDataResult<IPaginate<GetListLocationResponse>>> GetListAsync(PageRequest pageRequest);

        Task<IDataResult<Location>> GetAsync(Guid departmentId);
    }
}
