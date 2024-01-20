using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfBookCategoryDal : EfRepositoryBase<BookCategory, LibraryAPIDbContext>, IBookCategoryDal
    {
        public EfBookCategoryDal(LibraryAPIDbContext context) : base(context)
        {
        }
    }
}
