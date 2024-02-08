using AutoMapper;
using Business.Dtos.Request.SettingRequests;
using Business.Dtos.Response.SettingResponses;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class SettingMappingProfile:Profile
    {
        public SettingMappingProfile()
        {
            CreateMap<Setting,CreateSettingRequest>().ReverseMap();
            CreateMap<Setting,GetSettingResponse>().ReverseMap();
            CreateMap<Setting,UpdateSettingRequest>().ReverseMap();
            CreateMap<Setting,DeleteSettingRequest>().ReverseMap();

        }
    }
}
