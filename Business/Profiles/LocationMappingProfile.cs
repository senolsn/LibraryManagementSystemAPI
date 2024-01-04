using AutoMapper;
using Business.Dtos.Request.Location;
using Business.Dtos.Response.Location;
using Core.DataAccess.Paging;
using Entities.Concrete;

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
