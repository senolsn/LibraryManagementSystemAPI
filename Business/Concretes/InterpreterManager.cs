using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Interpreter;
using Business.Dtos.Request.InterpreterRequests;
using Business.Dtos.Response.InterpreterResponses;
using Core.DataAccess.Paging;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
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
            var result = BusinessRules.Run(CapitalizeFirstLetter(request));

            if(result is not null)
            {
                return result;
            }

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

            if(interpreterToDelete is not null)
            {
                var result = BusinessRules.Run(CheckIfInterpretersHasBook(interpreterToDelete.InterpreterBooks));

                if(result is not null)
                {
                    return result;
                }
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

        #region Helper Methods

        private IDataResult<IInterpreterRequest> CapitalizeFirstLetter(IInterpreterRequest request)
        {
            var firstNameToArray = request.InterpreterFirstName.Split(' ', ',', '.');
            var lastNameToArray = request.InterpreterLastName.Split(' ', ',', '.');

            string[] arrayToFirstName = new string[firstNameToArray.Length];
            string[] arrayToLastName = new string[lastNameToArray.Length];

            int count = 0;

            foreach (var word in firstNameToArray)
            {
                var capitalizedFirstName = char.ToUpper(word[0]) + word.Substring(1).ToLower();
                arrayToFirstName[count] = capitalizedFirstName;
                count++;
            }

            count = 0;

            foreach (var word in lastNameToArray)
            {
                var capitalizedLastName = char.ToUpper(word[0]) + word.Substring(1).ToLower();
                arrayToLastName[count] = capitalizedLastName;
                count++;
            }

            request.InterpreterFirstName = string.Join(" ", arrayToFirstName);
            request.InterpreterLastName = string.Join(" ", arrayToLastName);

            return new SuccessDataResult<IInterpreterRequest>(request);
        }
        private IResult CheckIfInterpretersHasBook(ICollection<Book> interpreterBooks)
        {

            if (interpreterBooks.Count < 1)
            {
                return new SuccessResult();

            }
            return new ErrorResult(Messages.AuthorExistInBook);
        }

        #endregion

    }

}
