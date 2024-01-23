using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Faculty;
using Business.Dtos.Response.Faculty;
using Core.Aspects.Autofac.Caching;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class FacultyManager : IFacultyService
    {
        protected readonly IFacultyDal _facultyDal;
        protected readonly IUserDal _userDal;
        protected readonly IMapper _mapper;
        public FacultyManager(IFacultyDal facultyDal, IUserDal userDal, IMapper mapper)
        {
            _facultyDal = facultyDal;
            _userDal = userDal;
            _mapper = mapper;    
        }
        public async Task<IResult> Add(CreateFacultyRequest request)
        {
            Faculty faculty = _mapper.Map<Faculty>(request);
            var createdFaculty = await _facultyDal.AddAsync(faculty);
            if(createdFaculty is null)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.FacultyAdded);
        }

        public async Task<IResult> Delete(DeleteFacultyRequest request)
        {
            var facultyToDelete = await _facultyDal.GetAsync(f => f.FacultyId == request.FacultyId);

            if (facultyToDelete is not null)
            {
                if (CheckIfExistInUsers(request.FacultyId))
                {
                    return new ErrorResult(Messages.FacultyExistInUsers);
                }
                await _facultyDal.DeleteAsync(facultyToDelete);
                return new SuccessResult(Messages.FacultyDeleted);
            }

            return new ErrorResult(Messages.Error);
        }
        [CacheAspect()]
        public async Task<IDataResult<Faculty>> GetAsync(Guid facultyId)
        {
            var result = await _facultyDal.GetAsync(f => f.FacultyId == facultyId);

            if (result is not null)
            {
                return new SuccessDataResult<Faculty>(result, Messages.FacultyListed);
            }

            return new ErrorDataResult<Faculty>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<GetListFacultyResponse>>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _facultyDal.GetListAsync(
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

        public async Task<IResult> Update(UpdateFacultyRequest request)
        {
            var facultyToUpdate = await _facultyDal.GetAsync(f => f.FacultyId == request.FacultyId);

            if (facultyToUpdate is null)
            {
                return new ErrorResult(Messages.Error);
            }

            _mapper.Map(request, facultyToUpdate);

            await _facultyDal.UpdateAsync(facultyToUpdate);

            return new SuccessResult(Messages.FacultyUpdated);
        }

        private bool CheckIfExistInUsers(Guid facultyId)
        {
            if(_userDal.GetAsync(u => u.FacultyId == facultyId) is null)
            {
                return false;
            }
            return true;
        }
    }
}
