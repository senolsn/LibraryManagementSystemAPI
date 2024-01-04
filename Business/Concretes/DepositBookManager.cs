using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.DepositBook;
using Business.Dtos.Response.DepositBook;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using Entities.Concrete.enums;
using System;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class DepositBookManager : IDepositBookService
    {
        protected readonly IDepositBookDal _depositBookDal;
        protected readonly IMapper _mapper;

        public object request { get; private set; }

        public DepositBookManager(IDepositBookDal depositBookDal, IMapper mapper)
        {
            _depositBookDal = depositBookDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CreateDepositBookRequest request)
        {
            DepositBook depositBook = _mapper.Map<DepositBook>(request);
            var createdDepositBook = await _depositBookDal.AddAsync(depositBook);
            if (createdDepositBook is null)
            {
                return new ErrorResult(Messages.Error);
            }
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

        public async Task<IResult> GetBookBack(Guid depositBookId)
        {
            var depositBookToUpdate = await _depositBookDal.GetAsync(d => d.DepositBookId == depositBookId);

            if (depositBookToUpdate is null)
            {
                return new ErrorResult(Messages.Error);
            }

            depositBookToUpdate.Status = DepositBookStatus.RECEIVED;

            await _depositBookDal.UpdateAsync(depositBookToUpdate);

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

        public async Task<IDataResult<IPaginate<GetListDepositBookResponse>>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _depositBookDal.GetListAsync(
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
    }
}
