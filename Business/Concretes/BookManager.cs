using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Business.Dtos.Request.BookRequests;
using Business.Dtos.Response.Book;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class BookManager : IBookService
    {
        protected readonly IBookDal _bookDal;
        protected readonly IMapper _mapper;
        private readonly IAuthorService _authorService;

        public BookManager(IBookDal bookDal, IMapper mapper, IAuthorService authorService)
        {
            _bookDal = bookDal;
            _mapper = mapper;
            _authorService = authorService;
        }

        [SecuredOperation("admin,add")]
        [ValidationAspect(typeof (BookValidator))]
        [CacheRemoveAspect("IBookService.Get")]
        public async Task<IResult> Add(CreateBookRequest request)
        {

            Book book = _mapper.Map<Book>(request);

            book.BookAuthors = new List<Author>();

            foreach (var authorId in request.Authors)
            {
                var result = await _authorService.GetAsync(authorId);
                ArgumentNullException.ThrowIfNull(result.Data , "Yazar");
                book.BookAuthors.Add(result.Data);
            }



            var createdBook = await _bookDal.AddAsync(book);

            //var dbResult = await _bookDal.SaveChangesAsync();

            if (createdBook is null)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.BookListed);
        }

        [SecuredOperation("admin,update")]
        [ValidationAspect(typeof (BookValidator))]
        [CacheRemoveAspect("IBookService.Get")]
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

        [SecuredOperation("admin,delete")]
        [ValidationAspect(typeof (BookValidator))]
        [CacheRemoveAspect("IBookService.Get")]
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

        [SecuredOperation("admin,get")]
        public async Task<IDataResult<Book>> GetAsync(Guid bookId)
        {
            var result = await _bookDal.GetAsync(b => b.BookId == bookId);

            if (result is not null)
            {
                return new SuccessDataResult<Book>(result, Messages.BookListed);
            }

            return new ErrorDataResult<Book>(Messages.Error);
        }

        [SecuredOperation("admin,get")]
        public async Task<IDataResult<Book>> GetAsyncByCategoryId(Guid categoryId)
        {
            var result = await _bookDal.GetAsync(b => b.CategoryId == categoryId);

            if (result is not null)
            {
                return new SuccessDataResult<Book>(result, Messages.BookListed);
            }
            return new ErrorDataResult<Book>(Messages.Error);
        }

        [SecuredOperation("admin,get")]
        public async Task<IDataResult<Book>> GetAsyncByLanguageId(Guid languageId)
        {
            var result = await _bookDal.GetAsync(b => b.LanguageId == languageId);

            if (result is not null)
            {
                return new SuccessDataResult<Book>(result, Messages.BookListed);
            }
            return new ErrorDataResult<Book>(Messages.Error);
        }

        [SecuredOperation("admin,get")]
        public async Task<IDataResult<IPaginate<GetListBookResponse>>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _bookDal.GetListAsync(
               null,
               index: pageRequest.PageIndex,
               size: pageRequest.PageSize);

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListBookResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListBookResponse>>(result, Messages.BooksListed);
            }

            return new ErrorDataResult<IPaginate<GetListBookResponse>>(Messages.Error);
        }

        [SecuredOperation("admin,get")]
        public async Task<IDataResult<IPaginate<GetListBookResponse>>> GetListAsyncByCategory(PageRequest pageRequest, Guid categoryId)
        {
            var data = await _bookDal.GetListAsync(
                predicate: b => b.CategoryId == categoryId,
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
                );

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListBookResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListBookResponse>>(result, Messages.BooksListed);
            }

            return new ErrorDataResult<IPaginate<GetListBookResponse>>(Messages.Error);
        }

        [SecuredOperation("admin,get")]
        public async Task<IDataResult<IPaginate<GetListBookResponse>>> GetListAsyncSortedByName(PageRequest pageRequest)
        {
            var data = await _bookDal.GetListAsyncOrderBy(
                predicate: null,
                orderBy: q => q.OrderBy(b => b.BookName),
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
                );

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListBookResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListBookResponse>>(result, Messages.BooksListed);
            }

            return new ErrorDataResult<IPaginate<GetListBookResponse>>(Messages.Error);
        }

        [SecuredOperation("admin,get")]
        public async Task<IDataResult<IPaginate<GetListBookResponse>>> GetListAsyncSortedByCreatedDate(PageRequest pageRequest)
        {
            var data = await _bookDal.GetListAsyncOrderBy(
                predicate: null,
                orderBy: q => q.OrderByDescending(b => b.CreatedDate),
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
                );

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListBookResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListBookResponse>>(result, Messages.BooksListed);
            }

            return new ErrorDataResult<IPaginate<GetListBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<Book>>> GetListWithAuthors(Expression<Func<Book, bool>> predicate, PageRequest pageRequest)
        {
            var data = await _bookDal.GetListWithAuthors(
               null,
               index: pageRequest.PageIndex,
               size: pageRequest.PageSize);

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<Book>>(data);

                return new SuccessDataResult<IPaginate<Book>>(result, Messages.BooksListed);
            }

            return new ErrorDataResult<IPaginate<Book>>(Messages.Error);
        }

    }
}
