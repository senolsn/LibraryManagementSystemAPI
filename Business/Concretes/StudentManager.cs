using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.StudentRequests;
using Business.Dtos.Response.StaffResponses;
using Business.Dtos.Response.StudentResponses;
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
    public class StudentManager : IStudentService
    {
        private readonly IStudentDal _studentDal;
        private readonly IMapper _mapper;

        public StudentManager(IStudentDal studentDal)
        {
            _studentDal = studentDal;
        }

        public async Task<IResult> Add(CreateStudentRequest request)
        {
            Student student = _mapper.Map<Student>(request);

            var createdStudent = await _studentDal.AddAsync(student);

            var dbResult = await _studentDal.SaveChangesAsync();

            if (!dbResult)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.StudentAdded);
        }

        public async Task<IDataResult<Student>> GetAsync(Guid studentId)
        {
            var result = await _studentDal.GetAsync(s => s.StudentId == studentId);

            if (result is not null)
            {
                return new SuccessDataResult<Student>(result, Messages.StudentListed);
            }

            return new ErrorDataResult<Student>(Messages.Error);
        }

        public async Task<IDataResult<List<GetAllStudentsResponse>>> GetListAsync()
        {
            var data = await _studentDal.GetListAsync(null);

            if (data is not null)
            {
                var studentsResponse = _mapper.Map<List<GetAllStudentsResponse>>(data);

                return new SuccessDataResult<List<GetAllStudentsResponse>>(studentsResponse, Messages.StudentsListed);
            }

            return new ErrorDataResult<List<GetAllStudentsResponse>>(Messages.Error);
        }
    }
}
