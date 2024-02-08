using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Auth;
using Business.Dtos.Request.UserRequests;
using Business.Dtos.Response.Publisher;
using Business.Dtos.Response.UserResponses;
using Core.Aspects.Autofac.Transaction;
using Core.DataAccess.Paging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class UserManager : IUserService
    {
        protected readonly IUserDal _userDal;
        protected readonly Lazy<IDepositBookDal> _depositBookDal;
        private readonly Lazy<IDepartmentService> _departmentService;
        private readonly Lazy<IFacultyService> _facultyService;
        protected readonly IMapper _mapper;

        public UserManager(IUserDal userDal, Lazy<IDepositBookDal> depositBookDal, IMapper mapper, Lazy<IDepartmentService> departmentService, Lazy<IFacultyService> facultyService)
        {
            _userDal = userDal;
            _mapper = mapper;
            _depositBookDal = depositBookDal;
            _departmentService = departmentService;
            _facultyService = facultyService;
        }

        public async Task<IResult> Add(User user, CreateRegisterRequest request)
        {
            user.UserDepartments = new List<Department>();
            user.Faculty = new Faculty();

            foreach (var departmentId in request.DepartmentIds)
            {
                var result = await _departmentService.Value.GetAsync(departmentId);
                ArgumentNullException.ThrowIfNull(result.Data, "Department");
                user.UserDepartments.Add(result.Data);
            }

            var facultyResult = await _facultyService.Value.GetAsync(request.FacultyId);
            user.Faculty = facultyResult.Data;

            var createdUser = await _userDal.AddAsync(user);

            if (createdUser is null)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.UserAdded);
        }

        public async Task<IResult> Update(UpdateUserRequest request)
        {
            HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User() { PasswordHash = passwordHash, PasswordSalt = passwordSalt };

            var mappedUser = _mapper.Map(request, user);

            if (mappedUser is null)
            {
                return new ErrorResult(Messages.Error);
            }

            await _userDal.UpdateAsync(mappedUser);

            return new SuccessResult(Messages.UserUpdated);
        }
        public async Task<IResult> Delete(DeleteUserRequest request)
        {
            var userToDelete = await _userDal.GetAsync(u => u.UserId == request.UserId);

            if (userToDelete is not null)
            {
                if (checkIfExistInDepositBook(request.UserId))
                {
                    return new ErrorResult(Messages.UserExistInDepositBooks);
                }
                await _userDal.DeleteAsync(userToDelete);
                return new SuccessResult(Messages.UserDeleted);
            }

            return new ErrorResult(Messages.Error);
        }
        public async Task<IDataResult<User>> GetAsync(Guid userId)
        {
            var result = await _userDal.GetAsync(u => u.UserId == userId);

            if (result is not null)
            {
                return new SuccessDataResult<User>(result, Messages.UserListed);
            }

            return new ErrorDataResult<User>(Messages.Error);
        }
        public async Task<IDataResult<IPaginate<GetListUserResponse>>> GetPaginatedListAsync(PageRequest pageRequest)
        {
            var data = await _userDal.GetPaginatedListAsync(
                null,
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize,
                true);

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListUserResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListUserResponse>>(result, Messages.UsersListed);
            }

            return new ErrorDataResult<IPaginate<GetListUserResponse>>(Messages.Error);
        }
        public async Task<User> GetByMail(string mail)
        {
            return await _userDal.GetAsync(u => u.Email == mail);
        }
        private bool checkIfExistInDepositBook(Guid userId)
        {
            if (_depositBookDal.Value.GetAsync(d => d.UserId == userId) is null)
            {
                return false;
            }
            return true;
        }
        public List<OperationClaim> GetClaims(User user)
        {
            var result = _userDal.GetClaims(user);
            return result; 
        }

		[TransactionScopeAspect]
		public Task<IResult> AddTransactionalTest(User user)
		{
			throw new NotImplementedException();
		}

        public async Task<IDataResult<List<GetListUserResponse>>> GetListAsync()
        {
            var data = await _userDal.GetListAsync();

            if (data is not null)
            {
                var usersResponse = _mapper.Map<List<GetListUserResponse>>(data);

                return new SuccessDataResult<List<GetListUserResponse>>(usersResponse, Messages.UsersListed);
            }

            return new ErrorDataResult<List<GetListUserResponse>>(Messages.Error);
        }

        //public async Task<IDataResult<User>> GetAsyncByFacultyId(Guid facultyId)
        //{
        //    var result = await _userDal.GetAsync(u => u.FacultyId == facultyId);

        //    if (result is not null)
        //    {
        //        return new SuccessDataResult<User>(result, Messages.UserListed);
        //    }

        //    return new ErrorDataResult<User>(Messages.Error);
        //}

        //public async Task<IDataResult<User>> GetAsyncByDepartmentId(Guid departmentId)
        //{
        //    var result = await _userDal.GetAsync(u => u.DepartmentId == departmentId);

        //    if (result is not null)
        //    {
        //        return new SuccessDataResult<User>(result, Messages.UserListed);
        //    }

        //    return new ErrorDataResult<User>(Messages.Error);
        //}
    }
}
