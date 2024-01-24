using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.User;
using Business.Dtos.Response.User;
using Core.Aspects.Autofac.Transaction;
using Core.DataAccess.Paging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class UserManager : IUserService
    {
        protected readonly IUserDal _userDal;
        protected readonly IDepositBookDal _depositBookDal;
        protected readonly IMapper _mapper;

        public UserManager(IUserDal userDal, IDepositBookDal depositBookDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
            _depositBookDal = depositBookDal;
        }

        public async Task<IResult> Add(User user)
        {
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
        public async Task<IDataResult<IPaginate<GetListUserResponse>>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _userDal.GetListAsync(
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
            if (_depositBookDal.GetAsync(d => d.UserId == userId) is null)
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

        public async Task<IDataResult<User>> GetAsyncByFacultyId(Guid facultyId)
        {
            var result = await _userDal.GetAsync(u => u.FacultyId == facultyId);

            if (result is not null)
            {
                return new SuccessDataResult<User>(result, Messages.UserListed);
            }

            return new ErrorDataResult<User>(Messages.Error);
        }
    }
}
