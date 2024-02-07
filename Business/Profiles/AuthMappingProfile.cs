using AutoMapper;
using Business.Dtos.Request.Auth;
using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class AuthMappingProfile:Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<CreateRegisterRequest, User>().ReverseMap();
        }
    }
}
