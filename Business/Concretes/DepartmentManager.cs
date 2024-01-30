using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Department;
using Business.Dtos.Request.Faculty;
using Business.Dtos.Response.Department;
using Core.DataAccess.Paging;
using Core.Utilities.Business;
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
    public class DepartmentManager : IDepartmentService
    {
        protected readonly IDepartmentDal _departmentDal;
        protected readonly IMapper _mapper;
        protected readonly IUserService _userService;

        public DepartmentManager(IDepartmentDal departmentDal,IMapper mapper, IUserService userService)
        {
            _departmentDal = departmentDal;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<IResult> Add(CreateDepartmentRequest request)
        {
            var result = BusinessRules.Run(CapitalizeFirstLetter(request),await IsDepartmentNameUnique(request.DepartmentName));

            if(result is not null)
            {
                return result;
            }

            Department department = _mapper.Map<Department>(request);

            var createdDepartment = await _departmentDal.AddAsync(department);

            if (createdDepartment is null)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.DepartmentAdded);
        }

        public async Task<IResult> Update(UpdateDepartmentRequest request)
        {
            var departmentToUpdate = await _departmentDal.GetAsync(d => d.DepartmentId == request.DepartmentId);

            if(departmentToUpdate is null)
            {
                return new ErrorResult(Messages.Error);
            }

            _mapper.Map(request,departmentToUpdate);
            
            await _departmentDal.UpdateAsync(departmentToUpdate);

            return new SuccessResult(Messages.DepartmentUpdated);
        }

        public async Task<IResult> Delete(DeleteDepartmentRequest request)
        {
            var departmentToDelete = await _departmentDal.GetAsync(d => d.DepartmentId == request.DepartmentId);

            var result = BusinessRules.Run(await CheckIfExistInUsers(request.DepartmentId));

           if(departmentToDelete is not null)
            {
                if (result is not null)
                {
                    return result;
                }

                await _departmentDal.DeleteAsync(departmentToDelete);
                return new SuccessResult(Messages.DepartmentDeleted);
            }
           return new ErrorResult(Messages.Error);
        }

        public async Task<IDataResult<Department>> GetAsync(Guid departmentId)
        {
            var result = await _departmentDal.GetAsync(d => d.DepartmentId == departmentId);
            
            if(result is not null)
            {
                return new SuccessDataResult<Department>(result);
            }

            return new ErrorDataResult<Department>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListDepartmentResponse>>> GetListAsync()
        {
            var data = await _departmentDal.GetListAsync(null);

            if (data is not null)
            {
                var departmentsResponse = _mapper.Map<List<GetListDepartmentResponse>>(data);

                return new SuccessDataResult<List<GetListDepartmentResponse>>(departmentsResponse, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListDepartmentResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListDepartmentResponse>>> GetListAsyncSortedByName()
        {
            var data = await _departmentDal.GetListAsyncOrderBy(null, orderBy: q => q.OrderBy(d => d.DepartmentName));

            if (data is not null)
            {
                var departmentsResponse = _mapper.Map<List<GetListDepartmentResponse>>(data);

                return new SuccessDataResult<List<GetListDepartmentResponse>>(departmentsResponse, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListDepartmentResponse>>(Messages.Error);
        }

        public async Task<IDataResult<List<GetListDepartmentResponse>>> GetListAsyncSortedByCreatedDate()
        {
            var data = await _departmentDal.GetListAsyncOrderBy(null, orderBy: q => q.OrderByDescending(b => b.CreatedDate));

            if (data is not null)
            {
                var departmentsResponse = _mapper.Map<List<GetListDepartmentResponse>>(data);

                return new SuccessDataResult<List<GetListDepartmentResponse>>(departmentsResponse, Messages.BooksListed);
            }

            return new ErrorDataResult<List<GetListDepartmentResponse>>(Messages.Error);
        }

        public async Task<IDataResult<IPaginate<GetListDepartmentResponse>>> GetPaginatedListAsync(PageRequest pageRequest)
        {
            var data = await _departmentDal.GetPaginatedListAsync(
                null,
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
                );

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListDepartmentResponse>>(data);
                return new SuccessDataResult<IPaginate<GetListDepartmentResponse>>(result, Messages.DepartmentListed);
            }

            return new ErrorDataResult<IPaginate<GetListDepartmentResponse>>(Messages.Error);
        }

 
        #region Helper Methods
        private async Task<IResult> CheckIfExistInUsers(Guid departmentId)
        {
            var result = await _userService.GetAsyncByDepartmentId(departmentId);
            if (result.IsSuccess)
            {
                return new ErrorResult(Messages.DepartmentExistInUsers);
            }
            return new SuccessResult();

        }
        private IDataResult<IDepartmentRequest> CapitalizeFirstLetter(IDepartmentRequest request)
        {
            var stringToArray = request.DepartmentName.Split(' ', ',', '.');
            string[] arrayToString = new string[stringToArray.Length];
            int count = 0;

            foreach (var word in stringToArray)
            {
                var capitalizedCategoryName = char.ToUpper(word[0]) + word.Substring(1).ToLower();
                arrayToString[count] = capitalizedCategoryName;
                count++;
            }
            request.DepartmentName = string.Join(" ", arrayToString);

            return new SuccessDataResult<IDepartmentRequest>(request);
        }
        private async Task<IResult> IsDepartmentNameUnique(string departmentName)
        {
            var result = await _departmentDal.GetAsync(f => f.DepartmentName.ToUpper() == departmentName.ToUpper());

            if (result is not null)
            {
                return new ErrorResult(Messages.FacultyNameNotUnique);
            }
            return new SuccessResult();
        }
        #endregion

    }
}
