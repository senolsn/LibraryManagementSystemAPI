using AutoMapper;
using Business.Dtos.Request.Create;
using Business.Dtos.Request.Update;
using Business.Dtos.Response.Create;
using Business.Dtos.Response.Read;
using Business.Dtos.Response.Update;
using Core.DataAccess.Paging;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class LanguageMappingProfile:Profile
    {
        public LanguageMappingProfile() 
        {
            CreateMap<Language,CreateLanguageRequest>().ReverseMap();
            CreateMap<Language,CreatedLanguageResponse>().ReverseMap();

            CreateMap<Language, UpdateLanguageRequest>().ReverseMap();
            CreateMap<Language,UpdatedLanguageResponse>().ReverseMap();

            CreateMap<IPaginate<Language>, Paginate<GetListLanguageResponse>>().ReverseMap();
            CreateMap<Language,GetListLanguageResponse>().ReverseMap();
        }
    }
}
