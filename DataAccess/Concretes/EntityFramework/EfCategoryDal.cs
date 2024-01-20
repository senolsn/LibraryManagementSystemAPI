using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfCategoryDal : EfRepositoryBase<Category, LibraryAPIDbContext>, ICategoryDal
    {
        public EfCategoryDal(LibraryAPIDbContext context) : base(context)
        {
        }
    }
}
