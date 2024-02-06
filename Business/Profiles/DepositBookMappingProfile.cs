using AutoMapper;
using Business.Dtos.Request.DepositBook;
using Business.Dtos.Request.FacultyResponses;
using Business.Dtos.Response.DepositBook;
using Core.DataAccess.Paging;
using Entities.Concrete;

namespace Business.Profiles
{
    public class DepositBookMappingProfile : Profile
    {
        public DepositBookMappingProfile()
        {
            CreateMap<DepositBook, CreateDepositBookRequest>().ReverseMap();

            CreateMap<DepositBook, UpdateDepositBookRequest>().ReverseMap();

            CreateMap<IPaginate<DepositBook>, Paginate<GetListDepositBookResponse>>().ReverseMap();
            CreateMap<DepositBook, GetListDepositBookResponse>().ReverseMap();
        }
    }
}
