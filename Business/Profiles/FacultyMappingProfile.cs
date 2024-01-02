using AutoMapper;
using Business.Dtos.Request.Faculty;
using Business.Dtos.Response.Faculty;
using Core.DataAccess.Paging;
using Entities.Concrete;

namespace Business.Profiles
{
    public class FacultyMappingProfile : Profile
    {
        public FacultyMappingProfile()
        {
            CreateMap<Faculty, CreateFacultyRequest>().ReverseMap();

            CreateMap<Faculty, UpdateFacultyRequest>().ReverseMap();

            CreateMap<IPaginate<Faculty>, Paginate<GetListFacultyResponse>>().ReverseMap();
            CreateMap<Faculty, GetListFacultyResponse>().ReverseMap();
        }

    }
}
