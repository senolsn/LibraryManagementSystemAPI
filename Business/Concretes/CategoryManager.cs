﻿using AutoMapper;
using Business.Abstracts;
using Business.BusinessAspects;
using Business.Constants;
using Business.Dtos.Request.Category;
using Business.Dtos.Response.Category;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess.Paging;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class CategoryManager : ICategoryService
    {
        protected readonly ICategoryDal _categoryDal;
        protected readonly IBookService _bookService;
        protected readonly IMapper _mapper;
        public CategoryManager(ICategoryDal categoryDal, IBookService bookService, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
            _bookService = bookService;
        }

        //[SecuredOperation("admin,add")]
        [ValidationAspect(typeof (CategoryValidator))]
        [CacheRemoveAspect("ICategoryService.Get")]
        public async Task<IResult> Add(CreateCategoryRequest request)
        {
            var result = BusinessRules.Run(CapitalizeFirstLetter(request),await IsCategoryNameUnique(request.CategoryName));

            if( result is not null)
            {
                return result;
            }

            Category category = _mapper.Map<Category>(request);

            var createdCategory = await _categoryDal.AddAsync(category);

            if (createdCategory is null)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.CategoryAdded);
        }

        [SecuredOperation("admin,delete")]
        [CacheRemoveAspect("ICategoryService.Get")]
        public async Task<IResult> Delete(DeleteCategoryRequest request)
        {
            var categoryToDelete = await _categoryDal.GetAsync(c => c.CategoryId == request.CategoryId);

            if (categoryToDelete is not null)
            {
                var result = BusinessRules.Run(await CheckIfCategoryHasBooks(request.CategoryId));
                if (result is not null)
                {
                    return result;
                }

                await _categoryDal.DeleteAsync(categoryToDelete);
                return new SuccessResult(Messages.CategoryDeleted);
            }

            return new ErrorResult(Messages.Error);
        }

        [SecuredOperation("admin,update")]
        [ValidationAspect(typeof (CategoryValidator))]
        [CacheRemoveAspect("ICategoryService.Get")]
        public async Task<IResult> Update(UpdateCategoryRequest request)
        {
            var result = BusinessRules.Run(CapitalizeFirstLetter(request),await IsCategoryNameUnique(request.CategoryName));

            if (result is not null)
            {
                return result;
            }

            var categoryToUpdate = await _categoryDal.GetAsync(c => c.CategoryId == request.CategoryId);

            if (categoryToUpdate is null)
            {
                return new ErrorResult(Messages.Error);
            }

            _mapper.Map(request, categoryToUpdate);

            await _categoryDal.UpdateAsync(categoryToUpdate);

            return new SuccessResult(Messages.CategoryUpdated);
        }

        [SecuredOperation("admin,get")]
        [CacheAspect]
        public async Task<IDataResult<Category>> GetAsync(Guid categoryId)
        {
            var result = await _categoryDal.GetAsync(c => c.CategoryId == categoryId);

            var dbResult = await _categoryDal.SaveChangesAsync();

            if (!dbResult)
            {
                return new SuccessDataResult<Category>(result, Messages.CategoryListed);
            }

            return new ErrorDataResult<Category>(Messages.Error);
        }

        //[SecuredOperation("admin,get")]
        [CacheAspect]
        public async Task<IDataResult<IPaginate<GetListCategoryResponse>>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _categoryDal.GetListAsync(
                null,
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize,
                true);

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListCategoryResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListCategoryResponse>>(result, Messages.CategoriesListed);
            }

            return new ErrorDataResult<IPaginate<GetListCategoryResponse>>(Messages.Error);
        }

        
        #region Helper Methods
        private async Task<IResult> CheckIfCategoryHasBooks(Guid categoryId)
        {
            var result = await _bookService.GetAsyncByCategoryId(categoryId);

            if (result is not null)
            {
            return new SuccessResult();

            }
            return new ErrorResult(Messages.CategoryExistInBooks);
        }

        private async Task<IResult> IsCategoryNameUnique(string categoryName)
        {
            var result = await _categoryDal.GetAsync(c => c.CategoryName == categoryName);

            if(result is not null)
            {
                return new ErrorResult(Messages.CategoryNameNotUnique);
            }
            return new SuccessResult();
        }

        private IDataResult<CreateCategoryRequest> CapitalizeFirstLetter(CreateCategoryRequest request)
        {
            var stringToArray = request.CategoryName.Split(' ', ',', '.');
            string[] arrayToString = new string[stringToArray.Length];
            int count = 0;

            foreach (var word in stringToArray)
            {
               var capitalizedCategoryName =  char.ToUpper(word[0]) + word.Substring(1).ToLower();
                arrayToString[count] = capitalizedCategoryName;
                count++;
            }
            request.CategoryName = string.Join(" ", arrayToString);

            return new SuccessDataResult<CreateCategoryRequest>(request);
        }

        private IDataResult<UpdateCategoryRequest> CapitalizeFirstLetter(UpdateCategoryRequest request)
        {
            var stringToArray = request.CategoryName.Split(' ', ',','.');
            string[] arrayToString = new string[stringToArray.Length];
            int count = 0;

            foreach (var word in stringToArray)
            {
                var capitalizedCategoryName = char.ToUpper(word[0]) + word.Substring(1).ToLower();
                arrayToString[count] = capitalizedCategoryName;
                count++;
            }
            request.CategoryName = string.Join(" ", arrayToString);

            return new SuccessDataResult<UpdateCategoryRequest>(request);
        }
        #endregion

    }
}
