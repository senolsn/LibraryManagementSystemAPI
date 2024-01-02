using AutoMapper;
using Business.Dtos.Request.Category;
using Business.Dtos.Request.Faculty;
using Business.Dtos.Response.Category;
using Business.Dtos.Response.Faculty;
using Core.DataAccess.Paging;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CreateCategoryRequest>().ReverseMap();

            CreateMap<Category, UpdateCategoryRequest>().ReverseMap();

            CreateMap<IPaginate<Category>, Paginate<GetListCategoryResponse>>().ReverseMap();
            CreateMap<Category, GetListCategoryResponse>().ReverseMap();
        }

    }
}
