using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concretes
{
    public class EfUserDal : EfRepositoryBase<User, LibraryAPIDbContext>, IUserDal
    {
        public EfUserDal(LibraryAPIDbContext context) : base(context)
        {
        }
    }
}
