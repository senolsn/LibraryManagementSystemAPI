using AutoMapper;
using Business.Dtos.Request.DepositBook;
using Business.Dtos.Request.FacultyResponses;
using Business.Dtos.Response.DepositBook;
<<<<<<< HEAD
using Business.Dtos.Response.FacultyResponses;
=======
>>>>>>> 5c43c7567816add2417b815efb5faed65d391e24
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
