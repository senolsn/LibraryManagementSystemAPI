using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Auth;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstracts;
using Entities.Concrete;
using Entities.Concrete.enums;
using System;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IStaffDal _staffDal;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, IMapper mapper, ITokenHelper tokenHelper, IStaffDal staffDal)
        {
            _userService = userService;
            _mapper = mapper;
            _tokenHelper = tokenHelper;
            _staffDal = staffDal;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken,Messages.AccessTokenCreated);
        }

        public async Task<IDataResult<User>> Login(CreateLoginRequest request)
        {
            var userToCheck = await _userService.GetByMail(request.Email);
            
            if(userToCheck is null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if(!HashingHelper.VerifyPasswordHash(request.Password, userToCheck.PasswordHash,userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.WrongMailOrPassword);
            }

            return new SuccessDataResult<User>(userToCheck,Messages.Loginned);
        }

        public async Task<IDataResult<User>> Register(CreateRegisterRequest request)
        {
            var userExists = await CheckUserExists(request.Email);

            if(!userExists) 
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                var user = new User { PasswordHash = passwordHash, PasswordSalt = passwordSalt };

                
                
                var mappedUser = _mapper.Map(request, user);
                await _userService.Add(mappedUser);


                /*
                 1- Register page'de staff ya da student farketmeksizin
                 veriler dolduruldu. User tablosuna eklendi.
                 
                 2- Staff ise staffManager, student ise studentManager'ı
                 çağırarak aşağıdaki map'leme işlemini front'ta yapıp 
                 ilgili manager'ın add metoduna vereceğiz.
                 
                 *** If yapısı hoşuma gitmedi. Interface'lerle soyutla.
                 */

                if(request.UserType == UserType.STAFF)
                {
                    Staff staff = new Staff();
                    staff.FirstName = user.FirstName;
                    staff.LastName = user.LastName;
                    staff.Email = user.Email;
                    staff.FacultyId = Guid.Parse("08dc275e-f321-4c4d-86b8-da3e578c6858");
                    staff.PasswordHash = passwordHash;
                    staff.PasswordSalt = passwordSalt;
                    staff.UserId = user.UserId;
                    staff.PhoneNumber = user.PhoneNumber;
                    staff.User = user;
                    await _staffDal.AddAsync(staff);
                }
                else if(request.UserType == UserType.STUDENT)
                {

                }           

                return new SuccessDataResult<User>(user);
            }

            return new ErrorDataResult<User>();
        }

        private async Task<bool> CheckUserExists(string mail)
        {
            var result = await _userService.GetByMail(mail);

            if (result is not null)
            {
                return true;
            }
            return false;
        }
    }
}
