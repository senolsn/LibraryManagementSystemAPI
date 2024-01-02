using AutoMapper;
using Business.Dtos.Request.Language;
using Business.Dtos.Request.Publisher;
using Business.Dtos.Response.Language;
using Business.Dtos.Response.Publisher;
using Core.DataAccess.Paging;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
