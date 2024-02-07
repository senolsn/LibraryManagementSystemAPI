using AutoMapper;
using Business.Dtos.Request.DepartmentRequests;
using Business.Dtos.Response.DepartmentResponses;
using Core.DataAccess.Paging;
using Entities.Concrete;

namespace Business.Profiles
{
    public class DepartmentMappingProfile:Profile
    {
        public DepartmentMappingProfile()
        {

            CreateMap<Department, CreateDepartmentRequest>().ReverseMap();

            CreateMap<Department, UpdateDepartmentRequest>().ReverseMap();

            CreateMap<IPaginate<Department>, Paginate<GetListDepartmentResponse>>().ReverseMap();
            CreateMap<Department, GetListDepartmentResponse>().ReverseMap();
        }
    }
}
