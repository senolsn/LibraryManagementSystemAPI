using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.User;
using Business.Dtos.Response.Language;
using Business.Dtos.Response.User;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class UserManager : IUserService
    {
        protected readonly IUserDal _userDal;
        protected readonly IMapper _mapper;

        public UserManager(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CreateUserRequest request)
        {
            User user = _mapper.Map<User>(request);

            var createdUser = await _userDal.AddAsync(user);

            if (createdUser is null)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.UserAdded);
        }

        public async Task<IResult> Delete(DeleteUserRequest request)
        {
            var userToDelete = await _userDal.GetAsync(u => u.UserId == request.UserId);

            if (userToDelete is not null)
            {
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

        public async Task<IResult> Update(UpdateUserRequest request)
        {
            var userToUpdate = await _userDal.GetAsync(u => u.UserId == request.UserId);

            if (userToUpdate is null)
            {
                return new ErrorResult(Messages.Error);
            }

            _mapper.Map(request, userToUpdate);

            await _userDal.UpdateAsync(userToUpdate);

            return new SuccessResult(Messages.UserUpdated);
        }
    }
}
