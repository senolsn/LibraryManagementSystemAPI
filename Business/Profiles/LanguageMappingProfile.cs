using AutoMapper;
using Business.Dtos.Request.Language;
using Business.Dtos.Response.Language;
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

            CreateMap<Language, UpdateLanguageRequest>().ReverseMap();

            CreateMap<IPaginate<Language>, Paginate<GetListLanguageResponse>>().ReverseMap();
            CreateMap<Language,GetListLanguageResponse>().ReverseMap();
        }
    }
}
