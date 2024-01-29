using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.InterpreterRequests;
using Business.Dtos.Response.InterpreterResponses;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class InterpreterManager : IInterpreterService
    {
        protected readonly IInterpreterDal _interpreterDal;
        protected readonly IMapper _mapper;

        public InterpreterManager(IInterpreterDal interpreterDal, IMapper mapper)
        {
            _interpreterDal = interpreterDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CreateInterpreterRequest request)
        {
            Interpreter interpreter = _mapper.Map<Interpreter>(request);

            var createdInterpreter = await _interpreterDal.AddAsync(interpreter);

            if (createdInterpreter is null)
            {
                return new ErrorResult(Messages.Error);
            }
            return new SuccessResult(Messages.InterpreterAdded);
        }

        public async Task<IResult> Update(UpdateInterpreterRequest request)
        {
            var interpreterToUpdate = await _interpreterDal.GetAsync(i => i.InterpreterId == request.InterpreterId);

            if (interpreterToUpdate is null)
            {
                return new ErrorResult(Messages.Error);
            }
            _mapper.Map(request, interpreterToUpdate);

            await _interpreterDal.UpdateAsync(interpreterToUpdate);

            return new SuccessResult(Messages.InterpreterUpdated);
        }

        public async Task<IResult> Delete(DeleteInterpreterRequest request)
        {
            var interpreterToDelete = await _interpreterDal.GetAsync(i => i.InterpreterId == request.InterpreterId);

            if (interpreterToDelete is not null)
            {
                await _interpreterDal.DeleteAsync(interpreterToDelete);
                return new SuccessResult(Messages.InterpreterDeleted);
            }

            return new ErrorResult(Messages.Error);
        }

        public async Task<IDataResult<Interpreter>> GetAsync(Guid interpreterId)
        {
            var result = await _interpreterDal.GetAsync(i => i.InterpreterId == interpreterId);

            if (result is not null)
            {
                var data = _mapper.Map<Interpreter>(result);
                return new SuccessDataResult<Interpreter>(data, Messages.InterpreterListed);

            }
            return new ErrorDataResult<Interpreter>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<GetListInterpreterResponse>>> GetPaginatedListAsync(PageRequest pageRequest)
        {
            var data = await _interpreterDal.GetPaginatedListAsync(
                predicate: null,
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
                );

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListInterpreterResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListInterpreterResponse>>(result, Messages.InterpretersListed);
            }

            return new ErrorDataResult<IPaginate<GetListInterpreterResponse>>(Messages.Error);
        }

    }

}
