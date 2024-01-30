using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Faculty;
using Business.Dtos.Request.Location;
using Business.Dtos.Response.Location;
using Core.DataAccess.Paging;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class LocationManager : ILocationService
    {
        protected readonly ILocationDal _locationDal;
        protected readonly IBookService _bookService;
        protected readonly IMapper _mapper;

        public LocationManager(ILocationDal locationDal, IBookService bookService, IMapper mapper)
        {
            _locationDal = locationDal;
            _bookService = bookService;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CreateLocationRequest request)
        {
            var result = BusinessRules.Run(CapitalizeFirstLetter(request),await IsLocationNameUnique(request.Shelf));

            if(result is not null)
            {
                return result;
            }
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
            var result = BusinessRules.Run(CapitalizeFirstLetter(request), await IsLocationNameUnique(request.Shelf));

            if (result is not null)
            {
                return result;
            }

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
                var result = BusinessRules.Run(await CheckIfExistInBooks(locationToDelete.LocationId));

                if(result is not null)
                {
                    return result;
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

        public async Task<IDataResult<List<GetListLocationResponse>>> GetListAsync()
        {
            var data = await _locationDal.GetListAsync(null);

            if (data is not null)
            {
                var locationResponse = _mapper.Map<List<GetListLocationResponse>>(data);

                return new SuccessDataResult<List<GetListLocationResponse>>(locationResponse, Messages.AuthorsListed);
            }

            return new ErrorDataResult<List<GetListLocationResponse>>(Messages.Error);
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

        public async Task<IDataResult<List<GetListLocationResponse>>> GetListAsyncSortedByName()
        {
            var data = await _locationDal.GetListAsyncOrderBy(
                predicate: null,
                orderBy: q => q.OrderBy(l => l.Shelf)
                );

            if (data is not null)
            {
                var locationResponse = _mapper.Map<List<GetListLocationResponse>>(data);
                return new SuccessDataResult<List<GetListLocationResponse>>(locationResponse);
            }
            return new ErrorDataResult<List<GetListLocationResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListLocationResponse>>> GetListAsyncSortedByCreatedDate()
        {
            var data = await _locationDal.GetListAsyncOrderBy(
              predicate: null,
              orderBy: q => q.OrderBy(l => l.CreatedDate)
              );

            if (data is not null)
            {
                var locationResponse = _mapper.Map<List<GetListLocationResponse>>(data);
                return new SuccessDataResult<List<GetListLocationResponse>>(locationResponse);
            }
            return new ErrorDataResult<List<GetListLocationResponse>>(Messages.Error);
        }


        #region Helper Methods
        private async Task<IResult> CheckIfExistInBooks(Guid locationId)
        {
            var result = await _bookService.GetAsyncByLocation(locationId);
            if (result.IsSuccess)
            {
                return new ErrorResult(Messages.LocationExistInBooks);
            }
            return new SuccessResult();
        }

        private IDataResult<ILocationRequest> CapitalizeFirstLetter(ILocationRequest request)
        {
            var stringToArray = request.Shelf.Split(' ', ',', '.');
            string[] arrayToString = new string[stringToArray.Length];
            int count = 0;

            foreach (var word in stringToArray)
            {
                var capitalizedCategoryName = char.ToUpper(word[0]) + word.Substring(1).ToLower();
                arrayToString[count] = capitalizedCategoryName;
                count++;
            }
            request.Shelf = string.Join(" ", arrayToString);

            return new SuccessDataResult<ILocationRequest>(request);
        }

        private async Task<IResult> IsLocationNameUnique(string shelf)
        {
            var result = await _locationDal.GetAsync(f => f.Shelf.ToUpper() == shelf.ToUpper());

            if (result is not null)
            {
                return new ErrorResult(Messages.LocationNameNotUnique);
            }
            return new SuccessResult();
        }
        #endregion

    }
}
