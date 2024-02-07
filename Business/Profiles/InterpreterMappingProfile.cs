using AutoMapper;
using Business.Dtos.Request.InterpreterRequests;
using Business.Dtos.Response.InterpreterResponses;
using Core.DataAccess.Paging;
using Entities.Concrete;

namespace Business.Profiles
{
    public class InterpreterMappingProfile : Profile
    {
        public InterpreterMappingProfile()
        {
            CreateMap<Interpreter, CreateInterpreterRequest>().ReverseMap();

            CreateMap<Interpreter, UpdateInterpreterRequest>().ReverseMap();

            CreateMap<IPaginate<Interpreter>, Paginate<GetListInterpreterResponse>>().ReverseMap();

            CreateMap<Interpreter, GetListInterpreterResponse>().ReverseMap();
        }
    }
}
