using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfDepartmentDal : EfRepositoryBase<Department, LibraryAPIDbContext>, IDepartmentDal
    {
        public EfDepartmentDal(LibraryAPIDbContext context) : base(context)
        {

        }
    }
}
