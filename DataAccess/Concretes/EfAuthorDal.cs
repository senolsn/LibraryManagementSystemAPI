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
    public class EfAuthorDal : EfRepositoryBase<Author, LibraryAPIDbContext>, IAuthorDal
    {
        public EfAuthorDal(LibraryAPIDbContext context) : base(context)
        {
        }
    }
}
