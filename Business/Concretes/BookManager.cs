using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Book;
using Business.Dtos.Response.Author;
using Business.Dtos.Response.Book;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class BookManager : IBookService
    {
        protected readonly IBookDal _bookDal;
        protected readonly IMapper _mapper;

        public BookManager(IBookDal bookDal, IMapper mapper)
        {
            _bookDal = bookDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CreateBookRequest request)
        {
            Book book = _mapper.Map<Book>(request);

            var createdBook = await _bookDal.AddAsync(book);

            if (createdBook is null)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.AuthorAdded);
        }

        public async Task<IResult> Update(UpdateBookRequest request)
        {
            var bookToUpdate = await _bookDal.GetAsync(b => b.BookId == request.BookId);

            if (bookToUpdate is null)
            {
                return new ErrorResult(Messages.Error);
            }

            _mapper.Map(request, bookToUpdate);

            await _bookDal.UpdateAsync(bookToUpdate);

            return new SuccessResult(Messages.AuthorUpdated);
        }

        public async Task<IResult> Delete(DeleteBookRequest request)
        {
            var bookToDelete = await _bookDal.GetAsync(b => b.BookId == request.BookId);

            if (bookToDelete is not null)
            {
                await _bookDal.DeleteAsync(bookToDelete);
                return new SuccessResult(Messages.AuthorDeleted);
            }

            return new ErrorResult(Messages.Error);
        }

        public async Task<IDataResult<Book>> GetAsync(Guid bookId)
        {
            var result = await _bookDal.GetAsync(b => b.BookId == bookId);

            if (result is not null)
            {
                return new SuccessDataResult<Book>(result, Messages.AuthorListed);
            }

            return new ErrorDataResult<Book>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<GetListBookResponse>>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _bookDal.GetListAsync(
               null,
               index: pageRequest.PageIndex,
               size: pageRequest.PageSize);

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListBookResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListBookResponse>>(result, Messages.AuthorsListed);
            }

            return new ErrorDataResult<IPaginate<GetListBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<GetListBookResponse>>> GetListAsyncByCategory(PageRequest pageRequest, Guid categoryId)
        {
            var data = await _bookDal.GetListAsync(
                predicate: b => b.CategoryId == categoryId,
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
                );

            if(data is not null)
            {
                var result = _mapper.Map<Paginate<GetListBookResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListBookResponse>>(result, Messages.AuthorsListed); 
            }

            return new ErrorDataResult<IPaginate<GetListBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<GetListBookResponse>>> GetListAsyncSortedByName(PageRequest pageRequest)
        {
            var data = await _bookDal.GetListAsyncSortedByName(
                predicate: null,
                orderBy : q => q.OrderBy(b => b.BookName),
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
                );

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListBookResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListBookResponse>>(result, Messages.AuthorsListed);
            }

            return new ErrorDataResult<IPaginate<GetListBookResponse>>(Messages.Error);
        }
    }
}
