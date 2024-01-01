using AutoMapper;
using Business.Dtos.Request.Author;
using Business.Dtos.Response.Author;
using Core.DataAccess.Paging;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class AuthorMappingProfile:Profile
    {
      public AuthorMappingProfile()
        {
            CreateMap<Author,CreateAuthorRequest>().ReverseMap();
            CreateMap<Author,CreatedAuthorResponse>().ReverseMap();

            CreateMap<Author,UpdateAuthorRequest>().ReverseMap();
            CreateMap<Author,UpdatedAuthorResponse>().ReverseMap();

            CreateMap<IPaginate<Author>, Paginate<GetListAuthorResponse>>().ReverseMap();
            CreateMap<Author,GetListAuthorResponse>().ReverseMap();
        }
    }
}
