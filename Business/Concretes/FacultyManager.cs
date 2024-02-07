using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
<<<<<<< HEAD
using Business.Dtos.Request.Category;
using Business.Dtos.Request.FacultyResponses;
using Business.Dtos.Response.Department;
using Business.Dtos.Response.FacultyResponses;
using Business.ValidationRules.FluentValidation.FacultyValidator;
=======
using Business.Dtos.Request.FacultyResponses;
using Business.Dtos.Response.FacultyResponses;
using Business.ValidationRules.FluentValidation;
>>>>>>> 5c43c7567816add2417b815efb5faed65d391e24
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess.Paging;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class FacultyManager : IFacultyService
    {
        protected readonly IFacultyDal _facultyDal;
        protected readonly IUserService _userService;
        protected readonly IMapper _mapper;
        public FacultyManager(IFacultyDal facultyDal, IUserService userService, IMapper mapper)
        {
            _facultyDal = facultyDal;
            _userService = userService;
            _mapper = mapper;    
        }

        //[SecuredOperation("admin,add")]
        [ValidationAspect(typeof(FacultyValidator))]
        [CacheRemoveAspect("IFacultyService.Get")]
        public async Task<IResult> Add(CreateFacultyRequest request)
        {
            var result = BusinessRules.Run(CapitalizeFirstLetter(request), await IsFacultyNameUnique(request.FacultyName));
            
            if (result is not null)
            {
                return result;
            }

            Faculty faculty = _mapper.Map<Faculty>(request);
            var createdFaculty = await _facultyDal.AddAsync(faculty);
            if(createdFaculty is null)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.FacultyAdded);
        }

        //[SecuredOperation("admin,update")]
        [ValidationAspect(typeof(FacultyValidator))]
        [CacheRemoveAspect("IFacultyService.Get")]
        public async Task<IResult> Update(UpdateFacultyRequest request)
        {
            var result = BusinessRules.Run(CapitalizeFirstLetter(request), await IsFacultyNameUnique(request.FacultyName));

            if (result is not null)
            {
                return result;
            }

            var facultyToUpdate = await _facultyDal.GetAsync(f => f.FacultyId == request.FacultyId);

            if (facultyToUpdate is null)
            {
                return new ErrorResult(Messages.Error);
            }

            _mapper.Map(request, facultyToUpdate);

            await _facultyDal.UpdateAsync(facultyToUpdate);

            return new SuccessResult(Messages.FacultyUpdated);
        }

        //[SecuredOperation("admin,delete")]
        [CacheRemoveAspect("IFacultyService.Get")]
        public async Task<IResult> Delete(DeleteFacultyRequest request)
        {

            var facultyToDelete = await _facultyDal.GetAsync(f => f.FacultyId == request.FacultyId);

            if (facultyToDelete is not null)
            {
                var result = BusinessRules.Run(await CheckIfExistInUsers(request.FacultyId));

                if (result is not null)
                {
                    return result;
                }

                await _facultyDal.DeleteAsync(facultyToDelete);
                return new SuccessResult(Messages.FacultyDeleted);
            }

            return new ErrorResult(Messages.Error);
        }

        [SecuredOperation("admin,get")]
        public async Task<IDataResult<Faculty>> GetAsync(Guid facultyId)
        {
            var result = await _facultyDal.GetAsync(f => f.FacultyId == facultyId);

            if (result is not null)
            {
                return new SuccessDataResult<Faculty>(result, Messages.FacultyListed);
            }

            return new ErrorDataResult<Faculty>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListFacultyResponse>>> GetListAsync()
        {
            var data = await _facultyDal.GetListAsync(null);

            if (data is not null)
            {
                var facultiesResponse = _mapper.Map<List<GetListFacultyResponse>>(data);

                return new SuccessDataResult<List<GetListFacultyResponse>>(facultiesResponse, Messages.FacultiesListed);
            }

            return new ErrorDataResult<List<GetListFacultyResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListFacultyResponse>>> GetListAsyncSortedByName()
        {
            var data = await _facultyDal.GetListAsyncOrderBy(null, orderBy: q => q.OrderBy(f => f.FacultyName));

            if (data is not null)
            {
                var facultiesResponse = _mapper.Map<List<GetListFacultyResponse>>(data);

                return new SuccessDataResult<List<GetListFacultyResponse>>(facultiesResponse, Messages.FacultiesListed);
            }

            return new ErrorDataResult<List<GetListFacultyResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListFacultyResponse>>> GetListAsyncSortedByCreatedDate()
        {
            var data = await _facultyDal.GetListAsyncOrderBy(null, orderBy: q => q.OrderByDescending(f => f.CreatedDate));

            if (data is not null)
            {
                var departmentsResponse = _mapper.Map<List<GetListFacultyResponse>>(data);

                return new SuccessDataResult<List<GetListFacultyResponse>>(departmentsResponse, Messages.FacultiesListed);
            }

            return new ErrorDataResult<List<GetListFacultyResponse>>(Messages.Error);
        }

        //[SecuredOperation("admin,get")]
        [CacheAspect]
        public async Task<IDataResult<IPaginate<GetListFacultyResponse>>> GetPaginatedListAsync(PageRequest pageRequest)
        {
            var data = await _facultyDal.GetPaginatedListAsync(
               null,
               index: pageRequest.PageIndex,
               size: pageRequest.PageSize,
               true
               );

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListFacultyResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListFacultyResponse>>(result, Messages.FacultiesListed);
            }

            return new ErrorDataResult<IPaginate<GetListFacultyResponse>>(Messages.Error);
        }

        #region Helper Methods
        private async Task<IResult> CheckIfExistInUsers(Guid facultyId)
        {
            //var result = await _userService.GetAsyncByFacultyId(facultyId);
            //if (result.IsSuccess)
            //{
            //    return new ErrorResult(Messages.FacultyExistInUsers);
            //}
            //    return new SuccessResult();
            return new SuccessResult();
        }

        private IDataResult<IFacultyRequest> CapitalizeFirstLetter(IFacultyRequest request)
        {
            var stringToArray = request.FacultyName.Split(' ', ',', '.');
            string[] arrayToString = new string[stringToArray.Length];
            int count = 0;

            foreach (var word in stringToArray)
            {
                var capitalizedCategoryName = char.ToUpper(word[0]) + word.Substring(1).ToLower();
                arrayToString[count] = capitalizedCategoryName;
                count++;
            }
            request.FacultyName = string.Join(" ", arrayToString);

            return new SuccessDataResult<IFacultyRequest>(request);
        }

        private async Task<IResult> IsFacultyNameUnique(string facultyName)
        {
            var result = await _facultyDal.GetAsync(f => f.FacultyName.ToUpper() == facultyName.ToUpper());

            if (result is not null)
            {
                return new ErrorResult(Messages.FacultyNameNotUnique);
            }
            return new SuccessResult();
        }
        #endregion
    }
}
