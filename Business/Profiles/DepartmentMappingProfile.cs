using AutoMapper;
using Business.Dtos.Request.Author;
using Business.Dtos.Request.Department;
using Business.Dtos.Response.Author;
using Business.Dtos.Response.Department;
using Core.DataAccess.Paging;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
