using AutoMapper;
using Business.Dtos.Request.Book;
using Business.Dtos.Request.Book;
using Business.Dtos.Response.Book;
using Core.DataAccess.Paging;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class BookMapingProfile:Profile
    {
        public BookMapingProfile()
        {
            CreateMap<Book, CreateBookRequest>().ReverseMap();

            CreateMap<Book, UpdateBookRequest>().ReverseMap();

            CreateMap<IPaginate<Book>, Paginate<GetListBookResponse>>().ReverseMap();
            CreateMap<Book, GetListBookResponse>().ReverseMap();
        }
    }
}
