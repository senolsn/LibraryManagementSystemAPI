using AutoMapper;
using Business.Dtos.Request.Language;
using Business.Dtos.Request.Location;
using Business.Dtos.Response.Language;
using Business.Dtos.Response.Location;
using Core.DataAccess.Paging;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class LocationMappingProfile:Profile
    {
        public LocationMappingProfile()
        {

            CreateMap<Location, CreateLocationRequest>().ReverseMap();

            CreateMap<Location, UpdateLocationRequest>().ReverseMap();

            CreateMap<IPaginate<Location>, Paginate<GetListLocationResponse>>().ReverseMap();
            CreateMap<Location, GetListLocationResponse>().ReverseMap();
        }
    }
}
