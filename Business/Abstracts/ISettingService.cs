using Business.Dtos.Request.FacultyResponses;
using Business.Dtos.Request.SettingRequests;
using Business.Dtos.Response.FacultyResponses;
using Business.Dtos.Response.SettingResponses;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface ISettingService
    {
        Task<IResult> Add(CreateSettingRequest request);

        Task<IResult> Update(UpdateSettingRequest request);

        Task<IResult> Delete(DeleteSettingRequest request);

        Task<IDataResult<GetSettingResponse>> GetAsync(Guid settingId);

        Task<IDataResult<List<GetSettingResponse>>> GetListAsync();
    }
}
