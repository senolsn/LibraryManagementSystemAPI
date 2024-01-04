using AutoMapper;
using Business.Dtos.Request.Publisher;
using Business.Dtos.Response.Publisher;
using Core.DataAccess.Paging;
using Entities.Concrete;

namespace Business.Profiles
{
    public class PublisherMappingProfile : Profile
    {
        public PublisherMappingProfile()
        {
            CreateMap<Publisher, CreatePublisherRequest>().ReverseMap();

            CreateMap<Publisher, UpdatePublisherRequest>().ReverseMap();

            CreateMap<IPaginate<Publisher>, Paginate<GetListPublisherResponse>>().ReverseMap();
            CreateMap<Publisher, GetListPublisherResponse>().ReverseMap();
        }
    }
}
