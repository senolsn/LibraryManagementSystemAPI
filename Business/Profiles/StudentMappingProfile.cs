using AutoMapper;
using Business.Dtos.Request.StudentRequests;
using Business.Dtos.Request.User;
using Business.Dtos.Response.StudentResponses;
using Business.Dtos.Response.User;
using Core.DataAccess.Paging;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<Student, CreateStudentRequest>().ReverseMap();

            CreateMap<Student, GetAllStudentsResponse>().ReverseMap();
        }
    }
}
