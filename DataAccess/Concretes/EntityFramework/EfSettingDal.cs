using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfSettingDal : EfRepositoryBase<Setting, LibraryAPIDbContext>, ISettingDal
    {
        public EfSettingDal(LibraryAPIDbContext context) : base(context)
        {
        }
    }
}
