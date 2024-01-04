using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concretes
{
    public class EfPublisherDal : EfRepositoryBase<Publisher, LibraryAPIDbContext>, IPublisherDal
    {
        public EfPublisherDal(LibraryAPIDbContext context) : base(context)
        {
        }
    }
}
