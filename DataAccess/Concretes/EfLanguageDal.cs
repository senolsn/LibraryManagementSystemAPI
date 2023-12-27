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
    public class EfLanguageDal:EfRepositoryBase<Language,LibraryAPIDbContext>,ILanguageDal
    {
        public EfLanguageDal(LibraryAPIDbContext context) : base(context)
        {
        }
    }
}
