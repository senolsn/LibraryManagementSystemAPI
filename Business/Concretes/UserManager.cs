using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Business.Dtos.Request.User;
using Business.Dtos.Response.User;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess.Paging;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
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

		[SecuredOperation("admin,add")]
		[ValidationAspect(typeof(UserValidator))] 
		[CacheRemoveAspect("IUserService.Get")]
		public async Task<IResult> Add(User user)
        {
			var result = BusinessRules.Run(await IsEmailUnique(user.Email));
			if (result != null)
			{
				return result;
			}

			var createdUser = await _userDal.AddAsync(user);
			if (createdUser == null)
			{
				return new ErrorResult(Messages.Error);
			}
			return new SuccessResult(Messages.UserAdded);
		}

		[SecuredOperation("admin,update")]
		[ValidationAspect(typeof(UserValidator))]
		[CacheRemoveAspect("IUserService.Get")]
		public async Task<IResult> Update(UpdateUserRequest request)
        {
			var result = BusinessRules.Run(await IsEmailUnique(request.Email, request.UserId));
			if (result != null)
			{
				return result;
			}
			var userToUpdate = await _userDal.GetAsync(u => u.UserId == request.UserId);
			if (userToUpdate == null)
			{
				return new ErrorResult(Messages.UserNotFound);
			}
			if (!string.IsNullOrEmpty(request.Password))
			{
				HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
				userToUpdate.PasswordHash = passwordHash;
				userToUpdate.PasswordSalt = passwordSalt;
			}
			_mapper.Map(request, userToUpdate);
			await _userDal.UpdateAsync(userToUpdate);
			return new SuccessResult(Messages.UserUpdated);
		}



		[SecuredOperation("admin,delete")]
		[ValidationAspect(typeof(UserValidator))] 
        [CacheRemoveAspect("IUserService.Get")]
		public async Task<IResult> Delete(DeleteUserRequest request)
		{
			var userToDelete = await _userDal.GetAsync(u => u.UserId == request.UserId);
			if (userToDelete != null)
			{
				if (await CheckIfExistInDepositBook(request.UserId))
				{
					return new ErrorResult(Messages.UserExistInDepositBooks);
				}

				await _userDal.DeleteAsync(userToDelete);
				return new SuccessResult(Messages.UserDeleted);
			}
			return new ErrorResult(Messages.UserNotFound);
		}



		[SecuredOperation("admin,get")]
		[CacheAspect] 
		public async Task<IDataResult<User>> GetAsync(Guid userId)
		{
			var result = await _userDal.GetAsync(u => u.UserId == userId);

			if (result != null)
			{
				return new SuccessDataResult<User>(result, Messages.UserListed);
			}

			return new ErrorDataResult<User>(Messages.UserNotFound); 
		}



		[SecuredOperation("admin,get")]
		[CacheAspect]
		public async Task<IDataResult<IPaginate<GetListUserResponse>>> GetListAsync(PageRequest pageRequest)
        {
			var data = await _userDal.GetListAsync(
			null,
			index: pageRequest.PageIndex,
			size: pageRequest.PageSize,
			true);

			if (data != null && data.Items.Any())
			{
				var result = _mapper.Map<Paginate<GetListUserResponse>>(data);

				return new SuccessDataResult<IPaginate<GetListUserResponse>>(result, Messages.UsersListed);
			}

			return new ErrorDataResult<IPaginate<GetListUserResponse>>(Messages.UsersNotFound);
		}


		[SecuredOperation("admin,get")]
		[CacheAspect]
		public async Task<IDataResult<User>> GetByMail(string mail)
        {

			var user = await _userDal.GetAsync(u => u.Email == mail);
            if (user != null)
            {
				return new SuccessDataResult<User>(user, Messages.UserFetched);
            }

			return new ErrorDataResult<User>(Messages.UserNotFound);

		}

		[SecuredOperation("admin,get")]
		[CacheAspect]
		public List<OperationClaim> GetClaims(User user)
        {
            var result = _userDal.GetClaims(user);
            return result; 
        }

		[SecuredOperation("admin,get")]
		[CacheAspect]
		public async Task<IDataResult<User>> GetAsyncByFacultyId(Guid facultyId)
        {
			var result = await _userDal.GetAsync(u=>u.FacultyId==facultyId);
			if (result != null)
			{
				return new SuccessDataResult<User>(result, Messages.UserListed);
			}
				
			return new ErrorDataResult<User>(Messages.UserNotFound);
		}

		#region Helpers
		private async Task<IResult> IsEmailUnique(string email, Guid? userId = null)
		{
			var existingUser = await _userDal.GetAsync(u => u.Email == email && (userId == null || u.UserId != userId));
			if (existingUser != null)
			{
				return new ErrorResult(Messages.UserEmailNotUnique);
			}
			return new SuccessResult();
		}
		private async Task<bool> CheckIfExistInDepositBook(Guid userId)
		{
			var depositBookEntry = await _depositBookDal.GetAsync(d=>d.UserId==userId);
			return depositBookEntry != null;
		}

		#endregion
	}
}
