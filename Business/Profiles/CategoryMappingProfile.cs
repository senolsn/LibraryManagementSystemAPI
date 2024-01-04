using AutoMapper;
using Business.Dtos.Request.Category;
using Business.Dtos.Response.Category;
using Core.DataAccess.Paging;
using Entities.Concrete;

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
