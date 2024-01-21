using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Category;
using Business.Dtos.Response.Category;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.CrossCuttingConcerns.Validation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entities.Concrete;
using System;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class CategoryManager : ICategoryService
    {
        protected readonly ICategoryDal _categoryDal;
        protected readonly IBookDal _bookDal;
        protected readonly IMapper _mapper;
        public CategoryManager(ICategoryDal categoryDal, IBookDal bookDal, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
            _bookDal = bookDal;
        }

        [ValidationAspect(typeof(CategoryValidator))]
        public async Task<IResult> Add(CreateCategoryRequest request)
        {
            Category category = _mapper.Map<Category>(request);

            var createdCategory = await _categoryDal.AddAsync(category);

            if (createdCategory is null)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.CategoryAdded);
        }

        public async Task<IResult> Delete(DeleteCategoryRequest request)
        {
            var categoryToDelete = await _categoryDal.GetAsync(c => c.CategoryId == request.CategoryId);

            if (categoryToDelete is not null)
            {
                var result = BusinessRules.Run(await checkIfExistInBooks(request.CategoryId));
                if (result is not null)
                {
                    return result;
                }

                await _categoryDal.DeleteAsync(categoryToDelete);
                return new SuccessResult(Messages.CategoryDeleted);
            }

            return new ErrorResult(Messages.Error);
        }

        public async Task<IDataResult<Category>> GetAsync(Guid categoryId)
        {
            var result = await _categoryDal.GetAsync(c => c.CategoryId == categoryId);

            if (result is not null)
            {
                return new SuccessDataResult<Category>(result, Messages.CategoryListed);
            }

            return new ErrorDataResult<Category>(Messages.Error);
        }

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
   
        public async Task<IResult> Update(UpdateCategoryRequest request)
        {
            var categoryToUpdate = await _categoryDal.GetAsync(c => c.CategoryId == request.CategoryId);

            if (categoryToUpdate is null)
            {
                return new ErrorResult(Messages.Error);
            }

            _mapper.Map(request, categoryToUpdate);

            await _categoryDal.UpdateAsync(categoryToUpdate);

            return new SuccessResult(Messages.CategoryUpdated);
        }


        private async Task<IResult> checkIfExistInBooks(Guid categoryId)
        {
            var result = await _bookDal.GetAsync(c => c.CategoryId == categoryId);

            if (result is not null)
            {
                return new ErrorResult(Messages.CategoryExistInBooks);
            }
            return new SuccessResult();
        }

        
    }
}
