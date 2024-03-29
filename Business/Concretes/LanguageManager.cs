﻿using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Response.LanguageResponses;
using Business.BusinessAspects;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Autofac.Caching;
using Core.DataAccess.Paging;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Business.Dtos.Request.LanguageRequests;
using Business.ValidationRules.FluentValidation.LanguageValidator.LanguageValidator;

namespace Business.Concretes
{
    public class LanguageManager : ILanguageService
    {

        protected readonly ILanguageDal _languageDal;
        protected readonly IBookService _bookDal;
        protected readonly IMapper _mapper;

        public LanguageManager(ILanguageDal languageDal, IBookService bookDal, IMapper mapper)
        {
            _languageDal = languageDal;
            _bookDal = bookDal;
            _mapper = mapper;
        }

        //[SecuredOperation("admin,add")]
        [ValidationAspect(typeof (UpdateLanguageValidator))]
        [CacheRemoveAspect("ILanguageService.Get")]
        public async Task<IResult> Add(CreateLanguageRequest request)
        {
            var result = BusinessRules.Run(CapitalizeFirstLetter(request), await IsLanguageNameUnique(request.LanguageName));
            
            if (result is not null)
            {
                return result;
            }

            Language language = _mapper.Map<Language>(request);

             await _languageDal.AddAsync(language);

            var dbResult = await _languageDal.SaveChangesAsync();

            if (!dbResult)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.LanguageAdded);
        }

        //[SecuredOperation("admin,update")]
        [ValidationAspect(typeof(UpdateLanguageValidator))]
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

        //[SecuredOperation("admin,delete")]
        [CacheRemoveAspect("ILanguageService.Get")]
        public async Task<IResult> Delete(DeleteLanguageRequest request)
        {
            var languageToDelete = await _languageDal.GetAsync(l => l.LanguageId == request.LanguageId);

            if(languageToDelete is not null)
            {
                var result = BusinessRules.Run(await CheckIfExistInBooks(languageToDelete.LanguageBooks));

               if(result is not null)
                {
                    return result;
                }

                await _languageDal.DeleteAsync(languageToDelete);
                return new SuccessResult(Messages.LanguageDeleted);
            }

            return new ErrorResult(Messages.Error);
        }

        //[SecuredOperation("admin,get")]
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

        public async Task<IDataResult<List<GetListLanguageResponse>>> GetListAsync()
        {
            var data = await _languageDal.GetListAsync(null);

            if (data is not null)
            {
                var languagesResponse = _mapper.Map<List<GetListLanguageResponse>>(data);

                return new SuccessDataResult<List<GetListLanguageResponse>>(languagesResponse, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListLanguageResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListLanguageResponse>>> GetListAsyncSortedByName()
        {
            var data = await _languageDal.GetListAsyncOrderBy(null, orderBy: q => q.OrderBy(l => l.LanguageName));

            if (data is not null)
            {
                var languagesResponse = _mapper.Map<List<GetListLanguageResponse>>(data);

                return new SuccessDataResult<List<GetListLanguageResponse>>(languagesResponse, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListLanguageResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListLanguageResponse>>> GetListAsyncSortedByCreatedDate()
        {
            var data = await _languageDal.GetListAsyncOrderBy(null, orderBy: q => q.OrderByDescending(l => l.CreatedDate));

            if (data is not null)
            {
                var languagesResponse = _mapper.Map<List<GetListLanguageResponse>>(data);

                return new SuccessDataResult<List<GetListLanguageResponse>>(languagesResponse, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListLanguageResponse>>(Messages.Error);
        }

        //[SecuredOperation("admin,get")]
        [CacheAspect]
        public async Task<IDataResult<IPaginate<GetListLanguageResponse>>> GetPaginatedListAsync(PageRequest pageRequest)
        {
            var data = await _languageDal.GetPaginatedListAsync(
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
        private async Task<IResult> CheckIfExistInBooks(ICollection<Book> languageBooks)
        {
            if(languageBooks.Count < 1)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.LanguageExistInBooks);
        }
        private IDataResult<ILanguageRequest> CapitalizeFirstLetter(ILanguageRequest request)
        {
            string capitalizedLanguageName = char.ToUpper(request.LanguageName[0]) + request.LanguageName.Substring(1).ToLower();
            request.LanguageName = capitalizedLanguageName;
            return new SuccessDataResult<ILanguageRequest>(request);
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
