using AutoMapper;
using Business.Dtos.Request.Create;
using Business.Dtos.Response.Create;
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
        }
    }
}
