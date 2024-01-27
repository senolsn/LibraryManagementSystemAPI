using AutoMapper;
using Business.Dtos.Request.BookRequests;
using Business.Dtos.Response.Book;
using Core.DataAccess.Paging;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Business.Profiles
{
    public class BookMappingProfile:Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, CreateBookRequest>().ReverseMap();

            CreateMap<Book, UpdateBookRequest>().ReverseMap();

            CreateMap<IPaginate<Book>, Paginate<GetListBookResponse>>().ReverseMap();
            CreateMap<Book, GetListBookResponse>().ReverseMap();
        }
    }
}
