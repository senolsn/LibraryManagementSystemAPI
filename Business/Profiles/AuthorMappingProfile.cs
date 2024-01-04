using AutoMapper;
using Business.Dtos.Request.Author;
using Business.Dtos.Response.Author;
using Core.DataAccess.Paging;
using Entities.Concrete;

namespace Business.Profiles
{
    public class AuthorMappingProfile:Profile
    {
      public AuthorMappingProfile()
        {
            CreateMap<Author,CreateAuthorRequest>().ReverseMap();

            CreateMap<Author,UpdateAuthorRequest>().ReverseMap();

            CreateMap<IPaginate<Author>, Paginate<GetListAuthorResponse>>().ReverseMap();
            CreateMap<Author,GetListAuthorResponse>().ReverseMap();
        }
    }
}
