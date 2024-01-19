using AutoMapper;
using Business.Dtos.Request.User;
using Business.Dtos.Response.User;
using Core.DataAccess.Paging;
using Core.Entities.Concrete;

namespace Business.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, CreateUserRequest>().ReverseMap();

            CreateMap<User, UpdateUserRequest>().ReverseMap();

            CreateMap<IPaginate<User>, Paginate<GetListUserResponse>>().ReverseMap();
            CreateMap<User, GetListUserResponse>().ReverseMap();
        }

    }
}
