using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes
{
    public class EfBookDal : EfRepositoryBase<Book, LibraryAPIDbContext>, IAsyncRepository<Book>,IBookDal
    {
        public EfBookDal(LibraryAPIDbContext context) : base(context)
        {
        }
    }
}
