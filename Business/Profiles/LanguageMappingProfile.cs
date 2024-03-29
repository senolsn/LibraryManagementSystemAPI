﻿using AutoMapper;
using Business.Dtos.Request.LanguageRequests;
using Business.Dtos.Response.LanguageResponses;
using Core.DataAccess.Paging;
using Entities.Concrete;

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
