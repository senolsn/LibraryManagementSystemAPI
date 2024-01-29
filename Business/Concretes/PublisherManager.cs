using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Publisher;
using Business.Dtos.Response.Language;
using Business.Dtos.Response.Publisher;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class PublisherManager : IPublisherService
    {
        protected readonly IPublisherDal _publisherDal;
        protected readonly IBookService _bookService;
        protected readonly IMapper _mapper;

        public PublisherManager(IPublisherDal publisherDal, IBookService bookService, IMapper mapper)
        {
            _publisherDal = publisherDal;
            _bookService = bookService;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CreatePublisherRequest request)
        {
            Publisher publisher = _mapper.Map<Publisher>(request);

            var createdPublisher = await _publisherDal.AddAsync(publisher);

            var dbResult = await _publisherDal.SaveChangesAsync();

            if (!dbResult)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.PublisherAdded);
        }

        public async Task<IResult> Delete(DeletePublisherRequest request)
        {
            var publisherToDelete = await _publisherDal.GetAsync(p => p.PublisherId == request.PublisherId);

            if (publisherToDelete is not null)
            {
                await _publisherDal.DeleteAsync(publisherToDelete);
                return new SuccessResult(Messages.PublisherDeleted);
            }

            return new ErrorResult(Messages.Error);
        }

        public async Task<IDataResult<Publisher>> GetAsync(Guid publisherId)
        {
            var result = await _publisherDal.GetAsync(p => p.PublisherId == publisherId);

            if (result is not null)
            {
                return new SuccessDataResult<Publisher>(result, Messages.PublisherListed);
            }

            return new ErrorDataResult<Publisher>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListPublisherResponse>>> GetListAsync()
        {
            var data = await _publisherDal.GetListAsync(null);

            if (data is not null)
            {
                var publishersResponse = _mapper.Map<List<GetListPublisherResponse>>(data);

                return new SuccessDataResult<List<GetListPublisherResponse>>(publishersResponse, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListPublisherResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListPublisherResponse>>> GetListAsyncSortedByCreatedDate()
        {
            var data = await _publisherDal.GetListAsyncOrderBy(null, orderBy: q => q.OrderByDescending(p => p.CreatedDate));

            if (data is not null)
            {
                var publishersResponse = _mapper.Map<List<GetListPublisherResponse>>(data);

                return new SuccessDataResult<List<GetListPublisherResponse>>(publishersResponse, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListPublisherResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListPublisherResponse>>> GetListAsyncSortedByName()
        {
            var data = await _publisherDal.GetListAsyncOrderBy(null, orderBy: q => q.OrderBy(p => p.PublisherName));

            if (data is not null)
            {
                var publishersResponse = _mapper.Map<List<GetListPublisherResponse>>(data);

                return new SuccessDataResult<List<GetListPublisherResponse>>(publishersResponse, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListPublisherResponse>>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<GetListPublisherResponse>>> GetPaginatedListAsync(PageRequest pageRequest)
        {
            var data = await _publisherDal.GetPaginatedListAsync(
                null,
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize,
                true);

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListPublisherResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListPublisherResponse>>(result, Messages.PublishersListed);
            }

            return new ErrorDataResult<IPaginate<GetListPublisherResponse>>(Messages.Error);
        }

        public async Task<IResult> Update(UpdatePublisherRequest request)
        {
            var publisherToUpdate = await _publisherDal.GetAsync(p => p.PublisherId == request.PublisherId);

            if (publisherToUpdate is null)
            {
                return new ErrorResult(Messages.Error);
            }

            _mapper.Map(request, publisherToUpdate);

            await _publisherDal.UpdateAsync(publisherToUpdate);

            return new SuccessResult(Messages.PublisherUpdated);
        }

    }
}
