using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concretes
{
    public class EfBookDal : EfRepositoryBase<Book, LibraryAPIDbContext>, IAsyncRepository<Book>,IBookDal
    {
        public EfBookDal(LibraryAPIDbContext context) : base(context)
        {
        }
    }
}
