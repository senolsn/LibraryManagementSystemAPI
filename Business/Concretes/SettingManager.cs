using Business.Abstracts;
using Business.Dtos.Request.SettingRequests;
using Business.Dtos.Response.SettingResponses;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class SettingManager : ISettingService
    {
        private readonly ISettingDal _settingDal;
        public SettingManager(ISettingDal settingDal)
        {
            _settingDal = settingDal;
        }

        public Task<IResult> Add(CreateSettingRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Delete(DeleteSettingRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<GetSettingResponse>> GetAsync(Guid settingId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<List<GetSettingResponse>>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Update(UpdateSettingRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
