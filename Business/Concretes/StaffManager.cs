using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.StaffRequests;
using Business.Dtos.Response.Language;
using Business.Dtos.Response.StaffResponses;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class StaffManager : IStaffService
    {
        private readonly IStaffDal _staffDal;
        private readonly IMapper _mapper;

        public StaffManager(IStaffDal staffDal)
        {
            _staffDal = staffDal;
        }

        public async Task<IResult> Add(CreateStaffRequest request)
        {

            Staff staff = _mapper.Map<Staff>(request);

            var createdStaff = await _staffDal.AddAsync(staff);

            var dbResult = await _staffDal.SaveChangesAsync();

            if (!dbResult)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.StaffAdded);
        }

        public async Task<IDataResult<Staff>> GetAsync(Guid staffId)
        {
            var result = await _staffDal.GetAsync(s => s.StaffId == staffId);

            if (result is not null)
            {
                return new SuccessDataResult<Staff>(result, Messages.StaffListed);
            }

            return new ErrorDataResult<Staff>(Messages.Error);
        }

        public async Task<IDataResult<List<GetAllStaffResponse>>> GetListAsync()
        {
            var data = await _staffDal.GetListAsync(null);

            if (data is not null)
            {
                var staffsResponse = _mapper.Map<List<GetAllStaffResponse>>(data);

                return new SuccessDataResult<List<GetAllStaffResponse>>(staffsResponse, Messages.StaffsListed);
            }

            return new ErrorDataResult<List<GetAllStaffResponse>>(Messages.Error);
        }
    }
}
