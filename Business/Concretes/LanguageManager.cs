using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Language;
using Business.Dtos.Response.Language;
using Business.ValidationRules.FluentValidation;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Autofac.Caching;
using Core.DataAccess.Paging;
using Core.Utilities.Business;
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
        protected readonly IBookService _bookService;
        protected readonly IMapper _mapper;

        public LanguageManager(ILanguageDal languageDal, IBookService bookService, IMapper mapper)
        {
            _languageDal = languageDal;
            _bookService = bookService;
            _mapper = mapper;
        }

        //[SecuredOperation("admin,add")]
        [ValidationAspect(typeof (LanguageValidator))]
        [CacheRemoveAspect("ILanguageService.Get")]
        public async Task<IResult> Add(CreateLanguageRequest request)
        {
            var result = BusinessRules.Run(CapitalizeFirstLetter(request), await IsLanguageNameUnique(request.LanguageName));
            
            if (result is not null)
            {
                return result;
            }

            Language language = _mapper.Map<Language>(request);

            var createdLanguage = await _languageDal.AddAsync(language);

            var dbResult = await _languageDal.SaveChangesAsync();

            if (!dbResult)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.LanguageAdded);
        }

        [SecuredOperation("admin,update")]
        [ValidationAspect(typeof(LanguageValidator))]
        [CacheRemoveAspect("ILanguageService.Get")]
        public async Task<IResult> Update(UpdateLanguageRequest request)
        {
            var result = BusinessRules.Run(CapitalizeFirstLetter(request), await IsLanguageNameUnique(request.LanguageName));

            if (result is not null)
            {
                return result;
            }
          
            var languageToUpdate = await _languageDal.GetAsync(l => l.LanguageId == request.LanguageId);

            if (languageToUpdate is null)
            {
                return new ErrorResult(Messages.Error);
            }

            _mapper.Map(request, languageToUpdate);

            await _languageDal.UpdateAsync(languageToUpdate);

            return new SuccessResult(Messages.LanguageUpdated);
        }

        [SecuredOperation("admin,delete")]
        [CacheRemoveAspect("ILanguageService.Get")]
        public async Task<IResult> Delete(DeleteLanguageRequest request)
        {
            var languageToDelete = await _languageDal.GetAsync(l => l.LanguageId == request.LanguageId);

            if(languageToDelete is not null)
            {
                var result = BusinessRules.Run(await CheckIfExistInBooks(request.LanguageId));

               if(result is not null)
                {
                    return result;
                }

                await _languageDal.DeleteAsync(languageToDelete);
                return new SuccessResult(Messages.LanguageDeleted);
            }

            return new ErrorResult(Messages.Error);
        }

        [SecuredOperation("admin,get")]
        [CacheAspect]
        public async Task<IDataResult<Language>> GetAsync(Guid languageId)
        {
            var result = await _languageDal.GetAsync(l => l.LanguageId == languageId);

            if(result is not null)
            {
                return new SuccessDataResult<Language>(result,Messages.LanguageListed);
            }
            
            return new ErrorDataResult<Language>(Messages.Error);
        }

        //[SecuredOperation("admin,get")]
        [CacheAspect]
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

        #region Helper Methods
        private async Task<IResult> CheckIfExistInBooks(Guid languageId)
        {
            var result = await _bookService.GetAsyncByLanguageId(languageId);
            if(result is not null)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.LanguageExistInBooks);
        }
        private IDataResult<CreateLanguageRequest> CapitalizeFirstLetter(CreateLanguageRequest request)
        {
            string capitalizedLanguageName = char.ToUpper(request.LanguageName[0]) + request.LanguageName.Substring(1).ToLower();
            request.LanguageName = capitalizedLanguageName;
            return new SuccessDataResult<CreateLanguageRequest>(request);
        }

        private IDataResult<UpdateLanguageRequest> CapitalizeFirstLetter(UpdateLanguageRequest request)
        {
            string capitalizedLanguageName = char.ToUpper(request.LanguageName[0]) + request.LanguageName.Substring(1).ToLower();
            request.LanguageName = capitalizedLanguageName;
            return new SuccessDataResult<UpdateLanguageRequest>(request);
        }

        private async Task<IResult> IsLanguageNameUnique(string languageName)
        {
            var result = await _languageDal.GetAsync(l => l.LanguageName.ToUpper() == languageName.ToUpper());

            if (result is not null)
            {
                return new ErrorResult(Messages.LanguageNameNotUnique);
            }
            return new SuccessResult();
        }

        #endregion
    }
}
