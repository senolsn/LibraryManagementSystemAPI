using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Location;
using Business.Dtos.Response.Location;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class LocationManager : ILocationService
    {
        protected readonly ILocationDal _locationDal;
        protected readonly IBookDal _bookDal;
        protected readonly IMapper _mapper;

        public LocationManager(ILocationDal locationDal, IBookDal bookDal, IMapper mapper)
        {
            _locationDal = locationDal;
            _bookDal = bookDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CreateLocationRequest request)
        {
            Location location = _mapper.Map<Location>(request);

            var createdLocation = await _locationDal.AddAsync(location);

            var dbResult = await _locationDal.SaveChangesAsync();

            if(!dbResult) 
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
                if (CheckIfExistInBooks(request.LocationId))
                {
                    return new ErrorResult(Messages.LocationExistInBooks);
                }
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

        public async Task<IDataResult<IPaginate<GetListLocationResponse>>> GetPaginatedListAsync(PageRequest pageRequest)
        {
            var data = await _locationDal.GetPaginatedListAsync(
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
        
        private bool CheckIfExistInBooks(Guid locationId)
        {
            if(_bookDal.GetAsync(b => b.Location.LocationId == locationId) is null)
            {
                return false;
            }
            return true;
        }

       
    }
}
