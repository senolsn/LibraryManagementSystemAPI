using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Business.Dtos.Request.Book;
using Business.Dtos.Request.BookRequests;
using Business.Dtos.Request.Category;
using Business.Dtos.Response.Book;
using Business.ValidationRules.FluentValidation.Book;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess.Paging;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace Business.Concretes
{
    public class BookManager : IBookService
    {
        protected readonly IBookDal _bookDal;
        protected readonly IMapper _mapper;
        private readonly Lazy<IAuthorService> _authorService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<ILanguageService> _languageService;
        private readonly Lazy<IPublisherService> _publisherService;
        private readonly Lazy<IInterpreterService> _interpreterService;
        private readonly Lazy<ILocationService> _locationService;

        public BookManager(IBookDal bookDal, IMapper mapper, Lazy<IAuthorService> authorService, Lazy<ICategoryService> categoryService, Lazy<ILanguageService> languageService, Lazy<IPublisherService> publisherService, Lazy<IInterpreterService> interpreterService, Lazy<ILocationService> locationService)
        {
            _bookDal = bookDal;
            _mapper = mapper;
            _authorService = authorService;
            _categoryService = categoryService;
            _languageService = languageService;
            _publisherService = publisherService;
            _interpreterService = interpreterService;
            _locationService = locationService;
        }

        //[SecuredOperation("admin,add")]
        [ValidationAspect(typeof (CreateBookValidator))]
        [CacheRemoveAspect("IBookService.Get")]
        public async Task<IResult> Add(CreateBookRequest request)
        {
            var businessResults = BusinessRules.Run(CapitalizeFirstLetter(request));

            if(businessResults is not null)
            {
                return businessResults;
            }

            Book book = _mapper.Map<Book>(request);

            book.BookAuthors = new List<Author>();
            book.BookCategories = new List<Category>();
            book.BookLanguages = new List<Language>();
            book.BookInterpreters = new List<Interpreter>();

            foreach (var authorId in request.AuthorIds)
            {
                var result = await _authorService.Value.GetAsync(authorId);
                ArgumentNullException.ThrowIfNull(result.Data , "Author");
                book.BookAuthors.Add(result.Data);
            }

            foreach (var categoryId in request.CategoryIds)
            {
                var result = await _categoryService.Value.GetAsync(categoryId);
                ArgumentNullException.ThrowIfNull(result.Data, "Category");
                book.BookCategories.Add(result.Data);

            }

            foreach(var languageId in request.LanguageIds)
            {
                var result = await _languageService.Value.GetAsync(languageId);
                ArgumentNullException.ThrowIfNull(result.Data, "Language");
                book.BookLanguages.Add(result.Data);
            }

            foreach (var interpreterId in request.InterpreterIds)
            {
                var result = await _interpreterService.Value.GetAsync(interpreterId);
                ArgumentNullException.ThrowIfNull(result.Data, "Interpreter");
                book.BookInterpreters.Add(result.Data);
            }

            var publisherResult = await _publisherService.Value.GetAsync(request.PublisherId);
            book.Publisher = publisherResult.Data;

            var locationResult = await _locationService.Value.GetAsync(request.LocationId);
            book.Location = locationResult.Data;


            var createdBook = await _bookDal.AddAsync(book);

            if (createdBook is null)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.BookListed);
        }

        [SecuredOperation("admin,update")]
        [ValidationAspect(typeof (CreateBookValidator))]
        [CacheRemoveAspect("IBookService.Get")]
        public async Task<IResult> Update(UpdateBookRequest request)
        {
            var result = BusinessRules.Run(CapitalizeFirstLetter(request));

            if (result is not null)
            {
                return result;
            }

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
        [ValidationAspect(typeof (CreateBookValidator))]
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

        //[SecuredOperation("admin,get")]
        public async Task<IDataResult<Book>> GetAsync(Guid bookId)
        {
            var result = await _bookDal.GetAsync(b => b.BookId == bookId);

            if (result is not null)
            {
                return new SuccessDataResult<Book>(result, Messages.BookListed);
            }

            return new ErrorDataResult<Book>(Messages.Error);
        }

        //[SecuredOperation("admin,get")]
        public async Task<IDataResult<IPaginate<GetListBookResponse>>> GetPaginatedListAsyncByCategory(PageRequest pageRequest, List<Guid> categoryIds)
        {
            var data = await _bookDal.GetPaginatedListAsync(
              null,
              index: pageRequest.PageIndex,
              size: pageRequest.PageSize);

            var filteredBooks = data.Items.Where(book => book.BookCategories.Any(category => categoryIds.Contains(category.CategoryId))).ToList();
            if (data is not null)
            {
                var booksResponse = _mapper.Map<List<GetListBookResponse>>(filteredBooks);

                var paginatedBooksResponse = new Paginate<GetListBookResponse>
                {
                    Items = booksResponse
                };


                return new SuccessDataResult<IPaginate<GetListBookResponse>>(paginatedBooksResponse, Messages.BooksListed);
            }
            return new ErrorDataResult<IPaginate<GetListBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListBookResponse>>> GetListAsyncByCategory(List<Guid> categoryIds)
        {
            var data = await _bookDal.GetListAsync(predicate: book => book.BookCategories.Any(category => categoryIds.Contains(category.CategoryId)));

            if (data is not null)
            {
                var bookResponse = _mapper.Map<List<GetListBookResponse>>(data);

                return new SuccessDataResult<List<GetListBookResponse>>(bookResponse, Messages.BooksListed);
            }
            return new ErrorDataResult<List<GetListBookResponse>>(Messages.Error);
        }

        //[SecuredOperation("admin,get")]
        public async Task<IDataResult<IPaginate<GetListBookResponse>>> GetPaginatedListAsyncByLanguage(PageRequest pageRequest, List<Guid> languageIds)
        {
            var data = await _bookDal.GetPaginatedListAsync(
               null,
               index: pageRequest.PageIndex,
               size: pageRequest.PageSize); 

            var filteredBooks = data.Items.Where(book => book.BookLanguages.Any(lang => languageIds.Contains(lang.LanguageId))).ToList();

            if (data is not null)
            {
                var booksResponse = _mapper.Map<List<GetListBookResponse>>(filteredBooks);

                var paginatedBooksResponse = new Paginate<GetListBookResponse>
                {
                    Items = booksResponse
                };


                return new SuccessDataResult<IPaginate<GetListBookResponse>>(paginatedBooksResponse, Messages.BooksListed);
            }
            return new ErrorDataResult<IPaginate<GetListBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListBookResponse>>> GetListAsyncByLanguage(List<Guid> languageIds)
        {
            var data = await _bookDal.GetListAsync(predicate: book => book.BookLanguages.Any(language => languageIds.Contains(language.LanguageId)));

            if(data is not null)
            {
                var bookResponse = _mapper.Map<List<GetListBookResponse>>(data);
                return new SuccessDataResult<List<GetListBookResponse>>(bookResponse, Messages.BooksListed);
            }
            return new ErrorDataResult<List<GetListBookResponse>>(Messages.Error);
                
        }

        //[SecuredOperation("admin,get")]
        public async Task<IDataResult<IPaginate<GetListBookResponse>>> GetPaginatedListAsync(PageRequest pageRequest)
        {
            var data = await _bookDal.GetPaginatedListAsync(
               null,
               index: pageRequest.PageIndex,
               size: pageRequest.PageSize);

            if (data is not null)
            {
                var booksResponse = _mapper.Map<List<GetListBookResponse>>(data.Items);

                var paginatedBooksResponse = new Paginate<GetListBookResponse>
                {
                    Items = booksResponse
                };


                return new SuccessDataResult<IPaginate<GetListBookResponse>>(paginatedBooksResponse, Messages.BooksListed);
            }

            return new ErrorDataResult<IPaginate<GetListBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListBookResponse>>> GetListAsync()
        {
            var data = await _bookDal.GetListAsync();

            List<GetListBookResponse> responseBooks = new List<GetListBookResponse>();
            if (data is not null)
            {
                foreach (var item in data)
                {
                    var bookResponse = _mapper.Map<GetListBookResponse>(item);
                    responseBooks.Add(bookResponse);
                }

                return new SuccessDataResult<List<GetListBookResponse>>(responseBooks, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListBookResponse>>(Messages.Error);
        }

        //[SecuredOperation("admin,get")]
        public async Task<IDataResult<IPaginate<GetListBookResponse>>> GetPaginatedListAsyncSortedByName(PageRequest pageRequest)
        {
            var data = await _bookDal.GetListPaginatedAsyncOrderBy(
                predicate: null,
                orderBy: q => q.OrderBy(b => b.BookName),
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
                );

            if (data is not null)
            {
                var booksResponse = _mapper.Map<List<GetListBookResponse>>(data.Items);

                var paginatedBooksResponse = new Paginate<GetListBookResponse>
                {
                    Items = booksResponse
                };


                return new SuccessDataResult<IPaginate<GetListBookResponse>>(paginatedBooksResponse, Messages.BooksListed);
            }

            return new ErrorDataResult<IPaginate<GetListBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListBookResponse>>> GetListAsyncSortedByName()
        {
            var data = await _bookDal.GetListAsyncOrderBy(
                predicate: null,
                orderBy: q => q.OrderBy(b => b.BookName)
                );

            if(data is not null)
            {
                var bookResponse = _mapper.Map<List<GetListBookResponse>>(data);
                return new SuccessDataResult<List<GetListBookResponse>>(bookResponse, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListBookResponse>>(Messages.Error);
        }

        [SecuredOperation("admin,get")]
        public async Task<IDataResult<IPaginate<GetListBookResponse>>> GetPaginatedListAsyncSortedByCreatedDate(PageRequest pageRequest)
        {
            var data = await _bookDal.GetListPaginatedAsyncOrderBy(
                predicate: null,
                orderBy: q => q.OrderByDescending(b => b.CreatedDate),
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
                );

            if (data is not null)
            {
                var booksResponse = _mapper.Map<List<GetListBookResponse>>(data.Items);

                var paginatedBooksResponse = new Paginate<GetListBookResponse>
                {
                    Items = booksResponse
                };


                return new SuccessDataResult<IPaginate<GetListBookResponse>>(paginatedBooksResponse, Messages.BooksListed);
            }

            return new ErrorDataResult<IPaginate<GetListBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListBookResponse>>> GetListAsyncSortedByCreatedDate()
        {
            var data = await _bookDal.GetListAsyncOrderBy(
                predicate:null,
                orderBy: q => q.OrderBy(b => b.CreatedDate)
                );

            if( data is not null)
            {
                var bookResponse = _mapper.Map<List<GetListBookResponse>>(data);

                return new SuccessDataResult<List<GetListBookResponse>>(bookResponse, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListBookResponse>>(Messages.Error);

        }

        public async Task<IDataResult<Book>> GetAsyncByLocation(Guid locationId)
        {
            var data = await _bookDal.GetAsync(predicate: b => b.Location.LocationId == locationId);

            if (data is not null)
            {
                return new SuccessDataResult<Book>(data,Messages.BooksListed);
            }
            return new ErrorDataResult<Book>(Messages.Error);

        }
        public async Task<IDataResult<Book>> GetAsyncByPublisher(Guid publisherId)
        {
            var data = await _bookDal.GetAsync(predicate: b => b.Publisher.PublisherId == publisherId);

            if (data is not null)
            {
                return new SuccessDataResult<Book>(data, Messages.BooksListed);
            }
            return new ErrorDataResult<Book>(Messages.Error);
        }

        #region Helper Methods
        private IDataResult<IBookRequest> CapitalizeFirstLetter(IBookRequest request)
        {
            var stringToArray = request.BookName.Split(' ', ',', '.');
            string[] arrayToString = new string[stringToArray.Length];
            int count = 0;

            foreach (var word in stringToArray)
            {
                var capitalizedCategoryName = char.ToUpper(word[0]) + word.Substring(1).ToLower();
                arrayToString[count] = capitalizedCategoryName;
                count++;
            }
            request.BookName = string.Join(" ", arrayToString);

            return new SuccessDataResult<IBookRequest>(request);
        }
        #endregion

    }
}
