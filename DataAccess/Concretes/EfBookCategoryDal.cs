using Core.DataAccess.Repositories;
using DataAccess.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes
{
    public class EfBookCategoryDal : EfRepositoryBase<BookCategory, LibraryAPIDbContext>
    {
        public EfBookCategoryDal(LibraryAPIDbContext context) : base(context)
        {
        }
    }
}
