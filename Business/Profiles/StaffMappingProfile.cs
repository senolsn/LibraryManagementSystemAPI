using AutoMapper;
using Business.Dtos.Request.StaffRequests;
using Business.Dtos.Response.StaffResponses;
using Entities.Concrete;

namespace Business.Profiles
{
    public class StaffMappingProfile : Profile
    {
        public StaffMappingProfile()
        {
            CreateMap<Staff, CreateStaffRequest>().ReverseMap();

            CreateMap<Staff, GetAllStaffResponse>().ReverseMap();
        }

    }
}
