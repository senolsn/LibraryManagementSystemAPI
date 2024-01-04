using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Department;
using Business.Dtos.Response.Department;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class DepartmentManager : IDepartmentService
    {
        protected readonly IDepartmentDal _departmentDal;
        protected readonly IMapper _mapper;

        public DepartmentManager(IDepartmentDal departmentDal, IMapper mapper)
        {
            _departmentDal = departmentDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CreateDepartmentRequest request)
        {
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

            if(departmentToDelete is not null)
            {
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

        public async Task<IDataResult<IPaginate<GetListDepartmentResponse>>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _departmentDal.GetListAsync(
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
    }
}
