using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.DepositBook;
using Business.Dtos.Response.DepositBook;
using Core.DataAccess.Paging;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using Entities.Concrete.enums;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class DepositBookManager : IDepositBookService
    {
        protected readonly IDepositBookDal _depositBookDal;
        protected readonly IBookService _bookService;
        private readonly IUserService _userService;
        private readonly ISettingService _settingService;
        protected readonly IMapper _mapper;

        public DepositBookManager(IDepositBookDal depositBookDal, IMapper mapper, IUserService userService, IBookService bookService, ISettingService settingService)
        {
            _depositBookDal = depositBookDal;
            _mapper = mapper;
            _userService = userService;
            _bookService = bookService;
            _settingService = settingService;
        }


        //[DecreaseBookStock]
        public async Task<IResult> Add(CreateDepositBookRequest request)
        {
            DepositBook depositBook = _mapper.Map<DepositBook>(request);

            var bookResult = await _bookService.GetAsync(request.BookId);
            depositBook.Book = bookResult.Data;

            var businessRules = BusinessRules.Run(CheckIfBookInStock(bookResult.Data), await CheckIfUserHasOverdueBooks(request.UserId));

            if(businessRules is not null)
            {
                return businessRules;
            }

            var userResult = await _userService.GetAsync(request.UserId);
            depositBook.User = userResult.Data;
            
            var createdDepositBook = await _depositBookDal.AddAsync(depositBook);
            if (createdDepositBook is null)
            {
                return new ErrorResult(Messages.Error);
            }

            //Stock azaltma --- Bu ve bu tarz işlemleri aspect'lerle yaparız. Şimdilik dursun. Oraya taşıyacağız.
            await DecreaseBookStock(depositBook);


            return new SuccessResult(Messages.DepositBookAdded);
        }

        public async Task<IResult> Delete(DeleteDepositBookRequest request)
        {
            var depositBookToDelete = await _depositBookDal.GetAsync(d => d.DepositBookId == request.DepositBookId);

            if (depositBookToDelete is not null)
            {
                await _depositBookDal.DeleteAsync(depositBookToDelete);
                return new SuccessResult(Messages.DepositBookDeleted);
            }

            return new ErrorResult(Messages.Error);
        }

        //[IncreaseBookStock]
        public async Task<IResult> GetBookBack(Guid depositBookId)
        {
            var depositBookToUpdate = await _depositBookDal.GetAsync(d => d.DepositBookId == depositBookId);

            if (depositBookToUpdate is null)
            {
                return new ErrorResult(Messages.Error);
            }

            depositBookToUpdate.Status = DepositBookStatus.RECEIVED;

            await _depositBookDal.UpdateAsync(depositBookToUpdate);

            //Stock artırma --- Bu ve bu tarz işlemleri aspect'lerle yaparız. Şimdilik dursun. Oraya taşıyacağız.
            await IncreaseBookStock(depositBookToUpdate);
            return new SuccessResult(Messages.DepositBookUpdated);
        }

        public async Task<IDataResult<DepositBook>> GetAsync(Guid depositBookId)
        {
            var result = await _depositBookDal.GetAsync(d => d.DepositBookId == depositBookId);

            if (result is not null)
            {
                return new SuccessDataResult<DepositBook>(result, Messages.DepositBookListed);
            }

            return new ErrorDataResult<DepositBook>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetPaginatedListAsync(PageRequest pageRequest)
        {
            var data = await _depositBookDal.GetPaginatedListAsync(
               null,
               index: pageRequest.PageIndex,
               size: pageRequest.PageSize,
               true
               );

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListDepositBookResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListDepositBookResponse>>(result, Messages.DepositBooksListed);
            }

            return new ErrorDataResult<IPaginate<GetListDepositBookResponse>>(Messages.Error);
        }

        public async Task<IResult> Update(UpdateDepositBookRequest request)
        {
            var depositBookToUpdate = await _depositBookDal.GetAsync(d => d.DepositBookId == request.DepositBookId);

            if (depositBookToUpdate is null)
            {
                return new ErrorResult(Messages.Error);
            }

            _mapper.Map(request, depositBookToUpdate);

            await _depositBookDal.UpdateAsync(depositBookToUpdate);

            return new SuccessResult(Messages.DepositBookUpdated);
        }

        public async Task<IDataResult<DepositBook>> GetAsyncByBookAndUserId(Guid bookId, Guid userId)
        {
            var result = await _depositBookDal.GetAsync(d => d.BookId == bookId && d.UserId == userId);

            if (result is not null)
            {
                return new SuccessDataResult<DepositBook>(result, Messages.DepositBookListed);
            }

            return new ErrorDataResult<DepositBook>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListDepositBookResponse>>> GetListAsync()
        {
            var data = await _depositBookDal.GetListAsync();

            List<GetListDepositBookResponse> responseBooks = new List<GetListDepositBookResponse>();
            if (data is not null)
            {
                foreach (var item in data)
                {
                    var bookResponse = _mapper.Map<GetListDepositBookResponse>(item);
                    responseBooks.Add(bookResponse);

                }

                return new SuccessDataResult<List<GetListDepositBookResponse>>(responseBooks, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListDepositBookResponse>>(Messages.Error);
        }
        
        public async Task<IDataResult<List<GetListDepositBookResponse>>> GetListAsyncSortedByCreatedDate()
        {
            var data = await _depositBookDal.GetListAsyncOrderBy(predicate: null, orderBy: q => q.OrderByDescending(b => b.CreatedDate));

            List<GetListDepositBookResponse> responseBooks = new List<GetListDepositBookResponse>();
            if (data is not null)
            {
                foreach (var item in data)
                {
                    var bookResponse = _mapper.Map<GetListDepositBookResponse>(item);
                    responseBooks.Add(bookResponse);

                }

                return new SuccessDataResult<List<GetListDepositBookResponse>>(responseBooks, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListDepositBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListDepositBookResponse>>> GetListAsyncUndeposited()
        {
            var data = await _depositBookDal.GetListAsync(predicate: d => d.Status == DepositBookStatus.NOT_RECEIVED);

            List<GetListDepositBookResponse> responseBooks = new List<GetListDepositBookResponse>();
            if (data is not null)
            {
                foreach (var item in data)
                {
                    var bookResponse = _mapper.Map<GetListDepositBookResponse>(item);
                    responseBooks.Add(bookResponse);

                }

                return new SuccessDataResult<List<GetListDepositBookResponse>>(responseBooks, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListDepositBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListDepositBookResponse>>> GetListAsyncDeposited()
        {
            var data = await _depositBookDal.GetListAsync(predicate: d => d.Status == DepositBookStatus.RECEIVED);

            List<GetListDepositBookResponse> responseBooks = new List<GetListDepositBookResponse>();
            if (data is not null)
            {
                foreach (var item in data)
                {
                    var bookResponse = _mapper.Map<GetListDepositBookResponse>(item);
                    responseBooks.Add(bookResponse);

                }

                return new SuccessDataResult<List<GetListDepositBookResponse>>(responseBooks, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListDepositBookResponse>>(Messages.Error);
        }
        public async Task<IDataResult<List<GetListDepositBookResponse>>> GetListAsyncExpired()
        {
            var settings = _settingService.GetListAsync().Result.Data[0];
            var data = await _depositBookDal.GetListAsync(predicate: d => d.Status == DepositBookStatus.NOT_RECEIVED);

            List<GetListDepositBookResponse> responseBooks = new List<GetListDepositBookResponse>();
            if (data is not null)
            {
                foreach (var item in data)
                {

                    if (item is not null)
                    {
                        DateTime expirationDate = item.DepositDate.AddDays(settings.BookReturnDay);
                        if (expirationDate <= DateTime.Now)
                        {
                            var bookResponse = _mapper.Map<GetListDepositBookResponse>(item);
                            responseBooks.Add(bookResponse);
                        }
                    }
                }

                return new SuccessDataResult<List<GetListDepositBookResponse>>(responseBooks, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListDepositBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListDepositBookResponse>>> GetListAsyncExpiredByUser(Guid userId)
        {
            var settings = _settingService.GetListAsync().Result.Data[0];
            var data = await _depositBookDal.GetListAsync(predicate: d => d.Status == DepositBookStatus.NOT_RECEIVED && d.UserId == userId);

            List<GetListDepositBookResponse> responseBooks = new List<GetListDepositBookResponse>();
            if (data is not null)
            {
                foreach (var item in data)
                {
                    if (item is not null)
                    {
                        DateTime expirationDate = item.DepositDate.AddDays(settings.BookReturnDay);
                        if (expirationDate <= DateTime.Now)
                        {
                            var bookResponse = _mapper.Map<GetListDepositBookResponse>(item);
                            responseBooks.Add(bookResponse);
                        }
                    }
                }

                return new SuccessDataResult<List<GetListDepositBookResponse>>(responseBooks, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListDepositBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListDepositBookResponse>>> GetListAsyncByUserId(Guid userId)
        {
            var data = await _depositBookDal.GetListAsync(predicate: d => d.UserId == userId);

            List<GetListDepositBookResponse> responseBooks = new List<GetListDepositBookResponse>();
            if (data is not null)
            {
                foreach (var item in data)
                {
                    var bookResponse = _mapper.Map<GetListDepositBookResponse>(item);
                    responseBooks.Add(bookResponse);

                }

                return new SuccessDataResult<List<GetListDepositBookResponse>>(responseBooks, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListDepositBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListDepositBookResponse>>> GetListAsyncByBookId(Guid bookId)
        {
            var data = await _depositBookDal.GetListAsync(predicate: d => d.BookId == bookId);

            List<GetListDepositBookResponse> responseBooks = new List<GetListDepositBookResponse>();
            if (data is not null)
            {
                foreach (var item in data)
                {
                    var bookResponse = _mapper.Map<GetListDepositBookResponse>(item);
                    responseBooks.Add(bookResponse);

                }

                return new SuccessDataResult<List<GetListDepositBookResponse>>(responseBooks, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListDepositBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListDepositBookResponse>>> GetPaginatedListAsyncUndepositedByUserId(Guid userId)
        {
            var data = await _depositBookDal.GetListAsync(predicate: d => d.Status == DepositBookStatus.NOT_RECEIVED && d.UserId == userId);

            List<GetListDepositBookResponse> responseBooks = new List<GetListDepositBookResponse>();
            if (data is not null)
            {
                foreach (var item in data)
                {
                    var bookResponse = _mapper.Map<GetListDepositBookResponse>(item);
                    responseBooks.Add(bookResponse);

                }

                return new SuccessDataResult<List<GetListDepositBookResponse>>(responseBooks, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListDepositBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetPaginatedListAsyncUndeposited(PageRequest pageRequest)
        {
            var data = await _depositBookDal.GetPaginatedListAsync(
               predicate: d => d.Status == DepositBookStatus.NOT_RECEIVED,
               index: pageRequest.PageIndex,
               size: pageRequest.PageSize,
               true
               );

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListDepositBookResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListDepositBookResponse>>(result, Messages.DepositBooksListed);
            }

            return new ErrorDataResult<IPaginate<GetListDepositBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetPaginatedListAsyncDeposited(PageRequest pageRequest)
        {
            var data = await _depositBookDal.GetPaginatedListAsync(
               predicate: d => d.Status == DepositBookStatus.RECEIVED,
               index: pageRequest.PageIndex,
               size: pageRequest.PageSize,
               true
               );

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListDepositBookResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListDepositBookResponse>>(result, Messages.DepositBooksListed);
            }

            return new ErrorDataResult<IPaginate<GetListDepositBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetPaginatedListAsyncByUserId(PageRequest pageRequest, Guid userId)
        {
            var data = await _depositBookDal.GetPaginatedListAsync(
               predicate: d => d.UserId == userId,
               index: pageRequest.PageIndex,
               size: pageRequest.PageSize,
               true
               );

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListDepositBookResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListDepositBookResponse>>(result, Messages.DepositBooksListed);
            }

            return new ErrorDataResult<IPaginate<GetListDepositBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetPaginatedListAsyncUndepositedByUserId(PageRequest pageRequest, Guid userId)
        {
            var data = await _depositBookDal.GetPaginatedListAsync(
               predicate: d => d.UserId == userId && d.Status == DepositBookStatus.NOT_RECEIVED,
               index: pageRequest.PageIndex,
               size: pageRequest.PageSize,
               true
               );

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListDepositBookResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListDepositBookResponse>>(result, Messages.DepositBooksListed);
            }

            return new ErrorDataResult<IPaginate<GetListDepositBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetPaginatedListAsyncByBookId(PageRequest pageRequest, Guid bookId)
        {
            var data = await _depositBookDal.GetPaginatedListAsync(
               predicate: d => d.BookId == bookId,
               index: pageRequest.PageIndex,
               size: pageRequest.PageSize,
               true
               );

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListDepositBookResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListDepositBookResponse>>(result, Messages.DepositBooksListed);
            }

            return new ErrorDataResult<IPaginate<GetListDepositBookResponse>>(Messages.Error);
        }

        public async Task<IDataResult<DepositBook>> GetAsyncByUserId(Guid userId)
        {
            var result = await _depositBookDal.GetAsync(d => d.UserId == userId);

            if (result is not null)
            {
                return new SuccessDataResult<DepositBook>(result, Messages.DepositBookListed);
            }

            return new ErrorDataResult<DepositBook>(Messages.Error);
        }

        public async Task<IDataResult<ICollection<GetAllDepositBooksResponse>>> GetAllAsync()
        {
            var result = await _depositBookDal.GetAllAsync(null);
            if(result is not null)
            {
                return new SuccessDataResult<ICollection<GetAllDepositBooksResponse>>(result, Messages.DepositBookListed);
            }
            return new ErrorDataResult<ICollection<GetAllDepositBooksResponse>>(Messages.Error);
        }

        #region Helper Methods
        private IResult CheckIfBookInStock(Book book)
        {
            if (book is not null)
            {
                return book.Stock < 1 ? new ErrorResult(Messages.BookOutOfStock) : new SuccessResult();

            }
            return new ErrorResult(Messages.Error);
        }

        private async Task<IResult> CheckIfUserHasOverdueBooks(Guid userId)
        {

            var result = await this.GetListAsyncExpiredByUser(userId);
            if (result is not null)
            {
                foreach (var item in result.Data)
                {
                    if (item is not null)
                    {
                        return new ErrorResult(Messages.ReturnOverdueBooks);
                    }
                    
                }
            }
            return new SuccessResult();
        }

        private async Task<IResult> DecreaseBookStock(DepositBook depositBook)
        {
            var book = await _bookService.GetAsync(depositBook.BookId);
            book.Data.Stock = book.Data.Stock - 1;

            var result = await _bookService.UpdateForStock(book.Data);
            if(result is not null)
            {
                return new SuccessResult(Messages.BookUpdated);
            }
            return new ErrorResult(Messages.Error);
        }

        private async Task<IResult> IncreaseBookStock(DepositBook depositBook)
        {
            var book = await _bookService.GetAsync(depositBook.BookId);
            book.Data.Stock = book.Data.Stock + 1;

            var result = await _bookService.UpdateForStock(book.Data);
            if (result is not null)
            {
                return new SuccessResult(Messages.BookUpdated);
            }
            return new ErrorResult(Messages.Error);
        }

       




        #endregion


    }
}
