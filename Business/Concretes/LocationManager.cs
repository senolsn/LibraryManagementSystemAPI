using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Location;
using Business.Dtos.Response.Department;
using Business.Dtos.Response.Location;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class LocationManager : ILocationService
    {
        protected readonly ILocationDal _locationDal;
        protected readonly IMapper _mapper;

        public LocationManager(ILocationDal locationDal, IMapper mapper)
        {
            _locationDal = locationDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CreateLocationRequest request)
        {
            Location location = _mapper.Map<Location>(request);

            var createdLocation = await _locationDal.AddAsync(location);

            if(createdLocation is null) 
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.LocationAdded);
        }

        public async Task<IResult> Update(UpdateLocationRequest request)
        {
           var locationToUpdate = await _locationDal.GetAsync(l => l.LocationId == request.LocationId);

            if(locationToUpdate is null)
            {
                return new ErrorResult(Messages.Error);
            }

            _mapper.Map(request, locationToUpdate);
            await _locationDal.UpdateAsync(locationToUpdate);

            return new SuccessResult(Messages.LocationUpdated);
        }

        public async Task<IResult> Delete(DeleteLocationRequest request)
        {
            var locationToDelete = await _locationDal.GetAsync(l => l.LocationId == request.LocationId);

            if(locationToDelete is not null)
            {
                await _locationDal.DeleteAsync(locationToDelete);
                return new SuccessResult(Messages.LocationDeleted);
            }
            return new ErrorResult(Messages.Error);
        }

        public async Task<IDataResult<Location>> GetAsync(Guid departmentId)
        {
            var result = await _locationDal.GetAsync(l => l.LocationId == departmentId);

            if(result is not null)
            {
                return new SuccessDataResult<Location>(result, Messages.LocationListed);
            }
            return new ErrorDataResult<Location>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<GetListLocationResponse>>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _locationDal.GetListAsync(
                null,
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize);

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListLocationResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListLocationResponse>>(result, Messages.AuthorsListed);
            }

            return new ErrorDataResult<IPaginate<GetListLocationResponse>>(Messages.Error);
        }

       
    }
}
