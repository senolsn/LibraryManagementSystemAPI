using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.SettingRequests;
using Business.Dtos.Response.SettingResponses;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class SettingManager : ISettingService
    {
        private readonly ISettingDal _settingDal;
        private readonly IMapper _mapper;

        public SettingManager(ISettingDal settingDal, IMapper mapper)
        {
            _settingDal = settingDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CreateSettingRequest request)
        {
            Setting setting = _mapper.Map<Setting>(request);

            var createdSetting = await _settingDal.AddAsync(setting);

            if( createdSetting is null) 
            {
                return new ErrorResult();
            }
            
            return new SuccessResult();
        }

        public async Task<IResult> Delete(DeleteSettingRequest request)
        {
            var settingToDelete = await _settingDal.GetAsync(s => s.SettingId == request.SettingId);
            
            if(settingToDelete is null ) 
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        public async Task<IDataResult<GetSettingResponse>> GetAsync(Guid settingId)
        {
           var result = await _settingDal.GetAsync(s => s.SettingId == settingId);

            var mappedResult = _mapper.Map<GetSettingResponse>(result);

            if(result is null)
            {
                return new ErrorDataResult<GetSettingResponse>(mappedResult);
            }

            return new SuccessDataResult<GetSettingResponse>(mappedResult);
        }

        public async Task<IDataResult<List<GetSettingResponse>>> GetListAsync()
        {
            var data = await _settingDal.GetListAsync();

            List<GetSettingResponse> responses  = new List<GetSettingResponse>();
            foreach (var setting in data)
            {
                var mappedSetting = _mapper.Map<GetSettingResponse>(setting);
                responses.Add(mappedSetting);
            }

            if (data is null)
            {
                return new ErrorDataResult<List<GetSettingResponse>>();
            }

            return new SuccessDataResult<List<GetSettingResponse>>(responses, Messages.SettingsListed);
        }

        public async Task<IResult> Update(UpdateSettingRequest request)
        {
            var settingToUpdate = await _settingDal.GetAsync(s => s.SettingId == request.SettingId);

            if(settingToUpdate is null)
            {
                return new ErrorResult();
            }

            _mapper.Map(request,settingToUpdate);

            await _settingDal.UpdateAsync(settingToUpdate);

            return new SuccessResult();
        }
    }
}
