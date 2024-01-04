using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Language;
using Business.Dtos.Response.Language;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class LanguageManager : ILanguageService
    {

        protected readonly ILanguageDal _languageDal;
        protected readonly IBookDal _bookDal;
        protected readonly IMapper _mapper;

        public LanguageManager(ILanguageDal languageDal, IBookDal bookDal, IMapper mapper)
        {
            _languageDal = languageDal;
            _bookDal = bookDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CreateLanguageRequest request)
        {
            Language language = _mapper.Map<Language>(request);

            var createdLanguage = await _languageDal.AddAsync(language);

            if (createdLanguage is null) 
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.LanguageAdded);
        }

        public async Task<IResult> Update(UpdateLanguageRequest request)
        {
            var languageToUpdate = await _languageDal.GetAsync(l => l.LanguageId == request.LanguageId);

            if (languageToUpdate is null)
            {
                return new ErrorResult(Messages.Error);
            }

            _mapper.Map(request, languageToUpdate);

            await _languageDal.UpdateAsync(languageToUpdate);

            return new SuccessResult(Messages.LanguageUpdated);
        }

        public async Task<IResult> Delete(DeleteLanguageRequest request)
        {
            var languageToDelete = await _languageDal.GetAsync(l => l.LanguageId == request.LanguageId);

            if(languageToDelete is not null)
            {
               if (CheckIfExistInBooks(request.LanguageId))
               {
                    return new ErrorResult(Messages.LanguageExistInBooks);
               }
               await _languageDal.DeleteAsync(languageToDelete);
               return new SuccessResult(Messages.LanguageDeleted);
            }

            return new ErrorResult(Messages.Error);
        }

        public async Task<IDataResult<Language>> GetAsync(Guid languageId)
        {
            var result = await _languageDal.GetAsync(l => l.LanguageId == languageId);

            if(result is not null)
            {
                return new SuccessDataResult<Language>(result,Messages.LanguageListed);
            }
            
            return new ErrorDataResult<Language>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<GetListLanguageResponse>>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _languageDal.GetListAsync(
                null, 
                index : pageRequest.PageIndex,
                size : pageRequest.PageSize,
                true);

           if(data is not null)
            {
                var result = _mapper.Map<Paginate<GetListLanguageResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListLanguageResponse>>(result,Messages.LanguagesListed);
            }

           return new ErrorDataResult<IPaginate<GetListLanguageResponse>>(Messages.Error);
        }

        private bool CheckIfExistInBooks(Guid languageId)
        {
            if(_bookDal.GetAsync(b => b.LanguageId == languageId) is null)
            {
                return false;
            }
            return true;
        }
    }
}
